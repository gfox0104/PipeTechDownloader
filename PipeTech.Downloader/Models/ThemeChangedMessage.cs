// <copyright file="ThemeChangedMessage.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.UI.Xaml;

namespace PipeTech.Downloader.Models;

/// <summary>
/// Theme changed message class.
/// </summary>
public class ThemeChangedMessage : ValueChangedMessage<ElementTheme>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ThemeChangedMessage"/> class.
    /// </summary>
    /// <param name="value">Theme.</param>
    public ThemeChangedMessage(ElementTheme value)
        : base(value)
    {
    }
}
