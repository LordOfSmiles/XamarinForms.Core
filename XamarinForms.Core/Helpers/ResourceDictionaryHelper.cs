using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Helpers
{
    public static class ResourceDictionaryHelper
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
    }
}