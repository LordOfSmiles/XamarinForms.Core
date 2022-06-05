using System.Collections.Generic;

namespace Xamarin.Core.Interfaces;

public interface ISelectableGroup
{
    public string Text { get; set; }
    public IReadOnlyCollection<ISelectable> Items { get; set; }
    public ICommand TapCommand { get; set; }
}