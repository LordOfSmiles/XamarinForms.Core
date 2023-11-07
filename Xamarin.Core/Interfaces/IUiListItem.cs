namespace Xamarin.Core.Interfaces;

public interface IUiListItem
{
    int Index { get; set; }
    bool IsFirst { get; set; }
    bool IsLast { get; set; }
}