using Xamarin.Forms.Xaml;

namespace XamarinForms.Core.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ButtonWithState
{
    public ButtonWithState()
    {
        InitializeComponent();
    }

    #region Bindable Properties

    #region Text

    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
                                                                                   typeof(string),
                                                                                   typeof(ButtonWithState));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    #endregion

    #region IsValid

    public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid),
                                                                                      typeof(bool),
                                                                                      typeof(ButtonWithState),
                                                                                      true,
                                                                                      propertyChanged: OnIsValidChanged);

    public bool IsValid
    {
        get => (bool)GetValue(IsValidProperty);
        set => SetValue(IsValidProperty, value);
    }

    private static void OnIsValidChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = (ButtonWithState)bindable;

        var isValid = (bool)newValue;
        if (isValid)
        {
            ctrl.NormalColor = ctrl.DefaultColor;
            ctrl.lbl.TextColor = ctrl.DefaultTextColor;
            ctrl.PressedColor = ctrl.DefaultPressedColor;
        }
        else
        {
            ctrl.NormalColor = ctrl.ErrorColor;
            ctrl.lbl.TextColor = ctrl.ErrorTextColor;
            ctrl.PressedColor = ctrl.ErrorPressedColor;
        }
    }

    #endregion

    #region NormalColor

    public static readonly BindableProperty DefaultColorProperty = BindableProperty.Create(nameof(DefaultColor),
                                                                                           typeof(Color),
                                                                                           typeof(ButtonWithState));

    public Color DefaultColor
    {
        get => (Color)GetValue(DefaultColorProperty);
        set => SetValue(DefaultColorProperty, value);
    }

    #endregion

    #region NormalTextColor

    public static readonly BindableProperty DefaultTextColorProperty = BindableProperty.Create(nameof(DefaultTextColor),
                                                                                               typeof(Color),
                                                                                               typeof(ButtonWithState));

    public Color DefaultTextColor
    {
        get => (Color)GetValue(DefaultTextColorProperty);
        set => SetValue(DefaultTextColorProperty, value);
    }

    #endregion

    #region DefaultPressedColor

    public static readonly BindableProperty DefaultPressedColorProperty = BindableProperty.Create(nameof(DefaultPressedColor),
                                                                                                  typeof(Color),
                                                                                                  typeof(ButtonWithState));

    public Color DefaultPressedColor
    {
        get => (Color)GetValue(DefaultColorProperty);
        set => SetValue(DefaultColorProperty, value);
    }

    #endregion

    #region InvalidColor

    public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(nameof(ErrorColor),
                                                                                         typeof(Color),
                                                                                         typeof(ButtonWithState));

    public Color ErrorColor
    {
        get => (Color)GetValue(ErrorColorProperty);
        set => SetValue(ErrorColorProperty, value);
    }

    #endregion

    #region InvalidTextColor

    public static readonly BindableProperty ErrorTextColorProperty = BindableProperty.Create(nameof(ErrorTextColor),
                                                                                             typeof(Color),
                                                                                             typeof(ButtonWithState));

    public Color ErrorTextColor
    {
        get => (Color)GetValue(ErrorTextColorProperty);
        set => SetValue(ErrorTextColorProperty, value);
    }

    #endregion

    #region ErrorPressedColor

    public static readonly BindableProperty ErrorPressedColorProperty = BindableProperty.Create(nameof(ErrorPressedColor),
                                                                                                typeof(Color),
                                                                                                typeof(ButtonWithState));

    public Color ErrorPressedColor
    {
        get => (Color)GetValue(ErrorPressedColorProperty);
        set => SetValue(ErrorPressedColorProperty, value);
    }

    #endregion

    #endregion
}