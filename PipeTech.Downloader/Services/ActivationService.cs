﻿// <copyright file="ActivationService.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Diagnostics.Eventing.Reader;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using PipeTech.Downloader.Activation;
using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Views;

namespace PipeTech.Downloader.Services;

/// <summary>
/// Activation service class.
/// </summary>
public class ActivationService : IActivationService
{
    private readonly ActivationHandler<LaunchActivatedEventArgs> defaultHandler;
    private readonly IEnumerable<IActivationHandler> activationHandlers;
    private readonly IThemeSelectorService themeSelectorService;
    private UIElement? shell = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="ActivationService"/> class.
    /// </summary>
    /// <param name="defaultHandler">Default activation handler.</param>
    /// <param name="activationHandlers">All activation handlers.</param>
    /// <param name="themeSelectorService">Theme selector service.</param>
    public ActivationService(
        ActivationHandler<LaunchActivatedEventArgs> defaultHandler,
        IEnumerable<IActivationHandler> activationHandlers,
        IThemeSelectorService themeSelectorService)
    {
        this.defaultHandler = defaultHandler;
        this.activationHandlers = activationHandlers;
        this.themeSelectorService = themeSelectorService;
    }

    /// <inheritdoc/>
    public async Task ActivateAsync(object activationArgs)
    {
        // Execute tasks before activation.
        await this.InitializeAsync();

        // Set the MainWindow Content.
        void FillContent()
        {
            if (App.MainWindow.Content == null)
            {
                this.shell = App.GetService<ShellPage>();
                App.MainWindow.Content = this.shell ?? new Frame();
            }
        }

        if (!App.MainWindow.DispatcherQueue.HasThreadAccess)
        {
            await App.MainWindow.DispatcherQueue.EnqueueAsync(
                FillContent,
                Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal);
        }
        else
        {
            FillContent();
        }

        // Handle activation via ActivationHandlers.
        await this.HandleActivationAsync(activationArgs);

        // Activate the MainWindow.
        if (!App.MainWindow.DispatcherQueue.HasThreadAccess)
        {
            await App.MainWindow.DispatcherQueue.EnqueueAsync(
                App.MainWindow.Activate,
                Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal);
        }
        else
        {
            App.MainWindow.Activate();
        }

        // Execute tasks after activation.
        await this.StartupAsync();
    }

    private async Task HandleActivationAsync(object activationArgs)
    {
        async Task Handle()
        {
            var activationHandler = this.activationHandlers.FirstOrDefault(h => h.CanHandle(activationArgs));

            if (activationHandler != null)
            {
                await activationHandler.HandleAsync(activationArgs);
            }

            if (this.defaultHandler.CanHandle(activationArgs))
            {
                await this.defaultHandler.HandleAsync(activationArgs);
            }
        }

        if (!App.MainWindow.DispatcherQueue.HasThreadAccess)
        {
            await App.MainWindow.DispatcherQueue.EnqueueAsync(
                Handle,
                Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal);
        }
        else
        {
            await Handle();
        }
    }

    private async Task InitializeAsync()
    {
        if (!App.MainWindow.DispatcherQueue.HasThreadAccess)
        {
            await App.MainWindow.DispatcherQueue.EnqueueAsync(
                this.themeSelectorService.InitializeAsync);
        }
        else
        {
            await this.themeSelectorService.InitializeAsync().ConfigureAwait(false);
        }

        await Task.CompletedTask;
    }

    private async Task StartupAsync()
    {
        if (!App.MainWindow.DispatcherQueue.HasThreadAccess)
        {
            await App.MainWindow.DispatcherQueue.EnqueueAsync(
                this.themeSelectorService.SetRequestedThemeAsync);
        }
        else
        {
            await this.themeSelectorService.SetRequestedThemeAsync();
        }

        await Task.CompletedTask;
    }
}
