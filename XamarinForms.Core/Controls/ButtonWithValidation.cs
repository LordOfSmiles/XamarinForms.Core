using Xamarin.Forms;

namespace XamarinForms.Core.Controls;

public sealed class ButtonWithValidation : Button
{
    public ButtonWithValidation()
    {
        BackgroundColor = NormalColor;
        TextColor = NormalTextColor;
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
            ctrl.BackgroundColor = ctrl.NormalColor;
            ctrl.TextColor = ctrl.NormalTextColor;
        }
        else
        {
            ctrl.BackgroundColor = ctrl.InvalidColor;
            ctrl.TextColor = ctrl.InvalidTextColor;
        }
    }

    #endregion

    #region NormalColor

    public static readonly BindableProperty NormalColorProperty = BindableProperty.Create(nameof(NormalColor),
        typeof(Color),
        typeof(ButtonWithValidation));

    public Color NormalColor
    {
        get => (Color)GetValue(NormalColorProperty);
        set => SetValue(NormalColorProperty, value);
    }

    #endregion

    #region NormalTextColor

    public static readonly BindableProperty NormalTextColorProperty = BindableProperty.Create(nameof(NormalTextColor),
        typeof(Color),
        typeof(ButtonWithValidation));

    public Color NormalTextColor
    {
        get => (Color)GetValue(NormalTextColorProperty);
        set => SetValue(NormalTextColorProperty, value);
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