using System;
using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;

namespace XamarinForms.Core.Droid.Helpers
{
    public static class AlarmManagerHelper
    {
        public static void FireWidgetIntent(Context context, Type receiverType)
        {
            var alarmManager = context.GetSystemService(Context.AlarmService) as AlarmManager;

            var intent = GetAlarmPendingIntent(context, receiverType);
            var interval = SystemClock.ElapsedRealtime() + (60 * 1000);

            if (VersionHelper.IsAndroid6AndHigher)
            {
                alarmManager?.SetExactAndAllowWhileIdle(AlarmType.ElapsedRealtime, interval, intent);
            }
            else if (VersionHelper.IsAndroid5AndHigher)
            {
                alarmManager?.SetExact(AlarmType.ElapsedRealtime, interval, intent);
            }
            else
            {
                alarmManager?.Set(AlarmType.ElapsedRealtime, interval, intent);
            }
        }

        public static void StopWidgetAlarms(Context context, Type receiverType)
        {
            var intent = GetCancelPendingIntent(context, receiverType);
            var alarmManager = context.GetSystemService(Context.AlarmService) as AlarmManager;
            if (alarmManager != null && intent != null)
            {
                alarmManager.Cancel(intent);
            }
        }

        #region Private Methods

        private static PendingIntent GetAlarmPendingIntent(Context context, Type receiverType)
        {
            var intent = new Intent(context, receiverType);
            intent.SetAction(AppWidgetManager.ActionAppwidgetUpdate);
            return PendingIntent.GetBroadcast(context, 0, intent, 0);
        }

        private static PendingIntent GetCancelPendingIntent(Context context, Type receiverType)
        {
            var intent = new Intent(context, receiverType);
            intent.SetAction(AppWidgetManager.ActionAppwidgetUpdate);
            return PendingIntent.GetBroadcast(context, 0, intent, 0);
        }

        #endregion
    }
}