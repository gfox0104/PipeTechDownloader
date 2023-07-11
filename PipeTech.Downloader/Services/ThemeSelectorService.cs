// <copyright file="ThemeSelectorService.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml;

using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Helpers;
using PipeTech.Downloader.Models;

namespace PipeTech.Downloader.Services;

/// <summary>
/// The theme selector service.
/// </summary>
public class ThemeSelectorService : IThemeSelectorService
{
    private const string SettingsKey = ILocalSettingsService.AppBackgroundRequestedThemeKey;

    private readonly ILocalSettingsService localSettingsService;
    private readonly IMessenger? messenger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ThemeSelectorService"/> class.
    /// </summary>
    /// <param name="localSettingsService">Local settings service.</param>
    /// <param name="messenger">Messenger service.</param>
    public ThemeSelectorService(
        ILocalSettingsService localSettingsService,
        IMessenger? messenger = null)
    {
        this.localSettingsService = localSettingsService;
        this.messenger = messenger;
    }

    /// <inheritdoc/>
    public ElementTheme Theme { get; set; } = ElementTheme.Default;

    /// <inheritdoc/>
    public async Task InitializeAsync()
    {
        this.Theme = await this.LoadThemeFromSettingsAsync();
        await Task.CompletedTask;
    }

    /// <inheritdoc/>
    public async Task SetThemeAsync(ElementTheme theme)
    {
        this.Theme = theme;

        await this.SetRequestedThemeAsync();
        await this.SaveThemeInSettingsAsync(this.Theme);
    }

    /// <inheritdoc/>
    public async Task SetRequestedThemeAsync()
    {
        if (App.MainWindow.Content is FrameworkElement rootElement)
        {
            rootElement.RequestedTheme = this.Theme;

            TitleBarHelper.UpdateTitleBar(this.Theme);
        }

        this.messenger?.Send(new ThemeChangedMessage(this.Theme));
        await Task.CompletedTask;
    }

    private async Task<ElementTheme> LoadThemeFromSettingsAsync()
    {
        var themeName = await this.localSettingsService.ReadSettingAsync<string>(SettingsKey);

        if (Enum.TryParse(themeName, out ElementTheme cacheTheme))
        {
            return cacheTheme;
        }

        return ElementTheme.Default;
    }

    private async Task SaveThemeInSettingsAsync(ElementTheme theme)
    {
        await this.localSettingsService.SaveSettingAsync(SettingsKey, theme.ToString());
    }
}
