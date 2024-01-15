namespace XamarinForms.Core.Controls;

public class LightDarkIndicator : BorderOld
{
    #region Bindable Properties

    public static readonly BindableProperty LightDarkColorProperty = BindableProperty.Create(nameof(LightDarkColor),
                                                                                             typeof(LightDarkModel),
                                                                                             typeof(LightDarkIndicator));

    public LightDarkModel LightDarkColor
    {
        get => (LightDarkModel)GetValue(LightDarkColorProperty);
        set => SetValue(LightDarkColorProperty, value);
    }

    #endregion

    protected override void OnPropertyChanged(string propertyName = null)
    {
        if (propertyName == nameof(LightDarkColor))
        {
            this.SetAppThemeColor(BackgroundColorProperty, LightDarkColor.LightColor, LightDarkColor.DarkColor);
        }

        base.OnPropertyChanged(propertyName);
    }
}

public struct LightDarkModel
{
    public LightDarkModel(string light, string dark)
        : this(Color.FromHex(light), Color.FromHex(dark))
    { }

    public LightDarkModel(Color light, Color dark)
    {
        LightColor = light;
        DarkColor = dark;
    }

    public Color LightColor { get; }
    public Color DarkColor { get; }
}