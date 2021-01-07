using Android.App;
using Android.Widget;
using XamarinForms.Core.Droid.PlatformServices;
using XamarinForms.Core.PlatformServices;

[assembly: Xamarin.Forms.Dependency(typeof(ToastPlatformServiceService))]

namespace XamarinForms.Core.Droid.PlatformServices
{
    public sealed class ToastPlatformServiceService : IToastPlatformService
    {
        public void ShowAlert(string title, string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short)?.Show();
        }

        public void ShowAlert(string message)
        {
            ShowAlert(null, message);
        }
    }
}