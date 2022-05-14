using Xamarin.Essentials;

namespace XamarinForms.Core.Helpers;

public static class VersionHelper
{
    public static bool IsEqualOrGreater(int major, int minor=0)
    {
        var result = false;
            
        var currentMajor = DeviceInfo.Version.Major;
            
        result = currentMajor > major;

        if (!result)
        {
            result = currentMajor == major && DeviceInfo.Version.Minor >= minor;
        }

        return result;
    }
}