namespace XamarinForms.Core.Helpers;

public sealed class InputTransparentHelper
{
    public static void SetAllChildrenInputTransparent(View rootControl)
    {
        if (rootControl == null)
            return;

        rootControl.InputTransparent = true;

        switch (rootControl)
        {
            case Layout<View> layout:
                foreach (var child in layout.Children)
                {
                    SetAllChildrenInputTransparent(child);
                }
                break;
            case ContentView contentView:
                SetAllChildrenInputTransparent(contentView.Content);
                break;
        }
    }
}