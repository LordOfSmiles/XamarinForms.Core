using XamarinForms.Core.Controls.Layouts.Helpers;

namespace XamarinForms.Core.Controls.Layouts;

public class TouchableGrid : Grid, ITouchableLayout
{
    public TouchableGrid()
    {
        BackgroundColor = NormalColor;

        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += (sender, _) => TouchableLayoutHelper.ProcessTapAsync(this, sender);
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

    #region ITouchableLayout

    public bool IsBusy { get; set; }

    public Task ColorTo(Color endColor, uint duration) => Extensions.ViewExtensions.ColorTo(this, endColor, duration);

    #endregion
}