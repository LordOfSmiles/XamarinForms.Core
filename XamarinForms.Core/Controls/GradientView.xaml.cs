using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinForms.Core.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class GradientView 
{
    public GradientView()
    {
        InitializeComponent();
    }
        
    #region Bindable Proeprties
        
    #region StartColor

    public static readonly BindableProperty StartColorProperty = BindableProperty.Create(nameof(StartColor),
        typeof(Color),
        typeof(GradientView),
        Color.Default);

    public Color StartColor
    {
        get => (Color) GetValue(StartColorProperty);
        set => SetValue(StartColorProperty, value);
    }

    #endregion
        
    #region EndColor

    public static readonly BindableProperty EndColorProperty = BindableProperty.Create(nameof(EndColor),
        typeof(Color),
        typeof(GradientView),
        Color.Default);

    public Color EndColor
    {
        get => (Color) GetValue(EndColorProperty);
        set => SetValue(EndColorProperty, value);
    }

    #endregion
        
    #endregion
}