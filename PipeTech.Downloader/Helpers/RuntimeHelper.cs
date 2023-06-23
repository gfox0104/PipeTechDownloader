// <copyright file="RuntimeHelper.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Runtime.InteropServices;
using System.Text;

namespace PipeTech.Downloader.Helpers;

/// <summary>
/// Runtime helper class.
/// </summary>
public class RuntimeHelper
{
    /// <summary>
    /// Gets a value indicating whether the current app is running as a MSIX.
    /// </summary>
    public static bool IsMSIX
    {
        get
        {
            var length = 0;

            return GetCurrentPackageFullName(ref length, null) != 15700L;
        }
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int GetCurrentPackageFullName(ref int packageFullNameLength, StringBuilder? packageFullName);
}
