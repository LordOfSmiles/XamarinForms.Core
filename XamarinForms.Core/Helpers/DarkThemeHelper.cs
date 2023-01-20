namespace XamarinForms.Core.Helpers;

public static class DarkThemeHelper
{
    public static bool IsSystemSupportedDarkMode => VersionHelper.IsEqualOrGreater(DeviceHelper.OnPlatform(13, 10));
}