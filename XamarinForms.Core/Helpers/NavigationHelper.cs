using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Helpers
{
    public static class NavigationHelper
    {
        public static async Task NavigateToAsync(Type pageType)
        {
            if (Application.Current != null)
            {
                var destinationPage = Activator.CreateInstance(pageType) as Page;
                
                if (Application.Current.MainPage is NavigationPage navPage)
                {
                    await navPage.Navigation.PushAsync(destinationPage);
                }
                else if(Application.Current.MainPage is MasterDetailPage masterDetailPage)
                {
                    await masterDetailPage.Detail.Navigation.PushAsync(destinationPage);
                }
            }
        }
    }
}