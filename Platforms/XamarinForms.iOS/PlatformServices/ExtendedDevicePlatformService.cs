using System.Linq;
using XamarinForms.Core.Helpers;
using XamarinForms.iOS.PlatformServices;
using XamarinForms.Core.PlatformServices;

[assembly:Dependency(typeof(ExtendedDevicePlatformService))]
namespace XamarinForms.iOS.PlatformServices;

public sealed class ExtendedDevicePlatformService:IExtendedDevicePlatformService
{
    public bool IsDeviceWithSafeArea
    {
        get
        {
            var result = false;

            if (VersionHelper.IsEqualOrGreater(13))
            {
                var window = UIApplication.SharedApplication.Windows.FirstOrDefault();
                var scene = window?.WindowScene;
                if (scene != null)
                {
                    result = CheckSafeArea(window, scene.InterfaceOrientation == UIInterfaceOrientation.Portrait);
                }
            }
            else if (VersionHelper.IsEqualOrGreater(11))
            {
                var window = UIApplication.SharedApplication.Windows.FirstOrDefault(x => x.IsKeyWindow);
                if (window != null)
                {
                    result = CheckSafeArea(window, UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.Portrait);
                }
            }

            return result;
        }
    }
        
    #region Private Methods

    private bool CheckSafeArea(UIView window, bool isPortrait)
    {
        if (isPortrait)
        {
            return window.SafeAreaInsets.Top >= DeviceHelper.OnIdiom(44, 24);
        }
        else
        {
            return window.SafeAreaInsets.Bottom > 0 || window.SafeAreaInsets.Top > 0;
        }
    }

    #endregion
}