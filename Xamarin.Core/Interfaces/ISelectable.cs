namespace Xamarin.Core.Interfaces;

public interface ISelectable : IUiListItem
{
    string Text { get; }
    object Tag { get; set; }
    bool IsSelected { get; set; }
    ICommand TapCommand { get; set; }
}

public interface ISelectable<out T> : ISelectable
{
    T Id { get; }
}