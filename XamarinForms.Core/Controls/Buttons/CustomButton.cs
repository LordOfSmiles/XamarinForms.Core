using XamarinForms.Core.Controls.Layouts;
using XamarinForms.Core.Helpers;

namespace XamarinForms.Core.Controls.Buttons;

public class CustomButton : TouchableFrame
{
    #region Fields

    private readonly Label _lbl;

    #endregion

    public CustomButton()
    {
        CornerRadius = 12;

        if (DeviceHelper.IsIos)
        {
            Padding = new Thickness(16, 10);
        }
        else
        {
            Padding = new Thickness(16, 10);
        }


        _lbl = new Label()
        {
            FontSize = 16,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            FontAttributes = FontAttributes.Bold
        };

        if (DeviceHelper.IsAndroid)
        {
            _lbl.TextTransform = TextTransform.Uppercase;
        }

        Content = _lbl;
    }

    #region Bindable Properties

    #region Text

    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
                                                                                   typeof(string),
                                                                                   typeof(CustomButton));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    #endregion

    #region TextColor

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor),
                                                                                        typeof(Color),
                                                                                        typeof(CustomButton));

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    #endregion

    #endregion

    protected override void OnPropertyChanged(string propertyName = null)
    {
        if (propertyName == nameof(Text))
        {
            _lbl.Text = Text;
        }

        if (propertyName == nameof(TextColor))
        {
            _lbl.TextColor = TextColor;
        }

        base.OnPropertyChanged(propertyName);
    }
}