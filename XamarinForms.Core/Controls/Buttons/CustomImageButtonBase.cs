using XamarinForms.Core.Controls.Layouts;

namespace XamarinForms.Core.Controls.Buttons;

public class CustomImageButtonBase : TouchableFrame
{
    protected Image Img { get; }

    protected CustomImageButtonBase()
    {
        Img = new Image()
        {
            WidthRequest = 24,
            HeightRequest = 24,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };

        Content = Img;
    }

    #region Properties

    #region ImageSource

    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(Source),
                                                                                          typeof(ImageSource),
                                                                                          typeof(CustomImageButtonBase));

    public ImageSource Source
    {
        get => (ImageSource)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    #endregion

    #region TintColor

    public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor),
                                                                                        typeof(Color),
                                                                                        typeof(CustomImageButtonBase));

    public Color TintColor
    {
        get => (Color)GetValue(TintColorProperty);
        set => SetValue(TintColorProperty, value);
    }

    #endregion

    #endregion

    protected override void OnPropertyChanged(string propertyName = null)
    {
        if (propertyName == nameof(Source))
        {
            Img.Source = Source;
        }
        else if (propertyName == nameof(TintColor))
        {
            IconTintColorEffect.SetTintColor(Img, TintColor);
        }

        base.OnPropertyChanged(propertyName);
    }
}