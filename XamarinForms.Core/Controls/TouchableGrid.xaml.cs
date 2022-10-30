using Xamarin.Forms.Xaml;

namespace XamarinForms.Core.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class TouchableGrid
{
    public TouchableGrid()
    {
        InitializeComponent();
    }

    #region Bindable Properties

    #region PressedColor

    public static readonly BindableProperty PressedColorProperty = BindableProperty.Create(nameof(PressedColor),
        typeof(Color),
        typeof(TouchableGrid),
        Color.Accent);

    public Color PressedColor
    {
        get => (Color)GetValue(PressedColorProperty);
        set => SetValue(PressedColorProperty, value);
    }

    #endregion

    #region NormalColor

    public static readonly BindableProperty NormalColorProperty = BindableProperty.Create(nameof(NormalColor),
        typeof(Color),
        typeof(TouchableGrid),
        Color.Transparent);

    public Color NormalColor
    {
        get => (Color)GetValue(NormalColorProperty);
        set => SetValue(NormalColorProperty, value);
    }

    #endregion

    #endregion
}