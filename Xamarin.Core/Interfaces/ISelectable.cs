using System;

namespace Xamarin.Core.Interfaces
{
    public interface ISelectable : IUiListItem
    {
        string Text { get; }
        bool IsSelected { get; set; }
        void SetSelected(bool isSelected);
    }
}