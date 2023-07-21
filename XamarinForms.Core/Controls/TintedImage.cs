namespace XamarinForms.Core.Controls;

public class TintedImage : Image
{
    #region TintColor

    public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor),
                                                                                        typeof(Color),
                                                                                        typeof(TintedImage));

    public Color TintColor
    {
        get => (Color)GetValue(TintColorProperty);
        set => SetValue(TintColorProperty, value);
    }

    #endregion

    protected override void OnPropertyChanged(string propertyName = null)
    {
        if (propertyName == nameof(TintColor))
        {
            IconTintColorEffect.SetTintColor(this, TintColor);
        }

        base.OnPropertyChanged(propertyName);
    }
}