using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForms.Core.Interfaces;

namespace XamarinForms.Core.Infrastructure
{
    public static class NavigationManager
    {
        #region Public Methods

        public static void Init(INavigation navigation)
        {
            _navigation = navigation;
        }

        public static async Task GoToAsync(Type pageType, IDictionary<string, object> values)
        {
            var page = await GetPageAsync(pageType, values);

            if (page != null)
            {
                await _navigation.PushAsync(page);
            }
        }

        public static async Task GoBackAsync(IDictionary<string, object> values)
        {
            await _navigation.PopAsync();

            var page = _navigation.NavigationStack.Last() as IPageBase;
            if (page != null)
            {
                await page.ViewModel.InitViewModelAsync(values);
            }
        }

        public static async Task<Page> GetPageAsync(Type pageType, IDictionary<string, object> values)
        {
            Page page = null;
            IPageBase pageBase = null;

            if (ViewCache.ContainsKey(pageType))
            {
                page = ViewCache[pageType];
                pageBase = page as IPageBase;
            }
            else
            {
                var x = Activator.CreateInstance(pageType);
                pageBase = x as IPageBase;
                page = x as Page;
            }

            if (pageBase?.ViewModel != null)
            {
                await pageBase.ViewModel.InitViewModelAsync(values);
            }

            return page;
        }

        #endregion

        #region Fields

        private static readonly Dictionary<Type, Page> ViewCache = new Dictionary<Type, Page>();

        #endregion

        #region Dependencies

        private static INavigation _navigation;

        #endregion
    }
}
