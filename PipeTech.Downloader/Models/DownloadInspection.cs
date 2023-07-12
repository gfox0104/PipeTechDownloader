// <copyright file="DownloadInspection.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PipeTech.Downloader.Models;

/// <summary>
/// Download inspection class.
/// </summary>
public partial class DownloadInspection : BindableRecipient, IDisposable
{
    /// <summary>
    /// Gets or sets the current state of the inspection.
    /// </summary>
    [ObservableProperty]
    private States state;

    /// <summary>
    /// Gets or sets completed path to the data.
    /// </summary>
    [ObservableProperty]
    private string? dataCompletePath;

    /// <summary>
    /// Gets or sets the completed path of the report in the inspection.
    /// </summary>
    [ObservableProperty]
    private string? reportCompletePath;

    /// <summary>
    /// Gets or sets the completed path of the exchange database in the inspection.
    /// </summary>
    [ObservableProperty]
    private string? exchangeDBCompletePath;

    /// <summary>
    /// Gets or sets the name for the inspection.
    /// </summary>
    [ObservableProperty]
    private string? name;

    /// <summary>
    /// Gets or sets the project name.
    /// </summary>
    [ObservableProperty]
    private string? project;

    [ObservableProperty]
    private string? downloadPath;

    /// <summary>
    /// Gets or sets the Json string.
    /// </summary>
    [ObservableProperty]
    private JsonElement? json;

    [ObservableProperty]
    private string? lastError;

    /// <summary>
    /// Initializes a new instance of the <see cref="DownloadInspection"/> class.
    /// </summary>
    public DownloadInspection()
    {
        this.Files.CollectionChanged += this.Files_CollectionChanged;
    }

    /// <summary>
    /// States of the download.
    /// </summary>
    public enum States
    {
        /// <summary>
        /// Loading.
        /// </summary>
        Loading,

        /// <summary>
        /// Error.
        /// </summary>
        Errored,

        /// <summary>
        /// Complete.
        /// </summary>
        Complete,

        /// <summary>
        /// Queued.
        /// </summary>
        Queued,

        /// <summary>
        /// Processing.
        /// </summary>
        Processing,

        /// <summary>
        /// Staged or ready for to confirm download.
        /// </summary>
        Staged,

        /// <summary>
        /// Paused.
        /// </summary>
        Paused,
    }

    /// <summary>
    /// Gets or sets the files but only by parsing.
    /// </summary>
    [JsonPropertyName("Files")]
    public File[] ParsingFiles
    {
        get => this.Files.ToArray();
        set
        {
            this.Files.Clear();
            this.Files.AddRange(value);
        }
    }

    /// <summary>
    /// Gets the files.
    /// </summary>
    [JsonIgnore]
    public ObservableCollection<File> Files { get; } = new();

    /// <summary>
    /// Gets the total size of the inspection.
    /// </summary>
    [JsonIgnore]
    public long TotalSize => this.Files?.Sum(f => f.Size ?? 0) ?? 0;

    /// <summary>
    /// Gets the progress based on the files.
    /// </summary>
    [JsonIgnore]
    public decimal Progress
    {
        get
        {
            try
            {
                var count = 0;
                var total = 0;
                var data = this.Files?.FirstOrDefault(f => Path.GetExtension(f.DownloadPath).ToUpperInvariant() == ".PTDX");
                if (data is not null)
                {
                    total += 1;
                    if (!string.IsNullOrEmpty(this.DataCompletePath) &&
                        System.IO.File.Exists(this.DataCompletePath))
                    {
                        count += 1;
                    }
                }

                var notDataFiles = this.Files?.Where(f => Path.GetExtension(f.DownloadPath).ToUpperInvariant() != ".PTDX");
                if (notDataFiles?.Any() == true)
                {
                    var totalSize = notDataFiles.Sum(f => 1);
                    var currentSize = notDataFiles.Sum(f => (f?.DownloadedSize ?? 0) == (f?.Size ?? 1) ? 1 : 0);
                    total += totalSize;
                    count += currentSize;
                }

                return (decimal)count / (total == 0 ? 1m : (decimal)total);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (this.Files is not null)
        {
            while (this.Files.Count > 0)
            {
                this.Files.RemoveAt(0);
            }

            this.Files.CollectionChanged -= this.Files_CollectionChanged;
        }
    }

    /// <inheritdoc/>
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        switch (e.PropertyName)
        {
            case nameof(this.ReportCompletePath):
            case nameof(this.ExchangeDBCompletePath):
            case nameof(this.DataCompletePath):
                this.RaisePropertyChanged(nameof(this.Progress));
                break;
            default:
                break;
        }
    }

    private void Files_CollectionChanged(
        object? sender,
        System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems is not null)
        {
            foreach (var oldItem in e.OldItems)
            {
                if (oldItem is File f)
                {
                    f.PropertyChanged -= this.File_PropertyChanged;
                }
            }
        }

        if (e.NewItems is not null)
        {
            foreach (var newItem in e.NewItems)
            {
                if (newItem is File f)
                {
                    f.PropertyChanged += this.File_PropertyChanged;
                }
            }
        }

        this.RaisePropertyChanged(nameof(this.TotalSize));
        this.RaisePropertyChanged(nameof(this.Progress));
    }

    private void File_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(File.Size):
                this.RaisePropertyChanged(nameof(this.TotalSize));
                this.RaisePropertyChanged(nameof(this.Progress));
                break;
            case nameof(File.DownloadedSize):
                this.RaisePropertyChanged(nameof(this.Progress));
                break;
            default:
                break;
        }
    }
}
