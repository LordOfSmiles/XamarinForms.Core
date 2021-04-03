using Xamarin.Forms;

namespace XamarinForms.Core.Helpers
{
    public static class ResourcesHelper
    {
        public static T GetValueByKey<T>(string key)
        {
            var result = default(T);
            
            if (Application.Current.Resources.ContainsKey(key))
            {
                result = (T) Application.Current.Resources[key];
            }
            else
            {
                foreach (var dictionary in Application.Current.Resources.MergedDictionaries)
                {
                    if (dictionary.ContainsKey(key))
                    {
                        result = (T) dictionary[key];
                        break;
                    }
                }
            }

            return result;
        }

        public static Color GetPlatformColor(string key)
        {
            var result = Color.Default;

            var onPlatform = (OnPlatform<Color>) Application.Current.Resources[key];
            if (onPlatform?.Platforms != null)
            {
                if (onPlatform.Platforms.Count == 2)
                {
                    var platform = DeviceHelper.OnPlatform(onPlatform.Platforms[0], onPlatform.Platforms[1]);
                    if (platform?.Value != null)
                    {
                        result = Color.FromHex(platform.Value.ToString());
                    }
                }
                else
                {
                    if (onPlatform.Platforms[0]?.Value != null)
                    {
                        var value = onPlatform.Platforms[0].Value.ToString();
                        result = Color.FromHex(value);
                    }
                }
            }

            return result;
        }

        public static Color GetThemeColor(Color light, Color dark)
        {
            if (Application.Current.UserAppTheme == OSAppTheme.Dark)
            {
                return dark;
            }
            else
            {
                return light;
            }
        }
    }
}