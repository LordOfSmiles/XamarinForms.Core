namespace Xamarin.Core.Interfaces
{
    public interface ISelectable
    {
        int Id { get; }
        bool IsSelected { get; set; }
        void SetSelected(bool isSelected);
    }
}