using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using AnyStatus.Apps.Windows.Features.NamedPipe;
using AnyStatus.Core;

namespace AnyStatus.Apps.Windows;

public static class Program
{
    [STAThread]
    private static void Main()
    {
        Debug.WriteLine("In Main");

        var mutexAcquired = false;
        var mutex         = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");

        try
        {
            if (mutex.WaitOne(200, true))
            {
                mutexAcquired = true;

                Bootstrapper.Bootstrap().Run();
            }
            else
            {
                new NamedPipeClient().Send("activate");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Oops! An unexpected error occurred.\n" + ex, "AnyStatus", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            if (mutexAcquired)
            {
                mutex.ReleaseMutex();
            }
        }
    }
}