// <copyright file="ServiceProviderActivator.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Hangfire;

namespace PipeTech.Downloader.Models;

/// <summary>
/// Service provider activator class.
/// </summary>
public class ServiceProviderActivator : JobActivator
{
    private readonly IServiceProvider serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceProviderActivator"/> class.
    /// </summary>
    /// <param name="serviceProvider">Service provider.</param>
    public ServiceProviderActivator(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    /// <inheritdoc/>
    public override object? ActivateJob(Type jobType)
    {
        return this.serviceProvider?.GetService(jobType);
    }
}
