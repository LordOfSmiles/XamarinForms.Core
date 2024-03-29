using XamarinForms.Core.Controls.Layouts;
using XamarinForms.Core.Controls.Renderers;
using XamarinForms.Core.Helpers;

namespace XamarinForms.Core.Controls.Buttons;

public abstract class CustomButtonBase : FrameWithTap
{
    #region Fields

    protected CustomLabel Lbl { get; }

    #endregion

    protected CustomButtonBase()
    {
        Lbl = new CustomLabel()
        {
            FontSize = FontHelper.ButtonTextSize,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            FontWeight = FontWeight,
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
    
    #region FontWeight

    public static readonly BindableProperty FontWeightProperty = BindableProperty.Create(nameof(FontWeight),
                                                                                         typeof(FontWeightTypeEnum),
                                                                                         typeof(CustomButtonBase),
                                                                                         FontWeightTypeEnum.Medium);

    public FontWeightTypeEnum FontWeight
    {
        get => (FontWeightTypeEnum)GetValue(FontWeightProperty);
        set => SetValue(FontWeightProperty, value);
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
        else if (propertyName == nameof(FontWeight))
        {
            Lbl.FontWeight = FontWeight;
        }

        base.OnPropertyChanged(propertyName);
    }
}