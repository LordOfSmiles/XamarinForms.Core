using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace XamarinForms.Core.Controls.Pickers;

public abstract class PickerBase : TouchableGrid
{
    protected PickerBase()
    {
        var gesture = new TapGestureRecognizer()
        {
            Command = OpenPickerCommand
        };

        GestureRecognizers.Add(gesture);
    }

    #region Commands

    public ICommand OpenPickerCommand => new Command(OnOpenPicker);

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
}