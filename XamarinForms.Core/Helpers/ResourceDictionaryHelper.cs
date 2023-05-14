namespace XamarinForms.Core.Helpers;

public static class ResourceDictionaryHelper
{
    public static T GetValueByKey<T>(string key)
    {
        var result = default(T);
        
        if (Application.Current.Resources.TryGetValue(key, out var resource))
        {
            result = (T) resource;
        }
        else
        {
            foreach (var dictionary in Application.Current.Resources.MergedDictionaries)
            {
                if (dictionary.TryGetValue(key, out var value))
                {
                    result = (T) value;
                    break;
                }
            }
        }

        return result;
    }
}