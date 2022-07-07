namespace Xamarin.Core.Interfaces;

public interface ISelectable
{
    string Text { get; }
    bool IsSelected { get; set; }
    ICommand TapCommand { get; set; }
}

public interface ISelectable<out T> : ISelectable
{
    T Id { get; }
}

public interface ISelectableWithOrder : ISelectable, IUiListItem
{
}

public enum LayoutTypeEnum
{
    Vertical,
    Wrap
}