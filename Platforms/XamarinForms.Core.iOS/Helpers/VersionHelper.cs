using UIKit;

namespace XamarinForms.Core.iOS.Helpers
{
    public static class VersionHelper
    {
        public static bool IsOs13OrHigher => UIDevice.CurrentDevice.CheckSystemVersion(13, 0);
        public static bool IsOs14_4_OrHigher => UIDevice.CurrentDevice.CheckSystemVersion(14, 4);
        public static bool IsOs14_5_OrHigher => UIDevice.CurrentDevice.CheckSystemVersion(14, 5);

        public static bool IsVersionEqualsOrHigher(int major, int minor) => UIDevice.CurrentDevice.CheckSystemVersion(major, minor);
    }
}