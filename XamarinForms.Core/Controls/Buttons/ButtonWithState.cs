namespace XamarinForms.Core.Controls.Buttons;

public sealed class ButtonWithState : CustomButtonBase
{
    public ButtonWithState()
    {
        CornerRadius = new CornerRadius(12);

        Padding = new Thickness(16, 10);

        NormalColor = ValidColor;
        TextColor = ValidTextColor;
    }

    #region Bindable Properties

    #region IsValid

    public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid),
                                                                                      typeof(bool),
                                                                                      typeof(ButtonWithState),
                                                                                      true);

    public bool IsValid
    {
        get => (bool)GetValue(IsValidProperty);
        set => SetValue(IsValidProperty, value);
    }

    #endregion

    #region NormalColor

    public static readonly BindableProperty ValidColorProperty = BindableProperty.Create(nameof(ValidColor),
                                                                                         typeof(Color),
                                                                                         typeof(ButtonWithState));

    public Color ValidColor
    {
        get => (Color)GetValue(ValidColorProperty);
        set => SetValue(ValidColorProperty, value);
    }

    #endregion

    #region NormalTextColor

    public static readonly BindableProperty ValidTextColorProperty = BindableProperty.Create(nameof(ValidTextColor),
                                                                                             typeof(Color),
                                                                                             typeof(ButtonWithState));

    public Color ValidTextColor
    {
        get => (Color)GetValue(ValidTextColorProperty);
        set => SetValue(ValidTextColorProperty, value);
    }

    #endregion

    #region InvalidColor

    public static readonly BindableProperty InvalidColorProperty = BindableProperty.Create(nameof(InvalidColor),
                                                                                           typeof(Color),
                                                                                           typeof(ButtonWithState));

    public Color InvalidColor
    {
        get => (Color)GetValue(InvalidColorProperty);
        set => SetValue(InvalidColorProperty, value);
    }

    #endregion

    #region InvalidTextColor

    public static readonly BindableProperty InvalidTextColorProperty = BindableProperty.Create(nameof(InvalidTextColor),
                                                                                               typeof(Color),
                                                                                               typeof(ButtonWithState));

    public Color InvalidTextColor
    {
        get => (Color)GetValue(InvalidTextColorProperty);
        set => SetValue(InvalidTextColorProperty, value);
    }

    #endregion

    #endregion

    protected override void OnPropertyChanged(string propertyName = null)
    {
        if (propertyName == nameof(IsValid))
        {
            if (IsValid)
            {
                NormalColor = ValidColor;
                TextColor = ValidTextColor;
            }
            else
            {
                NormalColor = InvalidColor;
                TextColor = InvalidTextColor;
            }
        }

        base.OnPropertyChanged(propertyName);
    }
}