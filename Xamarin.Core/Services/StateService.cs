namespace Xamarin.Core.Services;

public static class StateManager
{
    private static readonly Dictionary<string, object> AppService = new();

    public static void Set<T>(string name, T value) => AppService.TryAdd(name, value);

    public static T Get<T>(string key) => AppService.TryGetValue(key, out var result)
                                              ? (T)result
                                              : default;

    public static void RemoveKey(string key)
    {
        if (AppService.ContainsKey(key))
        {
            AppService.Remove(key);
        }
    }

    public static bool ContainsKey(string name) => AppService.ContainsKey(name);
}