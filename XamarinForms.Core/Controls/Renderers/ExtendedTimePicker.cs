namespace XamarinForms.Core.Controls.Renderers;

public sealed class ExtendedTimePicker:Xamarin.Forms.TimePicker
{
    public void RaiseDoneEvent()
    {
        DoneEvent?.Invoke(this, EventArgs.Empty);
    }

    public void RaiseCancelEvent()
    {
        CancelEvent?.Invoke(this, EventArgs.Empty);
    }
    
    public event EventHandler DoneEvent;
    public event EventHandler CancelEvent;
    
}