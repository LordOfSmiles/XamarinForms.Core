using Xamarin.Forms;

namespace XamarinForms.Core.Helpers;

public static class FontHelper
{
    public static double  LabelSmall => DeviceHelper.OnPlatform(14, Device.GetNamedSize(NamedSize.Small, typeof(Label)));
}