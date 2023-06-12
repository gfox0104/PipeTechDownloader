// <copyright file="Program.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.UI.Dispatching;
using Microsoft.Windows.AppLifecycle;
using Syncfusion.Licensing;

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
    [STAThread]
    public static void Main(string[] args)
    {
        WinRT.ComWrappersSupport.InitializeComWrappers();
        var isRedirect = DecideRedirection();
        if (!isRedirect)
        {
            Microsoft.UI.Xaml.Application.Start((p) =>
            {
                SyncfusionLicenseProvider.RegisterLicense(PT.Inspection.Constants.SyncfusionLicenseCode);
                var context = new DispatcherQueueSynchronizationContext(
                    DispatcherQueue.GetForCurrentThread());
                SynchronizationContext.SetSynchronizationContext(context);
                _ = new App();
            });
        }
    }

    private static bool DecideRedirection()
    {
        var isRedirect = false;
        var args = AppInstance.GetCurrent().GetActivatedEventArgs();
        var keyInstance = AppInstance.FindOrRegisterForKey(APPKEY);

        if (!keyInstance.IsCurrent)
        {
            isRedirect = true;
            RedirectActivationTo(args, keyInstance);
            ////await keyInstance.RedirectActivationToAsync(args);
        }

        return isRedirect;
    }

    // Do the redirection on another thread, and use a non-blocking
    // wait method to wait for the redirection to complete.
    private static void RedirectActivationTo(
        AppActivationArguments args, AppInstance keyInstance)
    {
        var redirectSemaphore = new Semaphore(0, 1);
        Task.Run(() =>
        {
            keyInstance.RedirectActivationToAsync(args).AsTask().Wait();
            redirectSemaphore.Release();
        });
        redirectSemaphore.WaitOne();
    }
}
