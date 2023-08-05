using XamarinForms.Core.Controls.Layouts.Helpers;

namespace XamarinForms.Core.Controls.Layouts;

public class StackLayoutWithTap : StackLayout, ITouchableLayout
{
    protected StackLayoutWithTap()
    {
        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += (_, _) => TouchableLayoutHelper.ProcessTapAsync(this);
        GestureRecognizers.Add(tapGesture);
        
        BackgroundColor = NormalColor;
    }

    #region Bindable Properties

    #region NormalColor

    public static readonly BindableProperty NormalColorProperty = BindableProperty.Create(nameof(NormalColor),
                                                                                          typeof(Color),
                                                                                          typeof(StackLayoutWithTap),
                                                                                          Color.Transparent,
                                                                                          propertyChanged: OnDefaultColorChanged);

    public Color NormalColor
    {
        get => (Color)GetValue(NormalColorProperty);
        set => SetValue(NormalColorProperty, value);
    }

    private static void OnDefaultColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = (StackLayoutWithTap)bindable;

        ctrl.BackgroundColor = (Color)newValue;
    }

    #endregion

    #region Command

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command),
                                                                                      typeof(ICommand),
                                                                                      typeof(StackLayoutWithTap));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    #endregion

    #region CommandParameter

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter),
                                                                                               typeof(object),
                                                                                               typeof(StackLayoutWithTap));

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    #endregion

    #endregion

    #region ITouchableLayout

    public bool IsBusy { get; set; }

    #endregion
}