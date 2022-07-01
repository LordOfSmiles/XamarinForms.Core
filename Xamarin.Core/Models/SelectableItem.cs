using Xamarin.Core.Interfaces;

namespace Xamarin.Core.Models;

public sealed class SelectableItem<T> : SelectableItem
{
    public SelectableItem(T id, string text, bool isSelected = false)
        : base(text, isSelected)
    {
        Id = id;
    }

    public T Id { get; }
}

public class SelectableItem : NotifyObject, ISelectableWithOrder
{
    #region ISelectable

    public string Text { get; }

    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }
    private bool _isSelected;

    public LayoutTypeEnum LayoutType { get; set; }

    public ICommand TapCommand { get; set; }

    #endregion

    #region IUiListItem

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

    #endregion

    public SelectableItem(string text, bool isSelected = false)
    {
        Text = text;
        IsSelected = isSelected;
    }
}