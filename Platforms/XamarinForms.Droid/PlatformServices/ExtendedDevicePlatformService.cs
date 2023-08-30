using System;
using Android.Content;
using Android.OS;
using Android.Provider;
using Xamarin.Essentials;
using XamarinForms.Core.Helpers;
using XamarinForms.Core.PlatformServices;
using Uri = Android.Net.Uri;

namespace XamarinForms.Droid.PlatformServices;

public sealed class ExtendedDevicePlatformService:IExtendedDevicePlatformService
{
    public bool IsDeviceWithSafeArea => false;

    public void GoToPowerSettings()
    {
        if (VersionHelper.IsEqualOrGreater(6))
        {
            var intent = new Intent();
            if (IsIgnoredPowerOptimizations)
            {
                intent.SetAction(Settings.ActionIgnoreBatteryOptimizationSettings);
            }
            else
            {
                intent.SetAction(Settings.ActionRequestIgnoreBatteryOptimizations);
                intent.SetData(Uri.Parse("package:" + AppInfo.PackageName));
            }

            try
            {
                Platform.CurrentActivity.StartActivity(intent);
            }
            catch
            {
              //
            }
        }
    }

    public bool IsIgnoredPowerOptimizations
    {
        get
        {
            var result = true;

            if (VersionHelper.IsEqualOrGreater(6))
            {
                var powerManager = (PowerManager)Platform.AppContext.GetSystemService(Context.PowerService);
                if (powerManager != null)
                {
                    result = powerManager.IsIgnoringBatteryOptimizations(AppInfo.PackageName);
                }
            }

            return result;
        }
    }
}