namespace Xamarin.Core.Infrastructure.Interfaces
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