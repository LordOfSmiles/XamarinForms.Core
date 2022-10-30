using Xamarin.Essentials;

namespace XamarinForms.Core.Helpers;

public static class VersionHelper
{
    public static bool IsEqualOrGreater(int major, int minor=0)
    {
        var currentMajor = DeviceInfo.Version.Major;
            
        var result = currentMajor > major;
        if (!result)
        {
            result = currentMajor == major && DeviceInfo.Version.Minor >= minor;
        }

        return result;
    }
    
    public static bool IsEqualOrLess(int major, int minor=0)
    {
        var currentMajor = DeviceInfo.Version.Major;
            
        var result = currentMajor < major;

        if (!result)
        {
            result = currentMajor == major && DeviceInfo.Version.Minor <= minor;
        }

        return result;
    }
    
    public static bool IsLess(int major, int minor=0)
    {
        var currentMajor = DeviceInfo.Version.Major;
            
        var result = currentMajor < major;

        if (!result)
        {
            result = currentMajor < major && DeviceInfo.Version.Minor < minor;
        }

        return result;
    }
}