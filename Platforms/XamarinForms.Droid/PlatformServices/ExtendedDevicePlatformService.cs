using System;
using Android.Content;
using Android.OS;
using Android.Provider;
using Xamarin.Forms;
using XamarinForms.Core.Helpers;
using XamarinForms.Core.PlatformServices;
using XamarinForms.Droid.PlatformServices;

namespace XamarinForms.Droid.PlatformServices;

public sealed class ExtendedDevicePlatformService:IExtendedDevicePlatformService
{
    public bool IsDeviceWithSafeArea => false;

    public void GoToPowerSettings()
    {
        if (VersionHelper.IsEqualOrGreater(6))
        {
            var intent = new Intent();
            intent.SetAction(Settings.ActionIgnoreBatteryOptimizationSettings);
            intent.SetFlags(ActivityFlags.NewTask);

            try
            {
                Xamarin.Essentials.Platform.AppContext.StartActivity(intent);
            }
            catch
            {
                //
            }
        }
    }

    public bool IsIgnoredPowerOptimizations()
    {
        var result = true;

        if (VersionHelper.IsEqualOrGreater(6))
        {
            var powerManager = (PowerManager)Xamarin.Essentials.Platform.AppContext.GetSystemService(Context.PowerService);
            if (powerManager != null)
            {
                result = powerManager.IsIgnoringBatteryOptimizations(Xamarin.Essentials.AppInfo.PackageName);
            }
        }

        return result;
    }
}