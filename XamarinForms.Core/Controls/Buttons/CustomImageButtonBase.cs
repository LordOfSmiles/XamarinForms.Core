using XamarinForms.Core.Controls.Layouts;

namespace XamarinForms.Core.Controls.Buttons;

public class CustomImageButtonBase : FrameWithTap
{
    private TintedImage Img { get; }

    protected CustomImageButtonBase()
    {
        Img = new TintedImage()
        {
            WidthRequest = ImageWidth,
            HeightRequest = ImageHeight,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };

        Content = Img;
    }

    #region Properties

    #region ImageSource

    public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source),
                                                                                     typeof(ImageSource),
                                                                                     typeof(CustomImageButtonBase));

    public ImageSource Source
    {
        get => (ImageSource)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    #endregion

    #region ImageWidth

    public static readonly BindableProperty ImageWidthProperty = BindableProperty.Create(nameof(ImageWidth),
                                                                                         typeof(double),
                                                                                         typeof(CustomImageButtonBase),
                                                                                         24.0);

    public double ImageWidth
    {
        get => (double)GetValue(ImageWidthProperty);
        set => SetValue(ImageWidthProperty, value);
    }

    #endregion

    #region ImageHeight

    public static readonly BindableProperty ImageHeightProperty = BindableProperty.Create(nameof(ImageHeight),
                                                                                          typeof(double),
                                                                                          typeof(CustomImageButtonBase),
                                                                                          24.0);

    public double ImageHeight
    {
        get => (double)GetValue(ImageHeightProperty);
        set => SetValue(ImageHeightProperty, value);
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
        switch (propertyName)
        {
            case nameof(Source):
                Img.Source = Source;
                break;
            case nameof(TintColor):
                Img.TintColor = TintColor;
                break;
            case nameof(ImageWidth):
                Img.WidthRequest = ImageWidth;
                break;
            case nameof(ImageHeight):
                Img.HeightRequest = ImageHeight;
                break;
        }

        base.OnPropertyChanged(propertyName);
    }
}