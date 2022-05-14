using Xamarin.Forms;

namespace XamarinForms.Core.Builders;

public sealed class ToolbarBuilder
{

    public ToolbarBuilder WithCommand(ICommand command)
    {
        _toolbarItem.Command = command;
        return this;
    }
        
    public ToolbarBuilder WithText(string text)
    {
        _toolbarItem.Text = text;
        return this;
    }
        
    public ToolbarBuilder WithIcon(ImageSource imageSource)
    {
        _toolbarItem.IconImageSource = imageSource;
        return this;
    }

    public ToolbarItem GetItem()
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