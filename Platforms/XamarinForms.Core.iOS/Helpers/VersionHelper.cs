using UIKit;
using Xamarin.Forms;

namespace XamarinForms.Core.iOS.Helpers
{
    public static class VersionHelper
    {
        public static bool IsOs13OrHigher => UIDevice.CurrentDevice.CheckSystemVersion(13, 0);
        public static bool IsOs14_5_OrHigher => UIDevice.CurrentDevice.CheckSystemVersion(14, 5);
    }
}