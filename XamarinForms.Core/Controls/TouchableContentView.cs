using Xamarin.CommunityToolkit.Extensions;
using XamarinForms.Core.Extensions;
using XamarinForms.Core.Helpers;

namespace XamarinForms.Core.Controls;

public class TouchableContentView : ContentView
{
    #region Fields

    protected bool IsBusy;
    
    #endregion
    
    public TouchableContentView()
    {
        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += TapGestureRecognizer_OnTapped;
        GestureRecognizers.Add(tapGesture);
    }

    #region Bindable Properties

    #region NormalColor

    public static readonly BindableProperty NormalColorProperty = BindableProperty.Create(nameof(NormalColor),
                                                                                          typeof(Color),
                                                                                          typeof(TouchableContentView),
                                                                                          propertyChanged: OnDefaultColorChanged);

    public Color NormalColor
    {
        get => (Color)GetValue(NormalColorProperty);
        set => SetValue(NormalColorProperty, value);
    }

    private static void OnDefaultColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = (TouchableContentView)bindable;

        ctrl.BackgroundColor = (Color)newValue;
    }

    #endregion

    #region Command

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command),
                                                                                      typeof(ICommand),
                                                                                      typeof(TouchableContentView));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    #endregion

    #region CommandParameter

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter),
                                                                                               typeof(object),
                                                                                               typeof(TouchableContentView));

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    #endregion

    #endregion

    #region Handlers

    private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
    {
        if (IsBusy)
            return;

        IsBusy = true;
        
        var startColor = NormalColor.IsTransparent()
                             ? ColorHelper.FindParentRealColor((View)sender)
                             : NormalColor;

        await this.ColorTo(ColorHelper.CalculatePressedColor(startColor), 150);
        await Task.Delay(25);

        if (NormalColor.IsTransparent())
        {
            await this.ColorTo(startColor, 50);
            BackgroundColor = NormalColor;
        }
        else
        {
            await this.ColorTo(NormalColor, 50);
        }
        
        Command?.Execute(CommandParameter);

        IsBusy = false;
    }

    #endregion
}