using Xamarin.Forms;

namespace XamarinForms.Core.Controls;

public sealed class ExtendedDatePicker:DatePicker
{
    public void RaiseDoneEvent()
    {
        DoneEvent?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler DoneEvent;
}