namespace XamarinForms.Core.Controls;

public sealed class ExtendedTimePicker:Xamarin.Forms.TimePicker
{
    public void RaiseDoneEvent()
    {
        DoneEvent?.Invoke(this, EventArgs.Empty);
    }
    
    public event EventHandler DoneEvent;
}