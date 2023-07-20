using XamarinForms.Core.Controls.Layouts;
using XamarinForms.Core.Helpers;

namespace XamarinForms.Core.Controls.Buttons;

public abstract class SettingsButtonBase : TouchableGrid
{
    #region Fields

    protected Label Lbl { get; }

    #endregion

    protected SettingsButtonBase()
    {
        Padding = new Thickness(12);

        Lbl = new Label()
        {
            VerticalOptions = LayoutOptions.Center,
            FontSize = FontSize
        };
    }

    #region Bindable Properties

    #region Text

    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
                                                                                   typeof(string),
                                                                                   typeof(SettingsButtonBase));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    #endregion

    #region FontSize

    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize),
                                                                                       typeof(double),
                                                                                       typeof(SettingsButtonBase),
                                                                                       FontHelper.CaptionFontSize);

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    #endregion

    #endregion

    protected override void OnPropertyChanged(string propertyName = null)
    {
        switch (propertyName)
        {
            case nameof(Text):
                Lbl.Text = Text;
                break;
            case nameof(FontSize):
                Lbl.FontSize = FontSize;
                break;
        }

        base.OnPropertyChanged(propertyName);
    }
}