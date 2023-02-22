using Xamarin.Core.Interfaces;

namespace Xamarin.Core.Models;

public sealed class SelectableItem<T> : SelectableItem, ISelectable<T>
{
    public SelectableItem(T id, string text, bool isSelected = false)
        : base(text, isSelected)
    {
        Id = id;
    }

    public T Id { get; }
}

public class SelectableItem : NotifyObject, ISelectable, IUiListItem
{
    #region ISelectable

    public string Text { get; }

    public bool IsDestruction { get; set; }

    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }
    private bool _isSelected;

    public ICommand TapCommand { get; set; }

    #endregion
    
    #region IUiListItem
    
    public bool IsFirst { get; set; }
    public bool IsLast { get; set; }
    
    #endregion

    public SelectableItem(string text, bool isSelected = false)
    {
        Text = text;
        IsSelected = isSelected;
    }
}