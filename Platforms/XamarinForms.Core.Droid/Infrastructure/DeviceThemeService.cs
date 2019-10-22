using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Content.Res;
using Xamarin.Core.Infrastructure.Interfaces;
using XamarinForms.Core.Droid.Helpers;

namespace XamarinForms.Core.Droid.Infrastructure
{
    public sealed class DeviceThemeService:IDeviceThemeService
    {
        public bool IsSupportNativeDarkTheme => VersionHelper.IsAndroid10;

        public DeviceThemeMode CurrentTheme
        {
            get
            {
                var result = DeviceThemeMode.Light;

//                if (VersionHelper.IsAndroid8)
//                {
//                    var appContext = context as Context;
//                    if (appContext != null)
//                    {
//                        var uiModeFlags = appContext.Resources.Configuration.UiMode & UiMode.NightMask;
//
//                        switch (uiModeFlags)
//                        {
//                            case UiMode.NightYes:
//                                result = DeviceThemeMode.Dark;
//                                break;
//                            case UiMode.NightNo:
//                                result = DeviceThemeMode.Light;
//                                break;
//                        }
//                    }
//                }

                return result;
            }
        }
    }
}