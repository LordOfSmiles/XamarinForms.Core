using Xamarin.Forms;

namespace XamarinForms.Core.Controls
{
    public sealed class AdvBanner : View
    {
        public AdvBanner(string adUnitId, Color colorLight, Color colorDark)
        {
            AdUnitId = adUnitId;

            this.SetAppThemeColor(View.BackgroundColorProperty, colorLight, colorDark);
        }

        public string AdUnitId { get; set; }
    }
}

