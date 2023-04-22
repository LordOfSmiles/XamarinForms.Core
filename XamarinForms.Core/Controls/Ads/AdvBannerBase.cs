namespace XamarinForms.Core.Controls.Ads;

public abstract class AdvBannerBase : View
{
    protected AdvBannerBase(string adUnitId, Color colorLight, Color colorDark)
    {
        AdUnitId = adUnitId;
        
        this.SetAppThemeColor(BackgroundColorProperty, colorLight, colorDark);
    }

    public string AdUnitId { get; }
}