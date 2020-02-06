using Xamarin.Forms;
using XamarinForms.Core.Standard.Infrastructure.Interfaces;
using XamarinForms.Core.Standard.Services;

namespace XamarinForms.Core.Standard.Helpers
{
    public static class FontHelper
    {
        public static double  LabelSmall => DeviceHelper.OnPlatform(14, Device.GetNamedSize(NamedSize.Small, typeof(Label)));
    }
}