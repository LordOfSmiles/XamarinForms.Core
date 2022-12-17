namespace XamarinForms.Core.Infrastructure.ContextMenu;

public sealed class ContextMenuContainer
{
    public string Text { get; set; }

    public IReadOnlyCollection<ContextMenuItemInfo> MenuItems { get; set; }
}

public sealed class ContextMenuItemInfo
{
    public ContextMenuItemInfo(string text, string icon)
    {
        Text = text;
        Icon = icon;
    }

    public string Text { get; }

    public string Icon { get; }

    public bool IsDestructive { get; set; }

    public int Group { get; set; }

    public Func<Task> Action { get; set; }
}