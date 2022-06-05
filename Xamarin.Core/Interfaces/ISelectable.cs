using System.Windows.Input;

namespace Xamarin.Core.Interfaces;

public interface ISelectable : IUiListItem
{
    string Text { get; }
    bool IsSelected { get; set; }
    ICommand TapCommand { get; set; }
}

public interface ISelectable<out T> : ISelectable
{
    T Id { get; }
}