namespace Xamarin.Core.Interfaces;

public interface ISelectableGroup :ISelectable
{
    public IReadOnlyCollection<ISelectable> Items { get; set; }
}