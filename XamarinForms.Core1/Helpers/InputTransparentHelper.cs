using Xamarin.Forms;

namespace XamarinForms.Core.Helpers
{
    public sealed class InputTransparentHelper
    {
        public static void SetAllChildrenInputTransparent(View rootControl)
        {
            if (rootControl == null)
                return;

            rootControl.InputTransparent = true;

            var itemsControl = rootControl as Layout<View>;
            if (itemsControl != null)
            {
                foreach (var child in itemsControl.Children)
                {
                    SetAllChildrenInputTransparent(child);
                }
            }
            else
            {
                var contentView = rootControl as ContentView;
                if (contentView != null)
                {
					SetAllChildrenInputTransparent(contentView.Content);
                }
            }

        }
    }
}
