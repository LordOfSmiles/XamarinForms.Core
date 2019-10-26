using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForms.Core.Standard.Infrastructure.Navigation;

namespace XamarinForms.Core.Services.NavigationService
{
    public sealed class NavigationService:INavigationService
    {
        public async Task NavigateToAsync(Type pageType, IDictionary<string, object> navigationParameters)
        {
            NavigationState.AddNavigationParameterByPageType(pageType, navigationParameters);

            var navPage = Application.Current.MainPage as NavigationPage;
            if (navPage != null)
            {
                switch (navPage.CurrentPage)
                {
                    case TabbedPage tabbedPage:
                       await tabbedPage.Navigation.PushAsync(Activator.CreateInstance(pageType) as Page);
                        break;
                    case MasterDetailPage masterDetailPage:

                        break;
                    default:

                        break;
                }
            }
        }

        public Task NavigateToAsync(Type pageType, string key, object parameter)
        {
            return NavigateToAsync(pageType, new Dictionary<string, object>()
            {
                {key, parameter}
            });
        }

        public Task NavigateToModalAsync(Type page, IDictionary<string, object> navigationParameters)
        {
            throw new NotImplementedException();
        }

        public Task NavigateToModalAsync(Type page, string key, object parameter)
        {
            throw new NotImplementedException();
        }

        public Task GoBackAsync()
        {
            throw new NotImplementedException();
        }

        public Task GoBackAsync(Type page, IDictionary<string, object> navigationParameters)
        {
            throw new NotImplementedException();
        }

        public Task GoBackAsync(Type page, string key, object parameter)
        {
            throw new NotImplementedException();
        }
    }
}