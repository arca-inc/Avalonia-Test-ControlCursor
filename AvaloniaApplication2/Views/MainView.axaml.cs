using Avalonia.Controls;
using System.Timers;
using System;
using Avalonia.Threading;

namespace AvaloniaApplication2.Views;

public partial class MainView : UserControl
{
    private Timer _timer;

    public MainView()
    {
        InitializeComponent();

        StartTimer();
    }

    private void StartTimer()
    {
        _timer = new Timer(1000); // Intervalle de 1 seconde
        _timer.Elapsed += OnTimedEvent;
        _timer.AutoReset = true;
        _timer.Enabled = true;
    }

    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {

        Dispatcher.UIThread.InvokeAsync(() =>
        {
            ControlCursor.Point cursorPosition = ControlCursor.Cursor.GetPosition();
            this.FindControl<TextBlock>("test").Text = ("Position de la souris : " + cursorPosition);
        });
        
    }
}
