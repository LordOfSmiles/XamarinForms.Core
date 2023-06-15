using Xamarin.Core.Interfaces;

namespace Xamarin.Core.Helpers;

public static class SelectableHelper
{
    public static void SetSelection(ISelectable selectedItem, IEnumerable<ISelectable> items)
    {
        foreach (var selectable in items)
        {
            selectable.IsSelected = selectable.Text == selectedItem.Text;
        }
    }

    public static void SetNullableSelection(ISelectable selectedItem, IEnumerable<ISelectable> items)
    {
        selectedItem.IsSelected = !selectedItem.IsSelected;

        foreach (var selectable in items.Where(x => x.Text != selectedItem.Text))
        {
            selectable.IsSelected = false;
        }
    }
}