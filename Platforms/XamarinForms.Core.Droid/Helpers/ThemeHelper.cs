using System.Collections.Generic;
using System.Linq;
using Android.Views;

namespace XamarinForms.Core.Droid.Helpers
{
    public static class ThemeHelper
    {
        private static string _themePrefix = "AppTheme";
        private static readonly Dictionary<string, int> Themes = new Dictionary<string, int>();

        static ThemeHelper()
        {
            var type = typeof(Resource.Style);
            foreach (var field in type.GetFields().Where(f => f.Name.StartsWith(_themePrefix)))
            {
                Themes.Add(field.Name, (int)field.GetRawConstantValue());
            }
        }

        public static int? GetTheme<T>(string style) where T : View
        {
            if (string.IsNullOrEmpty(style))
                return null;

            var control = typeof(T).Name;
            var themeName = $"AppTheme_{control}_{style}";

            if (Themes.TryGetValue(themeName, out var resourceId))
            {
                return resourceId;
            }

            return null;
        }
    }
}