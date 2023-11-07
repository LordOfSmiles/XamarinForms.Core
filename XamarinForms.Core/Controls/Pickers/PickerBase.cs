using XamarinForms.Core.Controls.Layouts;
using XamarinForms.Core.Helpers;

namespace XamarinForms.Core.Controls.Pickers;

public abstract class PickerBase : FrameWithTap
{
    public void InvokeFocusedEvent()
    {
        FocusedEvent?.Invoke(this, EventArgs.Empty);
    }
    
    public void InvokeUnfocusedEvent()
    {
        UnfocusedEvent?.Invoke(this, EventArgs.Empty);
    }
    
    protected PickerBase()
    {
        Command = OpenPickerCommand;
    }

    #region Commands

    public ICommand OpenPickerCommand => CommandHelper.Create(OnOpenPicker);

    protected virtual void OnOpenPicker()
    {
    }

    #endregion

    #region Bindable Proeprties

    #region AcceptCommand

    public static readonly BindableProperty AcceptCommandProperty = BindableProperty.Create(nameof(AcceptCommand),
        typeof(ICommand),
        typeof(PickerBase));

    public ICommand AcceptCommand
    {
        get => (ICommand)GetValue(AcceptCommandProperty);
        set => SetValue(AcceptCommandProperty, value);
    }

    #endregion

    #region ContentView

    public static readonly BindableProperty ContentViewProperty = BindableProperty.Create(nameof(ContentView),
        typeof(View),
        typeof(PickerBase));

    public View ContentView
    {
        get => (View)GetValue(ContentViewProperty);
        set => SetValue(ContentViewProperty, value);
    }

    #endregion

    #endregion
    
    #region Events

    public event EventHandler FocusedEvent;
    public event EventHandler UnfocusedEvent;
    
    #endregion
}