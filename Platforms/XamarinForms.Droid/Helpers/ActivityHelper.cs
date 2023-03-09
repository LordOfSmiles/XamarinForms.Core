using Android.Content;

namespace XamarinForms.Droid.Helpers;

public static class ActivityHelper
{
    public static void ReloadActivity(Context context, System.Type activityType)
    {
        var intent = new Intent(context, activityType);
        intent.SetFlags(ActivityFlags.ClearTask | ActivityFlags.NewTask);
        context.StartActivity(intent);
    }
}