using Xamarin.Forms.Xaml;

namespace XamarinForms.Core.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LightDarkIndicator
{
    public LightDarkIndicator()
    {
        InitializeComponent();
    }

    #region Bindable Properties

    public static readonly BindableProperty LightDarkColorProperty = BindableProperty.Create(nameof(LightDarkColor),
                                                                                             typeof(LightDarkIndicatorModel),
                                                                                             typeof(LightDarkIndicator),
                                                                                             propertyChanged: OnLightDarkIndicatorChanged);

    public LightDarkIndicatorModel LightDarkColor
    {
        get => (LightDarkIndicatorModel)GetValue(LightDarkColorProperty);
        set => SetValue(LightDarkColorProperty, value);
    }

    private static void OnLightDarkIndicatorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = (LightDarkIndicator)bindable;

        var value = (LightDarkIndicatorModel)newValue;
        if (!string.IsNullOrEmpty(value.LightColorString)
            && !string.IsNullOrEmpty(value.DarkColorString))
        {
            var lightColor = Color.FromHex(value.LightColorString);
            var darkColor = Color.FromHex(value.DarkColorString);

            ctrl.SetAppThemeColor(BackgroundColorProperty, lightColor, darkColor);
        }
    }

    #endregion
}

public struct LightDarkIndicatorModel
{
    public LightDarkIndicatorModel(string light, string dark)
    {
        LightColorString = light;
        DarkColorString = dark;
    }

    public string LightColorString { get; set; }

    public string DarkColorString { get; set; }
}