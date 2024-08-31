using Avalonia.Controls;
using Avalonia.Platform;
using Avalonia.VisualTree;
using System.Runtime.InteropServices;
using System;

namespace AvaloniaApplication2.Views;


public partial class MainWindow : Window
{
    private const int GWL_EXSTYLE = -20;
    private const uint WS_EX_TOOLWINDOW = 0x00000080;

    public MainWindow()
    {
        InitializeComponent();
       ShowInTaskbar = false;
       WindowStartupLocation = WindowStartupLocation.Manual;
       var visualRoot = this.GetVisualRoot() as TopLevel;
       if (visualRoot != null && visualRoot.TryGetPlatformHandle() is { } platformHandle)
       {
           var hwnd = platformHandle.Handle;
           NativeMethods.SetWindowLong(hwnd, GWL_EXSTYLE, (uint)NativeMethods.GetWindowLong(hwnd, GWL_EXSTYLE) | WS_EX_TOOLWINDOW);
       }

    }
}
