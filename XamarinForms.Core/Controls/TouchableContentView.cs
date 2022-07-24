using Xamarin.CommunityToolkit.Effects;
using Xamarin.Forms;
using XamarinForms.Core.Helpers;

namespace XamarinForms.Core.Controls;

public class TouchableContentView : ContentView
{
    public TouchableContentView()
    {
        TouchEffect.SetNativeAnimation(this, true);
        TouchEffect.SetPulseCount(this, DeviceHelper.OnPlatform(0, 1));
    }

    #region Bindable Properties

    #region PressedColor

    public static readonly BindableProperty PressedColorProperty = BindableProperty.Create(nameof(PressedColor),
        typeof(Color),
        typeof(TouchableContentView),
        Color.Default,
        propertyChanged: OnPressedColorChanged);

    public Color PressedColor
    {
        get => (Color)GetValue(PressedColorProperty);
        set => SetValue(PressedColorProperty, value);
    }

    private static void OnPressedColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = (TouchableContentView)bindable;

        var color = (Color)newValue;
        TouchEffect.SetNativeAnimationColor(ctrl, color);
    }

    #endregion

    #endregion


    protected override void OnPropertyChanged(string propertyName = null)
    {
        if (propertyName == BackgroundColorProperty.PropertyName)
        {
            TouchEffect.SetNormalBackgroundColor(this, BackgroundColor);
        }

        base.OnPropertyChanged(propertyName);
    }
}

public class TouchableGrid : Grid
{
    public TouchableGrid()
    {
        TouchEffect.SetNativeAnimation(this, true);
        TouchEffect.SetPulseCount(this, DeviceHelper.OnPlatform(0, 1));
    }

    #region Bindable Properties

    #region PressedColor

    public static readonly BindableProperty PressedColorProperty = BindableProperty.Create(nameof(PressedColor),
        typeof(Color),
        typeof(TouchableGrid),
        Color.Default,
        propertyChanged: OnPressedColorChanged);

    public Color PressedColor
    {
        get => (Color)GetValue(PressedColorProperty);
        set => SetValue(PressedColorProperty, value);
    }

    private static void OnPressedColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = (TouchableGrid)bindable;

        var color = (Color)newValue;
        TouchEffect.SetNativeAnimationColor(ctrl, color);
    }

    #endregion

    #endregion


    protected override void OnPropertyChanged(string propertyName = null)
    {
        if (propertyName == BackgroundColorProperty.PropertyName)
        {
            //TouchEffect.SetNormalBackgroundColor(this, BackgroundColor);
        }

        base.OnPropertyChanged(propertyName);
    }
}

public class TouchableStackLayout : StackLayout
{
    public TouchableStackLayout()
    {
        TouchEffect.SetNativeAnimation(this, true);
        TouchEffect.SetPulseCount(this, DeviceHelper.OnPlatform(0, 1));
    }

    #region Bindable Properties

    #region PressedColor

    public static readonly BindableProperty PressedColorProperty = BindableProperty.Create(nameof(PressedColor),
        typeof(Color),
        typeof(TouchableStackLayout),
        Color.Default,
        propertyChanged: OnPressedColorChanged);

    public Color PressedColor
    {
        get => (Color)GetValue(PressedColorProperty);
        set => SetValue(PressedColorProperty, value);
    }

    private static void OnPressedColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = (TouchableStackLayout)bindable;

        var color = (Color)newValue;
        TouchEffect.SetNativeAnimationColor(ctrl, color);
    }

    #endregion

    #endregion


    protected override void OnPropertyChanged(string propertyName = null)
    {
        if (propertyName == BackgroundColorProperty.PropertyName)
        {
            TouchEffect.SetNormalBackgroundColor(this, BackgroundColor);
        }

        base.OnPropertyChanged(propertyName);
    }
}