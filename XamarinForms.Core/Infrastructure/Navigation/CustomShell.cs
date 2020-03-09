using Xamarin.Forms;

namespace XamarinForms.Core.Infrastructure.Navigation
{
    public abstract class CustomShell:Shell
    {
        public CustomShell()
        {
            
        }

        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);
        }
    }
}