namespace Xamarin.Core.Standard.Infrastructure.Interfaces
{
    public interface IDeviceThemeService
    {
        bool IsSupportNativeDarkTheme { get; }

        DeviceThemeMode CurrentTheme { get; }
    }

    public enum DeviceThemeMode
    {
        Light,
        Dark
    }
}