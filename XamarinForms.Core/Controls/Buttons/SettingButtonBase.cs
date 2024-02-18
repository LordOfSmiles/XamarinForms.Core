using XamarinForms.Core.Controls.Layouts;
using XamarinForms.Core.Helpers;

namespace XamarinForms.Core.Controls.Buttons;

public abstract class SettingButtonBase : FrameWithTap
{
    #region Fields

    protected readonly Grid Grd;
    
    #endregion
    
    protected SettingButtonBase()
    {
        Grd = new Grid();

        Content = Grd;
    }
    
    #region Bindable Properties

    #region Text

    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
                                                                                   typeof(string),
                                                                                   typeof(SettingButtonBase));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    #endregion

    #endregion
}