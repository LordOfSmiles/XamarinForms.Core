using Xamarin.Core.Interfaces;

namespace Xamarin.Core.Models;

public class SelectableItem<T> : SelectableItem, ISelectable<T>
{
    public SelectableItem(T id, string text, bool isSelected = false)
        : base(text, isSelected)
    {
        Id = id;
    }

    public T Id { get; }
}

public class SelectableItem : NotifyObject, ISelectable
{
    #region ISelectable

    public string Text { get; }
    
    public object Tag { get; set; }

    public virtual bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }
    private bool _isSelected;

    public ICommand TapCommand { get; set; }

    #endregion

    #region IUiListItem

    public int Index
    {
        get => _index;
        set => SetProperty(ref _index, value);
    }
    private int _index;

    public bool IsFirst
    {
        get => _isFirst;
        set => SetProperty(ref _isFirst, value);
    }
    private bool _isFirst;

    public bool IsLast
    {
        get => _isLast;
        set => SetProperty(ref _isLast, value);
    }
    private bool _isLast;

    public bool IsSingle
    {
        get => _isSingle;
        set => SetProperty(ref _isSingle, value);
    }
    private bool _isSingle;

    #endregion

    public SelectableItem(string text, bool isSelected = false)
    {
        Text = text;
        IsSelected = isSelected;
    }
}

public sealed class ContextMenuSelectableItem : SelectableItem, IContextMenuItem
{
    public ContextMenuSelectableItem(string text, bool isDestruction = false)
        : base(text)
    {
        IsDestruction = isDestruction;
    }

    public bool IsDestruction { get; set; }
}

public interface IContextMenuItem : ISelectable
{
    bool IsDestruction { get; set; }
}