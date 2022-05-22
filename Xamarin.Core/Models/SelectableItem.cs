using System.Windows.Input;
using Xamarin.Core.Interfaces;

namespace Xamarin.Core.Models;

public sealed class SelectableItem<T> : NotifyObject, ISelectable
{
    #region ISelectable

    public string Text { get; }
        
    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }
    private bool _isSelected;

    public void SetSelected(bool isSelected)
    {
        _isSelected = isSelected;
        OnPropertyChanged(nameof(IsSelected));
    }

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

    public SelectableItem(T id, string text, bool isSelected = false)
    {
        Id = id;
        Text = text;
        IsSelected = isSelected;
    }
        
    public T Id { get; }
}