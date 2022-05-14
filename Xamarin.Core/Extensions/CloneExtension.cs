namespace Xamarin.Core.Extensions;

public static class CloneExtension
{
    public static T Clone<T>(this T source)
    {
        if (source == null)
            return default;

        var sourceType = source.GetType();
            
        var cloned = (T)Activator.CreateInstance(sourceType);

        foreach (var curPropInfo in sourceType.GetProperties())
        {
            var getMethod = curPropInfo.GetGetMethod();
            var setMethod = curPropInfo.GetSetMethod();

            if (getMethod != null && setMethod != null)
            {
                // Handle Non-indexer properties
                if (curPropInfo.Name != "Item")
                {
                    // get property from source
                    var getValue = getMethod.Invoke(source, new object[] { });

                    // clone if needed
                    // if (getValue != null && getValue is DependencyObject)
                    //     getValue = Clone((DependencyObject)getValue);

                    // set property on cloned
                    setMethod.Invoke(cloned, new[] { getValue });
                }
                // handle indexer
                else
                {
                    // get count for indexer
                    var numberOfItemInCollection = (int)curPropInfo.ReflectedType.GetProperty("Count")
                                                                   .GetGetMethod()
                                                                   .Invoke(source, new object[] { });

                    // run on indexer
                    for (var i = 0; i < numberOfItemInCollection; i++)
                    {
                        // get item through Indexer
                        var getValue = curPropInfo.GetGetMethod().Invoke(source, new object[] { i });

                        // clone if needed
                        // if (getValue != null && getValue is DependencyObject)
                        //     getValue = Clone((DependencyObject)getValue);
                        // add item to collection
                        curPropInfo.ReflectedType.GetMethod("Add").Invoke(cloned, new[] { getValue });
                    }
                }
            }
        }

        return cloned;
    }
}