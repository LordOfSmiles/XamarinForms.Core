namespace XamarinForms.Core.Helpers;

public static class ThemeHelper
{
    public static bool HasSystemDarkMode => VersionHelper.IsEqualOrGreater(DeviceHelper.OnPlatform(13, 10));
    public static bool IsLightTheme => !IsDarkTheme;
    public static bool IsDarkTheme => Application.Current.RequestedTheme == OSAppTheme.Dark;
}