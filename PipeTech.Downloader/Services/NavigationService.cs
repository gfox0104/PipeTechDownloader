// <copyright file="NavigationService.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Contracts.ViewModels;
using PipeTech.Downloader.Helpers;

namespace PipeTech.Downloader.Services;

// For more information on navigation between pages see
// https://github.com/microsoft/TemplateStudio/blob/main/docs/WinUI/navigation.md

/// <summary>
/// Nagivation service class.
/// </summary>
public class NavigationService : INavigationService
{
    private readonly IPageService pageService;
    private readonly ILogger<NavigationService>? logger;

    private object? lastParameterUsed;
    private Frame? frame;

    /// <summary>
    /// Initializes a new instance of the <see cref="NavigationService"/> class.
    /// </summary>
    /// <param name="pageService">Page service.</param>
    /// <param name="logger">Logger service.</param>
    public NavigationService(
        IPageService pageService,
        ILogger<NavigationService>? logger = null)
    {
        this.pageService = pageService;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public event NavigatedEventHandler? Navigated;

    /// <inheritdoc/>
    public Frame? Frame
    {
        get
        {
            if (this.frame == null)
            {
                this.frame = App.MainWindow.Content as Frame;
                this.RegisterFrameEvents();
            }

            return this.frame;
        }

        set
        {
            this.UnregisterFrameEvents();
            this.frame = value;
            this.RegisterFrameEvents();
        }
    }

    /// <inheritdoc/>
    [MemberNotNullWhen(true, nameof(Frame), nameof(frame))]
    public bool CanGoBack => this.Frame != null && this.Frame.CanGoBack;

    /// <inheritdoc/>
    public bool GoBack()
    {
        if (this.CanGoBack)
        {
            var vmBeforeNavigation = this.frame.GetPageViewModel();
            this.frame.GoBack();
            if (vmBeforeNavigation is INavigationAware navigationAware)
            {
                this.logger?.LogDebug("OnNavigatedFrom per GoBack");
                navigationAware.OnNavigatedFrom();
            }

            return true;
        }

        return false;
    }

    /// <inheritdoc/>
    public bool NavigateTo(string pageKey, object? parameter = null, bool clearNavigation = false)
    {
        var pageType = this.pageService.GetPageType(pageKey);

        if (this.frame != null && (this.frame.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(this.lastParameterUsed))))
        {
            this.frame.Tag = clearNavigation;
            var vmBeforeNavigation = this.frame.GetPageViewModel();
            var navigated = this.frame.Navigate(pageType, parameter);
            if (navigated)
            {
                this.lastParameterUsed = parameter;
                if (vmBeforeNavigation is INavigationAware navigationAware)
                {
                    this.logger?.LogDebug($"OnNavigatedFrom per NavigateTo {pageType}:{parameter}");
                    navigationAware.OnNavigatedFrom();
                }
            }

            return navigated;
        }

        return false;
    }

    private void RegisterFrameEvents()
    {
        if (this.frame != null)
        {
            this.frame.Navigated += this.OnNavigated;
        }
    }

    private void UnregisterFrameEvents()
    {
        if (this.frame != null)
        {
            this.frame.Navigated -= this.OnNavigated;
        }
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        if (sender is Frame frame)
        {
            var clearNavigation = (bool)frame.Tag;
            if (clearNavigation)
            {
                frame.BackStack.Clear();
            }

            if (frame.GetPageViewModel() is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedTo(e.Parameter);
            }

            this.Navigated?.Invoke(sender, e);
        }
    }
}
