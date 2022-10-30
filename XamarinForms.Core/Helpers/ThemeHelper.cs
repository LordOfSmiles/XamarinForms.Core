namespace XamarinForms.Core.Helpers;

public static class ThemeHelper
{
    public static bool IsDarkTheme => Application.Current.RequestedTheme == OSAppTheme.Dark;
}