// <copyright file="ShellPage.xaml.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Helpers;
using PipeTech.Downloader.ViewModels;

using Windows.System;

namespace PipeTech.Downloader.Views;

/// <summary>
/// ShellPage class.
/// </summary>
public sealed partial class ShellPage : Page
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShellPage"/> class.
    /// </summary>
    /// <param name="viewModel">View model.</param>
    public ShellPage(ShellViewModel viewModel)
    {
        this.ViewModel = viewModel;
        this.InitializeComponent();

        this.ViewModel.NavigationService.Frame = this.NavigationFrame;

        // https://docs.microsoft.com/windows/apps/develop/title-bar?tabs=winui3#full-customization
        App.MainWindow.ExtendsContentIntoTitleBar = true;
        App.MainWindow.SetTitleBar(this.AppTitleBar);
        App.MainWindow.Activated += this.MainWindow_Activated;

        this.ViewModel.CurrentTheme = this.RequestedTheme;
        this.RegisterPropertyChangedCallback(
            FrameworkElement.RequestedThemeProperty,
            (s, d) =>
            {
                if (s is ShellPage sp && d == FrameworkElement.RequestedThemeProperty)
                {
                    sp.ViewModel.CurrentTheme = sp.RequestedTheme;
                }
            });

        ////this.AppTitleBarText.Text = "AppDisplayName".GetLocalized();
    }

    /// <summary>
    /// Gets the view model.
    /// </summary>
    public ShellViewModel ViewModel
    {
        get;
    }

    private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
    {
        var keyboardAccelerator = new KeyboardAccelerator() { Key = key };

        if (modifiers.HasValue)
        {
            keyboardAccelerator.Modifiers = modifiers.Value;
        }

        keyboardAccelerator.Invoked += OnKeyboardAcceleratorInvoked;

        return keyboardAccelerator;
    }

    private static void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        var navigationService = App.GetService<INavigationService>();

        var result = navigationService.GoBack();

        args.Handled = result;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        TitleBarHelper.UpdateTitleBar(this.RequestedTheme);

        this.KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu));
        this.KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.GoBack));

        this.ShellMenuBarSettingsButton.AddHandler(
            UIElement.PointerPressedEvent,
            new PointerEventHandler(this.ShellMenuBarSettingsButton_PointerPressed),
            true);
        this.ShellMenuBarSettingsButton.AddHandler(
            UIElement.PointerReleasedEvent,
            new PointerEventHandler(this.ShellMenuBarSettingsButton_PointerReleased),
            true);
    }

    private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
    {
        ////var resource = args.WindowActivationState == WindowActivationState.Deactivated ? "WindowCaptionForegroundDisabled" : "WindowCaptionForeground";

        ////this.AppTitleBarText.Foreground = (SolidColorBrush)App.Current.Resources[resource];
        ////App.AppTitlebar = this.AppTitleBarText as UIElement;
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        this.ShellMenuBarSettingsButton.RemoveHandler(
            UIElement.PointerPressedEvent,
            (PointerEventHandler)this.ShellMenuBarSettingsButton_PointerPressed);
        this.ShellMenuBarSettingsButton.RemoveHandler(
            UIElement.PointerReleasedEvent,
            (PointerEventHandler)this.ShellMenuBarSettingsButton_PointerReleased);

        if (this.ViewModel?.NavigationService?.Frame?.Content is MainPage m)
        {
            m.ViewModel?.Dispose();
        }

        this.taskbarIcon?.Dispose();
    }

    private void ShellMenuBarSettingsButton_PointerEntered(object sender, PointerRoutedEventArgs e)
    {
        AnimatedIcon.SetState((UIElement)sender, "PointerOver");
    }

    private void ShellMenuBarSettingsButton_PointerPressed(object sender, PointerRoutedEventArgs e)
    {
        AnimatedIcon.SetState((UIElement)sender, "Pressed");
    }

    private void ShellMenuBarSettingsButton_PointerReleased(object sender, PointerRoutedEventArgs e)
    {
        AnimatedIcon.SetState((UIElement)sender, "Normal");
    }

    private void ShellMenuBarSettingsButton_PointerExited(object sender, PointerRoutedEventArgs e)
    {
        AnimatedIcon.SetState((UIElement)sender, "Normal");
    }
}
