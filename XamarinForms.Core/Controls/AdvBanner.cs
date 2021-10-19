using Xamarin.Forms;

namespace XamarinForms.Core.Controls
{
    public sealed class AdvBanner : View
    {
        public AdvBanner(string adUnitId, Color colorLight, Color colorDark)
        {
            #if DEBUG

            //adUnitId = "ca-app-pub-3940256099942544/6300978111";
            
            #endif
            
            AdUnitId = adUnitId;

            this.SetAppThemeColor(BackgroundColorProperty, colorLight, colorDark);
        }

        public string AdUnitId { get; }
    }
}

