namespace XamarinForms.Core.Controls.BottomSheets.Models;

public sealed class BottomSheetItemWithIcon : BottomSheetItemBase
{
    public BottomSheetItemWithIcon(ImageSource icon, string text, bool isDestructive = false)
        : base(text, isDestructive)
    {
        Icon = icon;
    }

    public ImageSource Icon { get; }
}