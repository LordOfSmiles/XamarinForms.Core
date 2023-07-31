using XamarinForms.Core.Controls.Layouts;
using XamarinForms.Core.Helpers;

namespace XamarinForms.Core.Controls.Buttons;

public abstract class SettingsButtonBase : GridWithTap
{
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
}