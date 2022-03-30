using System;
using System.Collections.Generic;

namespace XamarinForms.Core.Infrastructure.Navigation
{
    public static class NavigationHelper
    {
        public static void AddNavigationParameterByPageType(Type pageType, string key)
        {
            AddNavigationParameterByPageType(pageType, key, string.Empty);
        }
        
        public static void Add(string pageName, string key, object value)
        {
            if (!NavigationParameters2.ContainsKey(pageName))
            {
                NavigationParameters2.Add(pageName, new Dictionary<string, object>
                {
                    {key, value}
                });
            }
            else
            {
                var entry = NavigationParameters2[pageName];

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
    
        public static void AddNavigationParameterByPageType(Type pageType, string key, object value)
        {
            if (!NavigationParameters.ContainsKey(pageType))
            {
                NavigationParameters.Add(pageType, new Dictionary<string, object>
                {
                    {key, value}
                });
            }
            else
            {
                var entry = NavigationParameters[pageType];

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

        public static void AddNavigationParameterByPageType(Type pageType, IDictionary<string, object> parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            foreach (var parameter in parameters)
            {
                AddNavigationParameterByPageType(pageType, parameter.Key, parameter.Value);
            }
        }

        public static IDictionary<string, object> GetParametersByPageType(Type pageType)
        {
            IDictionary<string, object> result = new Dictionary<string, object>();

            if (NavigationParameters.ContainsKey(pageType))
            {
                result = NavigationParameters[pageType];
                NavigationParameters.Remove(pageType);
            }

            return result;
        }
        
        public static IDictionary<string, object> Get(string pageName)
        {
            IDictionary<string, object> result = new Dictionary<string, object>();

            if (NavigationParameters2.ContainsKey(pageName))
            {
                result = NavigationParameters2[pageName];
                NavigationParameters2.Remove(pageName);
            }

            return result;
        }

        #region Fields

        private static readonly IDictionary<Type, IDictionary<string, object>> NavigationParameters = new Dictionary<Type, IDictionary<string, object>>();
        private static readonly IDictionary<string, IDictionary<string, object>> NavigationParameters2 = new Dictionary<string, IDictionary<string, object>>();

        #endregion
    }
}
