using Xamarin.Forms;
using XamarinForms.Core.Helpers;

namespace XamarinForms.Core.Controls;

public sealed class HintControl:Grid
{
    #region Fields

    private readonly Label _lblDefault;
    private readonly Label _lblError;

    #endregion
        
    public HintControl()
    {
        Padding = new Thickness(16, 2, 8, 2);

        _lblDefault = new Label
        {
            TextColor = Color.Gray,
            FontSize = DeviceHelper.OnPlatform(10, 12),
            Text=" "
        };
        Children.Add(_lblDefault);

        _lblError = new Label
        {
            TextColor = Color.Red,
            FontSize = DeviceHelper.OnPlatform(10, 12),
            Opacity = 0,
            Text = " "
        };
        Children.Add(_lblError);
    }
        
    #region Bindable properties
        
    #region DefaultText

    public static readonly BindableProperty DefaultTextProperty = BindableProperty.Create(nameof(DefaultText),
        typeof(string),
        typeof(HintControl),
        " ",
        propertyChanged: OnDefaultTextChanged);

    public string DefaultText
    {
        get => (string) GetValue(DefaultTextProperty);
        set => SetValue(DefaultTextProperty, value);
    }

    private static void OnDefaultTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = bindable as HintControl;
        if (ctrl == null)
            return;

        ctrl._lblDefault.Text = newValue?.ToString() ?? string.Empty;
    }

    #endregion
        
    #region ErrorText

    public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(nameof(ErrorText),
        typeof(string),
        typeof(HintControl),
        " ",
        propertyChanged: OnErrorTextChanged);
        
    public string ErrorText
    {
        get => (string) GetValue(ErrorTextProperty);
        set => SetValue(ErrorTextProperty, value);
    }
        
    private static void OnErrorTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = bindable as HintControl;
        if (ctrl == null)
            return;

        ctrl._lblError.Text = newValue?.ToString() ?? string.Empty;
    }
        
    #endregion
        
    #region HasValidationErrors

    public static readonly BindableProperty HasValidationErrorsProperty = BindableProperty.Create(
        nameof(HasValidationErrors),
        typeof(bool),
        typeof(HintControl),
        false,
        propertyChanged: OnHasValidationErrorsChanged);

    public bool HasValidationErrors
    {
        get => (bool) GetValue(HasValidationErrorsProperty);
        set => SetValue(HasValidationErrorsProperty, value);
    }

    private static void OnHasValidationErrorsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = bindable as HintControl;
        if (ctrl == null)
            return;

        var hasErrors = (bool) newValue;
        if (hasErrors)
        {
            ctrl._lblDefault.Opacity = 0;
            ctrl._lblError.Opacity = 1;
        }
        else
        {
            ctrl._lblDefault.Opacity = 1;
            ctrl._lblError.Opacity = 0;
        }
    }

    #endregion

    #endregion
}