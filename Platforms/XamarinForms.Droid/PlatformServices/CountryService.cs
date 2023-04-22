using Android.Content;
using Android.Telephony;
using XamarinForms.Core.PlatformServices;

namespace XamarinForms.Droid.PlatformServices;

public class CountryService : ICountryService
{
    public string GetCountryCode()
    {
        var result = "";

        try
        {
            var tm = (TelephonyManager)Xamarin.Essentials.Platform.AppContext.GetSystemService(Context.TelephonyService);
            result = tm?.SimCountryIso;
        }
        catch
        {
            //
        }

        return result;
    }
}