using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinForms.Core.Services.NavigationService
{
    public interface INavigationService
    {
        Task NavigateToAsync(Type pageType, IDictionary<string, object> navigationParameters);
        Task NavigateToAsync(Type pageType, string key, object parameter);
        
        Task NavigateToModalAsync(Type page, IDictionary<string, object> navigationParameters);
        Task NavigateToModalAsync(Type page, string key, object parameter);

        Task GoBackAsync();
        Task GoBackAsync(Type page, IDictionary<string, object> navigationParameters);
        Task GoBackAsync(Type page, string key, object parameter);
    }
}