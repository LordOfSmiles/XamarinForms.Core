namespace Xamarin.Core.Helpers;

public static class CollectionHelper
{
    public static void RemoveByType<T>(ICollection<object> collection)
    {
        var itemsByType = collection.OfType<T>().ToArray();

        foreach (var item in itemsByType)
        {
            collection.Remove(item);
        }
    }
}