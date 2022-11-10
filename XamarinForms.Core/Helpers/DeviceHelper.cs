
using Xamarin.Essentials;

namespace XamarinForms.Core.Helpers;

public static class DeviceHelper
{
    public static bool IsIos => DeviceInfo.Platform == DevicePlatform.iOS;
    public static bool IsAndroid => DeviceInfo.Platform == DevicePlatform.Android;

    public static T OnPlatform<T>(T iOs, T android) => IsIos
                                                           ? iOs
                                                           : android;

    public static void OnPlatform(Action iOs, Action android)
    {
        if (IsIos)
        {
            iOs.Invoke();
        }
        else
        {
            android.Invoke();
        }
    }
    public static T OnIdiom<T>(T phone, T tablet) => IsPhone
                                                         ? phone
                                                         : tablet;

    public static bool IsPhone => DeviceInfo.Idiom == DeviceIdiom.Phone;
    public static bool IsTablet => DeviceInfo.Idiom == DeviceIdiom.Tablet;

    public static bool IsPortrait => DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Portrait;
    
    public static double ScreenDensity => DeviceDisplay.MainDisplayInfo.Density;
    
    /// <summary>
    /// виртуальная ширина экрана
    /// </summary>
    public static double ScreenWidthInDp => ScreenWidthInPx / ScreenDensity;
    
    /// <summary>
    /// вирутальная высота экрана
    /// </summary>
    public static double ScreenHeightInDp => ScreenHeightInPx / ScreenDensity;
    
    /// <summary>
    /// абсолютная ширина экрана в пикселях
    /// </summary>
    public static double ScreenWidthInPx => DeviceDisplay.MainDisplayInfo.Width;
    
    /// <summary>
    /// абсолютная высота экрана в пикселях
    /// </summary>
    public static double ScreenHeightInPx => DeviceDisplay.MainDisplayInfo.Height;
}