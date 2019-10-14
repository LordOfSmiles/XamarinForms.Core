using System;
using UIKit;
using Xamarin.Core.Standard.Infrastructure.Interfaces;
using Xamarin.Forms;
using XamarinForms.Core.iOS.Helpers;
using XamarinForms.Core.iOS.Infrastructure;

[assembly:Xamarin.Forms.Dependency(typeof(DeviceThemeService))]  
namespace XamarinForms.Core.iOS.Infrastructure
{
    public sealed class DeviceThemeService:IDeviceThemeService
    {
        public bool IsSupportNativeDarkTheme => VersionHelper.IsOs13OrHigher;

        public DeviceThemeMode CurrentTheme
        {
            get
            {
                var result = DeviceThemeMode.Light;

                if (VersionHelper.IsOs13OrHigher)
                {
                    var currentUiViewController = UIViewControllerHelper.GetRootViewController();
                    if (currentUiViewController?.TraitCollection != null)
                    {
                        var userInterfaceStyle = currentUiViewController.TraitCollection.UserInterfaceStyle;

                        switch (userInterfaceStyle)
                        {
                            case UIUserInterfaceStyle.Light:
                                return DeviceThemeMode.Light;
                            case UIUserInterfaceStyle.Dark:
                                return DeviceThemeMode.Dark;
                        }
                    }
                }

                return result;
            }
        }
    }
}