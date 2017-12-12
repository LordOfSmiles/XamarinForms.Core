using System.Windows.Input;

namespace Xamarin.Core.Standard.Models
{
    public interface ISelectable
    {
        bool IsSelected { get; set; }

        ICommand SelectCommand { get; set; }
    }
}
