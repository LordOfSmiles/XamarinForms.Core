using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinForms.Core.Extensions
{
    public static class ShellExtensions
    {
        public static Task GoBack(this Shell shell)
        {
            return shell?.Navigation.PopAsync();
        }
    }
}