using Android.App;
using Android.Content;
using Android.OS;
using XamarinForms.Core.Helpers;

namespace XamarinForms.Droid.Extensions;

public static class ContextExtensions
{
    public static void StartForegroundServiceCompat<T>(this Context context, Bundle args = null)
        where T : Service
    {
        var intent = new Intent(context, typeof(T));
        if (args != null) 
        {
            intent.PutExtras(args);
        }

        if (VersionHelper.IsEqualOrGreater(8))
        {
            context.StartForegroundService(intent);
        }
        else
        {
            context.StartService(intent);
        }
    }
}