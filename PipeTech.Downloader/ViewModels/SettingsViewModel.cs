// <copyright file="SettingsViewModel.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Reflection;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.UI.Xaml;

using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Helpers;

using Windows.ApplicationModel;

namespace PipeTech.Downloader.ViewModels;

/// <summary>
/// Setting vide model class.
/// </summary>
public partial class SettingsViewModel : ObservableRecipient
{
    private readonly IThemeSelectorService themeSelectorService;

    [ObservableProperty]
    private ElementTheme elementTheme;

    [ObservableProperty]
    private string versionDescription;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
    /// </summary>
    /// <param name="themeSelectorService">Theme selector service.</param>
    public SettingsViewModel(IThemeSelectorService themeSelectorService)
    {
        this.themeSelectorService = themeSelectorService;
        this.elementTheme = this.themeSelectorService.Theme;
        this.versionDescription = GetVersionDescription();

        this.SwitchThemeCommand = new RelayCommand<ElementTheme>(
            async (param) =>
            {
                if (this.ElementTheme != param)
                {
                    this.ElementTheme = param;
                    await this.themeSelectorService.SetThemeAsync(param);
                }
            });
    }

    /// <summary>
    /// Gets the switch theme command.
    /// </summary>
    public ICommand SwitchThemeCommand
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
