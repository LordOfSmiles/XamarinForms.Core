namespace XamarinForms.Core.Builders;

public sealed class ToolbarBuilder
{
    public ToolbarBuilder Command(ICommand command)
    {
        _toolbarItem.Command = command;
        return this;
    }
        
    public ToolbarBuilder Text(string text)
    {
        _toolbarItem.Text = text;
        return this;
    }
        
    public ToolbarBuilder Icon(ImageSource imageSource)
    {
        _toolbarItem.IconImageSource = imageSource;
        return this;
    }

    public ToolbarItem Build()
    {
        return _toolbarItem;
    }

    #region Fields

    private readonly ToolbarItem _toolbarItem;
        
    #endregion
        
    public ToolbarBuilder()
    {
        _toolbarItem = new ToolbarItem();
    }
}