using Android.Content;
using Android.Content.PM;
using Android.Telephony;
using AndroidX.Core.Content;
using AndroidX.Core.Telephony;
using XamarinForms.Core.PlatformServices;

namespace XamarinForms.Droid.PlatformServices;

public class CountryService : ICountryService
{
    public string GetCountryCode()
    {
        var result = "";

        try
        {
            if (Xamarin.Essentials.Platform.AppContext.PackageManager?.HasSystemFeature(PackageManager.FeatureTelephony) ?? false)
            {
                var tm = (TelephonyManager)Xamarin.Essentials.Platform.AppContext.GetSystemService(Context.TelephonyService);
                result = tm?.SimCountryIso;
            }
        }
        catch
        {
            //
        }

        return result;
    }
}