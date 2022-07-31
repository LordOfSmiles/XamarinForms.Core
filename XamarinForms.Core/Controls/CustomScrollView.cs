using System.Diagnostics;
using System.Threading;
using Xamarin.Forms;

namespace XamarinForms.Core.Controls;

public sealed class CustomScrollView:ScrollView
{
    public CustomScrollView()
    {
        Scrolled += OnScrolled;
    }

    private CancellationTokenSource _cts;

    private async void OnScrolled(object sender, ScrolledEventArgs e)
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();

        try
        {
            await Task.Delay(500, _cts.Token);

            _cts.Token.ThrowIfCancellationRequested();
            ScrollEnded?.Invoke(this, EventArgs.Empty);
        }
        catch
        {
            // ignored
        }
    }

    #region Events

    public event EventHandler ScrollEnded;

    #endregion
}