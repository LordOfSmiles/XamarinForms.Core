using UIKit;
using Xamarin.Forms;

namespace XamarinForms.Core.iOS.Helpers
{
    public static class VersionHelper
    {
        public static bool IsOs13OrHigher => UIDevice.CurrentDevice.CheckSystemVersion(13, 0);
    }
}