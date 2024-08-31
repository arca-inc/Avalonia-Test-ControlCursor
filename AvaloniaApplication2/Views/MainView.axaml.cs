using Avalonia.Controls;
using System.Timers;
using System;
using Avalonia.Threading;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Interactivity;

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

            AeraManager manager = new AeraManager();

            manager.AddAera(new Aera("test", 0, 0, 100, 100));

            ControlCursor.Point cursorPosition = ControlCursor.Cursor.GetPosition();
            
            Aera found = manager.GetAeraFromPoint(cursorPosition.x, cursorPosition.y);
            if(found != null)
            {
                this.FindControl<TextBlock>("test").Text = ("Position de la souris : " + cursorPosition + " dans " + found.ToString());
            }else this.FindControl<TextBlock>("test").Text = ("Position de la souris : " + cursorPosition);
        });
        
    }

    private void OpenSettings_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var settingsWindow = new SettingsWindow();
        settingsWindow.Show();
    }
}
public class Aera
{
    public string Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public Aera(string id, int x, int y, int width, int height)
    {
        Id = id;
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public override string ToString()
    {
        return $"Aera ID: {Id}, Position: ({X}, {Y}), Size: ({Width}x{Height})";
    }

    public bool ContainsPoint(int x, int y)
    {
        return x >= X && x <= X + Width && y >= Y && y <= Y + Height;
    }
}

public class AeraManager
{
    private List<Aera> aeras = new List<Aera>();

    public void AddAera(Aera aera)
    {
        aeras.Add(aera);
    }

    public void RemoveAera(string id)
    {
        var aera = aeras.FirstOrDefault(a => a.Id == id);
        if (aera != null)
        {
            aeras.Remove(aera);
        }
    }

    public void LoadAera()
    {
    }

    public Aera GetAeraFromPoint(int x, int y)
    {
        return aeras.FirstOrDefault(a => a.ContainsPoint(x, y));
    }

    public Aera GetAeraFromId(string id)
    {
        return aeras.FirstOrDefault(a => a.Id == id);
    }


    public override string ToString()
    {
        return string.Join("\n", aeras);
    }
}
