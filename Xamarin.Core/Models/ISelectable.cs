using System.Windows.Input;

namespace Xamarin.Core.Models
{
    public interface ISelectable
    {
        bool IsSelected { get; set; }

        ICommand SelectCommand { get; set; }
    }
}
