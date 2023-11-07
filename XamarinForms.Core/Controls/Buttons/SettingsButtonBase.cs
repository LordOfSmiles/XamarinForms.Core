using XamarinForms.Core.Controls.Layouts;
using XamarinForms.Core.Helpers;

namespace XamarinForms.Core.Controls.Buttons;

public abstract class SettingsButtonBase : FrameWithTap
{
    #region Fields

    protected readonly Grid Grd;
    
    #endregion
    
    protected SettingsButtonBase()
    {
        Grd = new Grid();

        Content = Grd;
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
    
    #region ButtonType

    public static readonly BindableProperty ButtonTypeProperty = BindableProperty.Create(nameof(ButtonType),
                                                                                         typeof(SettingsButtonTypeEnum),
                                                                                         typeof(SettingsButtonBase),
                                                                                         SettingsButtonTypeEnum.Default);

    public SettingsButtonTypeEnum ButtonType
    {
        get => (SettingsButtonTypeEnum)GetValue(ButtonTypeProperty);
        set => SetValue(ButtonTypeProperty, value);
    }

    #endregion

    #endregion
}

public enum SettingsButtonTypeEnum
{
    Default,
    Navigation
}