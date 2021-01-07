using System;
using System.Linq;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace XamarinForms.Core.Droid.Helpers
{
    public static class WidgetHelper
    {
        public static void UpdateWidget(Context context, Type widgetType)
        {
            if (context == null)
                return;
            
            var widgetManager = AppWidgetManager.GetInstance(context);
            if (widgetManager != null)
            {
                var widgetProvider = new ComponentName(context, GetWidgetClassName(widgetType));
                var ids = widgetManager.GetAppWidgetIds(widgetProvider);
                if (ids?.Any() ?? false)
                {
                    var updateIntent = new Intent(context, widgetType);
                    updateIntent.SetAction(AppWidgetManager.ActionAppwidgetUpdate);
                    updateIntent.PutExtra(AppWidgetManager.ExtraAppwidgetIds, ids);
                    context.SendBroadcast(updateIntent);
                }
            }
        }

        public static void UpdateWidget(Context context, Func<Context, RemoteViews> buildRemoteViews, Type widgetType)
        {
            if (context == null)
                return;
            
            var widgetManager = AppWidgetManager.GetInstance(context);
            if (widgetManager != null)
            {
                var widgetProvider = new ComponentName(context, GetWidgetClassName(widgetType));
                var ids = widgetManager.GetAppWidgetIds(widgetProvider);
                if (ids?.Any() ?? false)
                    widgetManager.UpdateAppWidget(ids, buildRemoteViews(context));
            }
        }
        
        public static void PinWidget(Context context, Type widgetType)
        {
            var widgetManager = AppWidgetManager.GetInstance(context);
            if (widgetManager != null && IsWidgetPinningSupported(context))
            {
                var widgetProvider = new ComponentName(context, GetWidgetClassName(widgetType));
                widgetManager.RequestPinAppWidget(widgetProvider, Bundle.Empty, null);
            }
        }
        
        public static bool IsWidgetPinningSupported(Context context)
        {
            var result = false;

            if (VersionHelper.IsAndroid8AndHigher)
            {
                var widgetManager = AppWidgetManager.GetInstance(context);

                if (widgetManager != null)
                    result = widgetManager.IsRequestPinAppWidgetSupported;
            }

            return result;
        }

        public static string GetWidgetClassName(Type widgetType)
        {
            return Java.Lang.Class.FromType(widgetType).Name;
        }
    }
}