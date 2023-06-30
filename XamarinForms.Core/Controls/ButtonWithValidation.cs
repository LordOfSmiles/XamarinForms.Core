using XamarinForms.Core.Controls.Buttons;

namespace XamarinForms.Core.Controls;

public sealed class ButtonWithValidation : CustomButton
{
    public ButtonWithValidation()
    {
        NormalColor = ValidColor;
        TextColor = ValidTextColor;
    }

    #region Bindable Properties

    #region IsValid

    public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid),
                                                                                      typeof(bool),
                                                                                      typeof(ButtonWithValidation),
                                                                                      true,
                                                                                      propertyChanged: OnIsValidChanged);

    public bool IsValid
    {
        get => (bool)GetValue(IsValidProperty);
        set => SetValue(IsValidProperty, value);
    }

    private static void OnIsValidChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = (ButtonWithValidation)bindable;

        var isValid = (bool)newValue;
        if (isValid)
        {
            ctrl.NormalColor = ctrl.ValidColor;
            ctrl.TextColor = ctrl.ValidTextColor;
        }
        else
        {
            ctrl.NormalColor = ctrl.InvalidColor;
            ctrl.TextColor = ctrl.InvalidTextColor;
        }
    }

    #endregion

    #region NormalColor

    public static readonly BindableProperty ValidColorProperty = BindableProperty.Create(nameof(ValidColor),
                                                                                         typeof(Color),
                                                                                         typeof(ButtonWithValidation));

    public Color ValidColor
    {
        get => (Color)GetValue(ValidColorProperty);
        set => SetValue(ValidColorProperty, value);
    }

    #endregion

    #region NormalTextColor

    public static readonly BindableProperty ValidTextColorProperty = BindableProperty.Create(nameof(ValidTextColor),
                                                                                              typeof(Color),
                                                                                              typeof(ButtonWithValidation));

    public Color ValidTextColor
    {
        get => (Color)GetValue(ValidTextColorProperty);
        set => SetValue(ValidTextColorProperty, value);
    }

    #endregion

    #region InvalidColor

    public static readonly BindableProperty InvalidColorProperty = BindableProperty.Create(nameof(InvalidColor),
                                                                                           typeof(Color),
                                                                                           typeof(ButtonWithValidation));

    public Color InvalidColor
    {
        get => (Color)GetValue(InvalidColorProperty);
        set => SetValue(InvalidColorProperty, value);
    }

    #endregion

    #region InvalidTextColor

    public static readonly BindableProperty InvalidTextColorProperty = BindableProperty.Create(nameof(InvalidTextColor),
                                                                                               typeof(Color),
                                                                                               typeof(ButtonWithValidation));

    public Color InvalidTextColor
    {
        get => (Color)GetValue(InvalidTextColorProperty);
        set => SetValue(InvalidTextColorProperty, value);
    }

    #endregion

    #endregion
}