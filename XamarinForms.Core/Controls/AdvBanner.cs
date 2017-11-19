using Xamarin.Forms;

namespace XamarinForms.Core.Controls
{
    public sealed class AdvBanner : View
    {
        public AdvBanner(string adUnitId)
        {
            AdUnitId = adUnitId;
        }

        public string AdUnitId { get; set; }
    }
}

