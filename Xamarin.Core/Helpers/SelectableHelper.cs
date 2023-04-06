using Xamarin.Core.Interfaces;

namespace Xamarin.Core.Helpers;

public static class SelectableHelper
{
    public static void SetSelection(ISelectable selectedItem, IReadOnlyCollection<ISelectable> items, bool withNull = false)
    {
        foreach (var selectable in items)
        {
            var isSelected = selectable.Text == selectedItem.Text;
            if (!withNull || selectable.Text != selectedItem.Text)
            {
                selectable.IsSelected = isSelected;
            }
        }
    }

    // public static void SetSelection(ISelectable sender, IEnumerable<ISelectable> allItemsForSelection, bool withDeselection = false)
    // {
    //     if (sender.IsSelected)
    //     {
    //         foreach (var selectable in allItemsForSelection)
    //         {
    //             //selectable.SetSelected(sender.Text == selectable.Text);
    //         }
    //     }
    //     else if (withDeselection)
    //     {
    //         //sender.SetSelected(false);
    //     }
    //     else
    //     {
    //         //sender.SetSelected(true);
    //     }
    // }
}