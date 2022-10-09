namespace XamarinForms.Core.Infrastructure.Navigation;

public static class NavigationHelper
{
    public static void Add(string pageName, string key)
    {
        Add(pageName, key, string.Empty);
    }

    public static void Add(string pageName, string key, object value)
    {
        if (!InnerDict.ContainsKey(pageName))
        {
            InnerDict.Add(pageName, new Dictionary<string, object>
                                    {
                                        { key, value }
                                    });
        }
        else
        {
            var entry = InnerDict[pageName];

            if (!entry.ContainsKey(key))
            {
                entry.Add(key, value);
            }
            else
            {
                entry[key] = value;
            }
        }
    }

    public static void Add(string pageName, IDictionary<string, object> parameters)
    {
        if (parameters == null)
            throw new ArgumentNullException(nameof(parameters));

        foreach (var parameter in parameters)
        {
            Add(pageName, parameter.Key, parameter.Value);
        }
    }

    public static IDictionary<string, object> Get(string pageName)
    {
        IDictionary<string, object> result = new Dictionary<string, object>();

        if (InnerDict.ContainsKey(pageName))
        {
            result = InnerDict[pageName];
            InnerDict.Remove(pageName);
        }

        return result;
    }

    #region Fields

    private static readonly IDictionary<string, IDictionary<string, object>> InnerDict = new Dictionary<string, IDictionary<string, object>>();

    #endregion
}