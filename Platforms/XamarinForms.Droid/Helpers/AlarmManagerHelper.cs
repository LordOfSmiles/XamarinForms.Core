using System;
using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using XamarinForms.Core.Helpers;

namespace XamarinForms.Droid.Helpers;

public static class AlarmManagerHelper
{
    public static void FireIntent(Context context, Type receiverType, int intervalInMs)
    {
        var alarmManager = context.GetSystemService(Context.AlarmService) as AlarmManager;

        var intent = GetAlarmPendingIntent(context, receiverType);
        var interval = SystemClock.ElapsedRealtime() + intervalInMs;

        if (VersionHelper.IsEqualOrGreater(6))
        {
            alarmManager?.SetExactAndAllowWhileIdle(AlarmType.ElapsedRealtimeWakeup, interval, intent);
        }
        else if (VersionHelper.IsEqualOrGreater(5))
        {
            alarmManager?.SetExact(AlarmType.ElapsedRealtimeWakeup, interval, intent);
        }
        else
        {
            alarmManager?.Set(AlarmType.ElapsedRealtimeWakeup, interval, intent);
        }
    }

    public static void StopWidgetAlarms(Context context, Type receiverType)
    {
        var intent = GetCancelPendingIntent(context, receiverType);
        var alarmManager = context.GetSystemService(Context.AlarmService) as AlarmManager;
        if (intent != null)
        {
            alarmManager?.Cancel(intent);
            intent.Cancel();
        }
    }

    #region Private Methods

    private static PendingIntent GetAlarmPendingIntent(Context context, Type receiverType)
    {
        var intent = new Intent(context, receiverType);
        intent.SetAction(AppWidgetManager.ActionAppwidgetUpdate);
        var intentFlag = VersionHelper.IsEqualOrGreater(12)
                             ? PendingIntentFlags.Mutable
                             : PendingIntentFlags.OneShot;

        return PendingIntent.GetBroadcast(context, 0, intent, intentFlag);
    }

    private static PendingIntent GetCancelPendingIntent(Context context, Type receiverType)
    {
        var intent = new Intent(context, receiverType);
        intent.SetAction(AppWidgetManager.ActionAppwidgetUpdate);
        var intentFlag = VersionHelper.IsEqualOrGreater(12)
                             ? PendingIntentFlags.Mutable
                             : PendingIntentFlags.OneShot;

        return PendingIntent.GetBroadcast(context, 0, intent, intentFlag);
    }

    #endregion
}