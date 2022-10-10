namespace Xamarin.Core.Interfaces;

public interface ISelectable
{
    string Text { get; }
    bool IsDestruction { get; }
    bool IsSelected { get; set; }
    ICommand TapCommand { get; set; }
}

public interface ISelectable<out T> : ISelectable
{
    T Id { get; }
}