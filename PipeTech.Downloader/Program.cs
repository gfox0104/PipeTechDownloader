// <copyright file="Program.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.UI.Dispatching;
using Microsoft.Windows.AppLifecycle;

namespace PipeTech.Downloader;

/// <summary>
/// Program class.
/// </summary>
public class Program
{
    private static readonly string APPKEY = "b972964b-1da2-4afe-9d66-c45beb67ac98";

    /// <summary>
    /// Main entry point.
    /// </summary>
    /// <param name="args">Arguments.</param>
    /// <returns>Asynchronous task.</returns>
    [STAThread]
    public static async Task Main(string[] args)
    {
        WinRT.ComWrappersSupport.InitializeComWrappers();
        var isRedirect = await DecideRedirection();
        if (!isRedirect)
        {
            Microsoft.UI.Xaml.Application.Start((p) =>
            {
                var context = new DispatcherQueueSynchronizationContext(
                    DispatcherQueue.GetForCurrentThread());
                SynchronizationContext.SetSynchronizationContext(context);
                _ = new App();
            });
        }
    }

    private static async Task<bool> DecideRedirection()
    {
        var isRedirect = false;
        var args = AppInstance.GetCurrent().GetActivatedEventArgs();
        var keyInstance = AppInstance.FindOrRegisterForKey(APPKEY);

        if (!keyInstance.IsCurrent)
        {
            isRedirect = true;
            await keyInstance.RedirectActivationToAsync(args);
        }

        return isRedirect;
    }
}
