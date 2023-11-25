namespace XamarinForms.Core.Controls.Renderers;

public sealed class ExtendedDatePicker:DatePicker
{
    public void RaiseDoneEvent()
    {
        DoneEvent?.Invoke(this, EventArgs.Empty);
    }

    public void RaiseClearEvent()
    {
        ClearEvent?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler DoneEvent;
    public event EventHandler ClearEvent;
    
    #region Properties

    public bool WithClear { get; set; }
    
    public static string ConfirmText { get; set; }
    public static string ClearText { get; set; }

    #endregion
}