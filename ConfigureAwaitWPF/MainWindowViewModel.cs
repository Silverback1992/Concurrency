using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Concurrency.ConfigureAwait;
using Concurrency.Deadlock;
using System.Diagnostics;
using System.Windows;

namespace ConfigureAwaitWPF;

public class MainWindowViewModel : ObservableRecipient
{
    public IAsyncRelayCommand BadCommand { get; set; }
    public IAsyncRelayCommand NiceCommand { get; set; }
    public RelayCommand DeadlockCommand { get; set; }

    public MainWindowViewModel()
    {
        BadCommand = new AsyncRelayCommand(DoBadWorkCommand);
        NiceCommand = new AsyncRelayCommand(DoNiceWorkCommand);
        DeadlockCommand = new RelayCommand(DoDeadlockCommand);
    }

    private void DoDeadlockCommand()
    {
        Debug.Print("DoDeadlockCommand method is doing work.");
        Debug.Print($"UI Thread Id: {Environment.CurrentManagedThreadId}");

        if (Application.Current.Dispatcher.CheckAccess())
        {
            Debug.Print("I'm a UI Thread.");
        }

        Concurrency.Deadlock.Lock.Deadlock();
    }

    private async Task DoBadWorkCommand()
    {
        Debug.Print("DoBadWorkCommand method is doing work.");
        Debug.Print($"UI Thread Id: {Environment.CurrentManagedThreadId}");

        if (Application.Current.Dispatcher.CheckAccess())
        {
            Debug.Print("I'm a UI Thread.");
        }

        await ConfigureAwaitBadExample.DoWorkAsync();
    }

    private async Task DoNiceWorkCommand()
    {
        Debug.Print("DoNiceWorkCommand method is doing work.");
        Debug.Print($"UI Thread Id: {Environment.CurrentManagedThreadId}");

        if (Application.Current.Dispatcher.CheckAccess())
        {
            Debug.Print("I'm a UI Thread.");
        }

        await ConfigureAwaitGoodExample.DoWorkAsync();
    }
}
