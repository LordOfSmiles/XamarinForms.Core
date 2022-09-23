using System;
using System.Collections.Generic;
using System.Linq;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Widget;
using XamarinForms.Core.Helpers;

namespace XamarinForms.Droid.Helpers;

public static class WidgetHelper
{
    public static void UpdateWidget(Context context, Type widgetType)
    {
        if (context != null)
        {
            var widgetManager = AppWidgetManager.GetInstance(context);

            if (widgetManager != null)
            {
                var widgetProvider = new ComponentName(context, GetWidgetClassName(widgetType));
                var ids = widgetManager.GetAppWidgetIds(widgetProvider);

                if (ids?.Any() ?? false)
                {
                    foreach (var id in ids)
                    {
                        var updateIntent = new Intent(context, widgetType);
                        updateIntent.SetAction(AppWidgetManager.ActionAppwidgetUpdate);
                        updateIntent.PutExtra(AppWidgetManager.ExtraAppwidgetId, id);
                        context.SendBroadcast(updateIntent);
                    }
                }
            }
        }
    }

    public static void UpdateWidget(Context context, Type widgetType, Func<Context, RemoteViews> buildRemoteViews)
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
                foreach (var id in ids)
                {
                    widgetManager.UpdateAppWidget(id, buildRemoteViews(context));
                }
            }
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

        if (VersionHelper.IsEqualOrGreater(8))
        {
            var widgetManager = AppWidgetManager.GetInstance(context);

            if (widgetManager != null)
                result = widgetManager.IsRequestPinAppWidgetSupported;
        }

        return result;
    }

    public static string GetWidgetClassName(Type widgetType)
    {
        if (!WidgetNames.ContainsKey(widgetType))
        {
            WidgetNames.Add(widgetType, Java.Lang.Class.FromType(widgetType).Name);
        }

        return WidgetNames[widgetType];
    }

    #region Fields

    private static readonly Dictionary<Type, string> WidgetNames = new();

    #endregion
}