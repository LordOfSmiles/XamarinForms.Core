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

            FillProperties(item, i, items.Count);
        }
    }

    public static void SetFirstAndLast<T>(IEnumerable<T> items)
        where T : IUiListItem
    {
        var count = items.Count();

        for (var i = 0; i < count; i++)
        {
            var item = items.ElementAt(i);

            FillProperties(item, i, count);
        }
    }

    #region private Methods

    private static void FillProperties<T>(T item, int index, int total)
        where T : IUiListItem
    {
        item.Index = index;

        item.IsFirst = index == 0;
        item.IsLast = index == total - 1;

        item.IsSingle = index == 0 && total == 1;
    }

    #endregion
}