using System;
using Android.App;
using Android.Content;
using XamarinForms.Core.Helpers;

namespace XamarinForms.Droid.Factories;

public static class PendingIntentFactory
{
    public static PendingIntent CreateBroadcastIntent(Context context, Type type, string action)
    {
        var intent = new Intent(context, type);
        intent.SetAction(action);
        intent.PutExtra("action", action);

        var flag = VersionHelper.IsEqualOrGreater(12)
                       ? PendingIntentFlags.Mutable
                       : PendingIntentFlags.OneShot;

        return PendingIntent.GetBroadcast(context, 0, intent, flag);
    }
    
    public static PendingIntent CreateActivityIntent(Context context, Type type)
    {
        var intent = new Intent(context, type);

        var flag = VersionHelper.IsEqualOrGreater(12)
                       ? PendingIntentFlags.Mutable
                       : PendingIntentFlags.OneShot;
        
        return PendingIntent.GetActivity(context, 0, intent, flag);
    }
}