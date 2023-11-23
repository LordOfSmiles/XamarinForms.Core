using Android.Content;
using XamarinForms.Core.PlatformServices;

namespace XamarinForms.Droid.PlatformServices;

public sealed class ExactAlarmsPermission : IExactAlarmsPermission
{
    public void Request()
    {
        Xamarin.Essentials.Platform.CurrentActivity.StartActivity(new Intent("android.settings.REQUEST_SCHEDULE_EXACT_ALARM"));
    }
}