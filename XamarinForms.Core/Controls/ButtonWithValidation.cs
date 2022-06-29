using Xamarin.Forms;

namespace XamarinForms.Core.Controls;

public sealed class ButtonWithValidation:Button
{
    public ButtonWithValidation()
    {
        Style = InvalidStyle;
    }
    
    #region Bindable Properties

    #region IsValid

    public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid),
        typeof(bool),
        typeof(ButtonWithValidation), false,
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
        ctrl.Style = isValid
            ? ctrl.ValidStyle 
            : ctrl.InvalidStyle;
    }

    #endregion
    
    #region ValidStyle

    public static readonly BindableProperty ValidStyleProperty = BindableProperty.Create(nameof(ValidStyle),
        typeof(Style),
        typeof(ButtonWithValidation));

    public Style ValidStyle
    {
        get => (Style)GetValue(ValidStyleProperty);
        set => SetValue(ValidStyleProperty, value);
    }

    #endregion
    
    #region InvalidStyle

    public static readonly BindableProperty InvalidStyleProperty = BindableProperty.Create(nameof(InvalidStyle),
        typeof(Style),
        typeof(ButtonWithValidation));

    public Style InvalidStyle
    {
        get => (Style)GetValue(InvalidStyleProperty);
        set => SetValue(InvalidStyleProperty, value);
    }

    #endregion

    #endregion
}