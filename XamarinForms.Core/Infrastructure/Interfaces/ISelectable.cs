namespace XamarinForms.Core.Infrastructure.Interfaces
{
    public interface ISelectable
    {
        void SetSelected(bool isSelected);
        
        int Id { get; }
        bool IsSelected { get; set; }
    }
}