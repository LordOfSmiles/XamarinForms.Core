using Xamarin.Core.Interfaces;

namespace Xamarin.Core.Helpers;

public static class UiListHelper
{
    public static void SetFirstAndLast<T>(IReadOnlyList<T> items)
        where T : IUiListItem
    {
        for (var i = 0; i < items.Count; i++)
        {
            var item = items[i];
            
            item.Index = i;
            item.IsFirst = i == 0;
            item.IsLast = i == items.Count - 1;
        }
    }

    public static void SetFirstAndLast<T>(IEnumerable<T> items)
        where T : IUiListItem
    {
        if (items.Any())
        {
            var count = items.Count();

            for (var i = 0; i < count; i++)
            {
                var item = items.ElementAt(i);
                
                item.Index = i;
                item.IsFirst = i == 0;
                item.IsLast = i == count - 1;
            }
        }
    }
}