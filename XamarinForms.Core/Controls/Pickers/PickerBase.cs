using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace XamarinForms.Core.Controls.Pickers;

public abstract class PickerBase : Grid
{
    #region Commands

    public ICommand OpenPickerCommand => new AsyncCommand(OnOpenPicker);

    protected virtual Task OnOpenPicker()
    {
        return Task.CompletedTask;
    }

    #endregion

    #region Bindable Proeprties

    #region Title

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title),
        typeof(string),
        typeof(PickerBase),
        string.Empty);

    public string Title
    {
        get => (string) GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    #endregion
        
    #region AcceptCommand

    public static readonly BindableProperty AcceptCommandProperty = BindableProperty.Create(nameof(AcceptCommand),
        typeof(ICommand),
        typeof(PickerBase));

    public ICommand AcceptCommand
    {
        get => (ICommand) GetValue(AcceptCommandProperty);
        set => SetValue(AcceptCommandProperty, value);
    }

    #endregion

    public static readonly BindableProperty ContentViewProperty = BindableProperty.Create(nameof(ContentView),
        typeof(View),
        typeof(PickerBase),
        propertyChanged: OnViewChanged);

    public View ContentView
    {
        get => (View) GetValue(ContentViewProperty);
        set => SetValue(ContentViewProperty, value);
    }

    private static void OnViewChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = (PickerBase) bindable;
            
        ctrl.UpdateControl();
    }
        
    #endregion
        

    #region Methods

    protected virtual void UpdateControl()
    {
    }

    #endregion
}