// <copyright file="SettingsViewModel.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Reflection;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.UI.Xaml;
using Microsoft.WindowsAPICodePack.Dialogs;
using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Helpers;

using Windows.ApplicationModel;
using Windows.System;

namespace PipeTech.Downloader.ViewModels;

/// <summary>
/// Setting vide model class.
/// </summary>
public partial class SettingsViewModel : ObservableRecipient
{
    private readonly IThemeSelectorService themeSelectorService;
    private readonly ILocalSettingsService localSettingsService;
    private readonly INavigationService navigationService;

    [ObservableProperty]
    private ElementTheme elementTheme;

    [ObservableProperty]
    private string versionDescription;

    [ObservableProperty]
    private string? dataFolder;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
    /// </summary>
    /// <param name="themeSelectorService">Theme selector service.</param>
    /// <param name="localSettingsService">Local settings service.</param>
    /// <param name="navigationService">Navigation service.</param>
    public SettingsViewModel(
        IThemeSelectorService themeSelectorService,
        ILocalSettingsService localSettingsService,
        INavigationService navigationService)
    {
        this.themeSelectorService = themeSelectorService;
        this.navigationService = navigationService;
        this.localSettingsService = localSettingsService;
        this.elementTheme = this.themeSelectorService.Theme;
        this.versionDescription = GetVersionDescription();

        this.EmailCommand = new AsyncRelayCommand(() =>
        {
            _ = Launcher.LaunchUriAsync(new Uri("mailto:support.pipetech.com"));
            return Task.CompletedTask;
        });

        this.SwitchThemeCommand = new RelayCommand<ElementTheme>(
            async (param) =>
            {
                if (this.ElementTheme != param)
                {
                    this.ElementTheme = param;
                    await this.themeSelectorService.SetThemeAsync(param);
                }
            });

        this.CloseCommand = new RelayCommand(() =>
        {
            if (this.navigationService.CanGoBack)
            {
                this.navigationService.GoBack();
            }
            else
            {
                this.navigationService.NavigateTo(typeof(DownloadsViewModel).FullName!, clearNavigation: true);
            }
        });

        this.LogFolderCommand = new AsyncRelayCommand(async () =>
        {
            await Launcher.LaunchFolderPathAsync(this.DiagnosticLogFolder);
        });

        this.BrowseFolderCommand = new RelayCommand(() =>
        {
            var d = new CommonOpenFileDialog()
            {
                IsFolderPicker = true,
                Title = "Select Download Folder",
            };

            if (d.ShowDialog() == CommonFileDialogResult.Ok)
            {
                this.DataFolder = d.FileName;
                _ = this.localSettingsService.SaveSettingAsync<string?>(
                    ILocalSettingsService.LastDataFolderKey,
                    this.DataFolder);
            }
        });

        _ = Task.Run(async () =>
        {
            this.DataFolder = await this.localSettingsService.ReadSettingAsync<string?>(ILocalSettingsService.LastDataFolderKey);
        });
    }

    /// <summary>
    /// Gets the diagnostic log folder.
    /// </summary>
    public string DiagnosticLogFolder => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
        "PipeTech",
        "PipeTech.Downloader");

    /// <summary>
    /// Gets the diagnostic log folder content.
    /// </summary>
    public string DiagnosticLogFolderContent => $"Log folder: {this.DiagnosticLogFolder}";

    /// <summary>
    /// Gets the email command.
    /// </summary>
    public IAsyncRelayCommand EmailCommand
    {
        get;
    }

    /// <summary>
    /// Gets the log folder command.
    /// </summary>
    public IAsyncRelayCommand LogFolderCommand
    {
        get;
    }

    /// <summary>
    /// Gets the command to browse for a folder.
    /// </summary>
    public IRelayCommand BrowseFolderCommand
    {
        get;
    }

    /// <summary>
    /// Gets the switch theme command.
    /// </summary>
    public ICommand SwitchThemeCommand
    {
        get;
    }

    /// <summary>
    /// Gets the switch theme command.
    /// </summary>
    public ICommand CloseCommand
    {
        get;
    }

    private static string GetVersionDescription()
    {
        Version version;

        if (RuntimeHelper.IsMSIX)
        {
            var packageVersion = Package.Current.Id.Version;
            version = new(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
        }
        else
        {
            version = Assembly.GetExecutingAssembly().GetName().Version!;
        }

        return $"{"AppDisplayName".GetLocalized()} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }
}
