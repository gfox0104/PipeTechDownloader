// <copyright file="BindableRecipient.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PipeTech.Downloader.Models;

/// <summary>
/// Bindable Recipient class.
/// </summary>
public class BindableRecipient : ObservableRecipient
{
    /// <summary>
    /// Raises this object's PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">Name of the property used to notify listeners. This
    /// value is optional and can be provided automatically when invoked from compilers
    /// that support <see cref="CallerMemberNameAttribute"/>.</param>
    protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        this.OnPropertyChanged(propertyName);
    }

    /// <summary>
    /// Raises this object's PropertyChanging event.
    /// </summary>
    /// <param name="propertyName">Name of the property used to notify listeners. This
    /// value is optional and can be provided automatically when invoked from compilers
    /// that support <see cref="CallerMemberNameAttribute"/>.</param>
    protected void RaisePropertyChanging([CallerMemberName] string? propertyName = null)
    {
        this.OnPropertyChanging(propertyName);
    }
}
