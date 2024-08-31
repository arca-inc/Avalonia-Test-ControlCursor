using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using AvaloniaApplication2.ViewModels;
using AvaloniaApplication2.Views;
using System.Timers;
using System;
using Avalonia.Controls;
using System.Linq;

namespace AvaloniaApplication2;

public partial class App : Application
{

    

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private TrayIcon trayIcon;
    private NativeMenu contextMenu;

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }

        string iconPath = "C:\\Users\\Warmadon\\Downloads\\logo (1).png";
        string toolTipText = "none";

        trayIcon = new TrayIcon
        {
            Icon = new WindowIcon(iconPath),
            ToolTipText = toolTipText
        };

        contextMenu = new NativeMenu();
        contextMenu.NeedsUpdate += OnMenuOpened;
        trayIcon.Menu = contextMenu;
        trayIcon.IsVisible = true;

        

        // Ajoutez un gestionnaire d'événements pour chaque élément de menu
        AddMenuItem("Open", (sender, e) => {});
        AddSeparator();
        AddMenuItem("Exit", (sender, e) => {});


        base.OnFrameworkInitializationCompleted();
    }

    private void OnMenuOpened(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }


    public void AddMenuItem(string header, EventHandler onClick)
    {
        var menuItem = new NativeMenuItem(header);
        menuItem.Click += onClick;
        contextMenu.Items.Add(menuItem);
    }

    public void AddSeparator()
    {
        var separator = new NativeMenuItemSeparator();
        contextMenu.Items.Add(separator);
    }

    public void RemoveMenuItem(string header)
    {
        var menuItem = contextMenu.Items.OfType<NativeMenuItem>().FirstOrDefault(item => item.Header == header);
        if (menuItem != null)
        {
            contextMenu.Items.Remove(menuItem);
        }
    }

    public void SetToolTipText(string toolTipText)
    {
        trayIcon.ToolTipText = toolTipText;
    }
    public void SetIcon(string iconPath)
    {
        trayIcon.Icon = new WindowIcon(iconPath);
    }
}
