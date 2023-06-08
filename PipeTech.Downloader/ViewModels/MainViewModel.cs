// <copyright file="MainViewModel.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Contracts.ViewModels;
using PipeTech.Downloader.Models;

namespace PipeTech.Downloader.ViewModels;

/// <summary>
/// Main view model class.
/// </summary>
public partial class MainViewModel : BindableRecipient, INavigationAware
{
    private readonly INavigationService navigationService;

    [ObservableProperty]
    private string? downloadName;

    [ObservableProperty]
    private int? totalCount;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainViewModel"/> class.
    /// </summary>
    /// <param name="navigationService">Navigation service.</param>
    public MainViewModel(
        INavigationService navigationService)
    {
        this.navigationService = navigationService;
        this.Inspections = new ObservableCollection<DownloadInspection>();
        this.Inspections.CollectionChanged += this.Inspections_CollectionChanged;
        this.CloseCommand = new RelayCommand(() =>
        {
            this.navigationService.GoBack();
        });
    }

    /// <summary>
    /// Gets the close command.
    /// </summary>
    public ICommand CloseCommand
    {
        get;
    }

    /// <summary>
    /// Gets the inspections.
    /// </summary>
    public ObservableCollection<DownloadInspection> Inspections
    {
        get;
    }

    /// <summary>
    /// Gets the inspection count string.
    /// </summary>
    public string? InspectionCountString
    {
        get
        {
            var value = $"{this.Inspections.Where(dl => dl.State != DownloadInspection.States.Loading).Count()}";
            if (this.TotalCount is int totalCount)
            {
                return value + $" of {totalCount}";
            }

            return value;
        }
    }

    /// <inheritdoc/>
    public void OnNavigatedFrom()
    {
    }

    /// <inheritdoc/>
    public void OnNavigatedTo(object parameter)
    {
        if (parameter is not Uri uri)
        {
            if (this.navigationService.CanGoBack)
            {
                this.navigationService.GoBack();
            }
            else
            {
                this.navigationService.NavigateTo(typeof(DownloadsViewModel).FullName!);
            }

            return;
        }

        if (!Guid.TryParse(uri.Host, out var g))
        {
        }

        var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
        if (query != null)
        {
            this.DownloadName = query.Get("name");
            if (int.TryParse(query.Get("count"), out var count))
            {
                this.TotalCount = count;
            }
        }

        // Make the call
    }

    /// <inheritdoc/>
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        switch (e.PropertyName)
        {
            case nameof(this.TotalCount):
                this.RaisePropertyChanged(nameof(this.InspectionCountString));
                break;
            default:
                break;
        }
    }

    private void Inspections_CollectionChanged(
        object? sender,
        System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems is not null)
        {
            foreach (INotifyPropertyChanged item in e.OldItems)
            {
                item.PropertyChanged -= this.Inspection_PropertyChanged;
            }
        }

        if (e.NewItems is not null)
        {
            foreach (INotifyPropertyChanged item in e.NewItems)
            {
                item.PropertyChanged += this.Inspection_PropertyChanged;
            }
        }

        this.RaisePropertyChanged(nameof(this.InspectionCountString));
    }

    private void Inspection_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
    }
}
