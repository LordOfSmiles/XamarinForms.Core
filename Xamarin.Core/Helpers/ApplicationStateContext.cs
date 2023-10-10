namespace Xamarin.Core.Helpers;

public static class ApplicationStateContext
{
    public static void Add(string key, object value) => InnerDict.TryAdd(key, value);

    public static T Get<T>(string key)
    {
        if (InnerDict.ContainsKey(key))
        {
            var result = (T)InnerDict[key];
            InnerDict.Remove(key);
            return result;
        }
        else
        {
            return default;
        }
    }

    #region Fields

    private static readonly IDictionary<string, object> InnerDict = new Dictionary<string, object>();

    #endregion
}