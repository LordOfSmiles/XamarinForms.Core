using System.Collections.Generic;

namespace Xamarin.Core.Services;

public static class StateManager
{
    private static readonly Dictionary<string, object> AppService = new Dictionary<string, object>();

    public static void Set<T>(string name, T value)
    {
        AppService[name] = value;
    }

    public static T Get<T>(string name)
    {
        T result = default(T);

        if (AppService.ContainsKey(name))
        {
            result = (T)AppService[name];
        }

        return result;
    }

    public static void RemoveKey(string key)
    {
        if (AppService.ContainsKey(key))
        {
            AppService.Remove(key);
        }
    }

    public static bool ContainsKey(string name)
    {
        return AppService.ContainsKey(name);
    }
}