using System;

namespace Xamarin.Core.Interfaces
{
    public interface ISelectable
    {
        [Obsolete]
        int Id { get; }
        string Header { get; }
        bool IsSelected { get; set; }
        void SetSelected(bool isSelected);
    }
}