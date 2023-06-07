// <copyright file="PageService.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml.Controls;

using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.ViewModels;
using PipeTech.Downloader.Views;

namespace PipeTech.Downloader.Services;

/// <summary>
/// Page service class.
/// </summary>
public class PageService : IPageService
{
    private readonly Dictionary<string, Type> pages = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="PageService"/> class.
    /// </summary>
    public PageService()
    {
        this.Configure<MainViewModel, MainPage>();
        this.Configure<SettingsViewModel, SettingsPage>();
        this.Configure<DownloadsViewModel, DownloadsPage>();
    }

    /// <inheritdoc/>
    public Type GetPageType(string key)
    {
        Type? pageType;
        lock (this.pages)
        {
            if (!this.pages.TryGetValue(key, out pageType))
            {
                throw new ArgumentException($"Page not found: {key}. Did you forget to call PageService.Configure?");
            }
        }

        return pageType;
    }

    private void Configure<TVM, TV>()
        where TVM : ObservableObject
        where TV : Page
    {
        lock (this.pages)
        {
            var key = typeof(TVM).FullName!;
            if (this.pages.ContainsKey(key))
            {
                throw new ArgumentException($"The key {key} is already configured in PageService");
            }

            var type = typeof(TV);
            if (this.pages.ContainsValue(type))
            {
                throw new ArgumentException($"This type is already configured with key {this.pages.First(p => p.Value == type).Key}");
            }

            this.pages.Add(key, type);
        }
    }
}
