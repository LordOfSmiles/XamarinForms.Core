using System.Collections.Generic;
using Xamarin.Core.Interfaces;

namespace Xamarin.Core.Helpers
{
    public static class SelectableHelper
    {
        public static void SetSelection(ISelectable sender, IEnumerable<ISelectable> allItemsForSelection, bool withDeselection = false)
        {
            if (sender.IsSelected)
            {
                foreach (var selectable in allItemsForSelection)
                {
                    selectable.SetSelected(sender.Id == selectable.Id);
                }
            }
            else if(withDeselection)
            {
                sender.SetSelected(false);
            }
            else
            {
                sender.SetSelected(true);
            }
        }
    }
}