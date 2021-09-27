using Xamarin.Essentials;

namespace XamarinForms.Core.Helpers
{
    public static class VersionHelper
    {
        public static bool IsEqualOrGreater(int major, int minor)
        {
            var result = false;
            
            var currentMajor = DeviceInfo.Version.Major;
            var currentMinor = DeviceInfo.Version.Minor;

            result = major > currentMajor;

            if (!result)
            {
                result = major == currentMajor && minor >= currentMinor;
            }

            return result;
        }
    }
}