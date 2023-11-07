using XamarinForms.Core.Controls.Layouts;
using XamarinForms.Core.Helpers;

namespace XamarinForms.Core.Controls.Buttons;

public abstract class CustomButtonBase : FrameWithTap
{
    #region Fields

    protected Label Lbl { get; }

    #endregion

    protected CustomButtonBase()
    {
        Lbl = new Label()
        {
            FontSize = FontSize,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            FontAttributes = DeviceHelper.OnPlatform(FontAttributes.Bold, FontAttributes.None),
            TextColor = TextColor
        };

        Content = Lbl;
    }

    #region Bindable Properties

    #region Text

    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
                                                                                   typeof(string),
                                                                                   typeof(CustomButtonBase));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    #endregion

    #region TextColor

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor),
                                                                                        typeof(Color),
                                                                                        typeof(CustomButtonBase));

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    #endregion

    #region FontSize

    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize),
                                                                                       typeof(double),
                                                                                       typeof(CustomButtonBase),
                                                                                       16.0);

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    #endregion

    #endregion

    protected override void OnPropertyChanged(string propertyName = null)
    {
        if (propertyName == nameof(Text))
        {
            Lbl.Text = Text;
        }
        else if (propertyName == nameof(TextColor))
        {
            Lbl.TextColor = TextColor;
        }
        else if (propertyName == nameof(FontSize))
        {
            Lbl.FontSize = FontSize;
        }

        base.OnPropertyChanged(propertyName);
    }
}