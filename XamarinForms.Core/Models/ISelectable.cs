using System.Windows.Input;

namespace XamarinForms.Core.Models
{
    public interface ISelectable
    {
        bool IsSelected { get; set; }

        ICommand SelectCommand { get; set; }
    }
}
