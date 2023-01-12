using Xamarin.CommunityToolkit.Extensions;
using XamarinForms.Core.Extensions;
using XamarinForms.Core.Helpers;

namespace XamarinForms.Core.Controls;

public class TouchableGrid : Grid
{
    public TouchableGrid()
    {
        BackgroundColor = NormalColor;

        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += TapGestureRecognizer_OnTapped;
        GestureRecognizers.Add(tapGesture);
    }

    #region Bindable Properties

    #region NormalColor

    public static readonly BindableProperty NormalColorProperty = BindableProperty.Create(nameof(NormalColor),
                                                                                          typeof(Color),
                                                                                          typeof(TouchableGrid),
                                                                                          propertyChanged: OnDefaultColorChanged);

    public Color NormalColor
    {
        get => (Color)GetValue(NormalColorProperty);
        set => SetValue(NormalColorProperty, value);
    }

    private static void OnDefaultColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = (TouchableGrid)bindable;

        ctrl.BackgroundColor = (Color)newValue;
    }

    #endregion

    #region Command

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command),
                                                                                      typeof(ICommand),
                                                                                      typeof(TouchableGrid));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    #endregion

    #region CommandParameter

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter),
                                                                                               typeof(object),
                                                                                               typeof(TouchableGrid));

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
        await this.ColorTo(ColorHelper.CalculatePressedColor(NormalColor), 150);
        await Task.Delay(25);
        await this.ColorTo(NormalColor, 50);

        Command?.Execute(CommandParameter);
    }

    #endregion
}