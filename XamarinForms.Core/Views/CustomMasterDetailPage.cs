using System;
using Xamarin.Forms;
using XamarinForms.Core.Standard.Infrastructure.Navigation;
using XamarinForms.Core.ViewModels;

namespace XamarinForms.Core.Standard.Views
{
    public abstract class CustomMasterDetailPage:MasterDetailPage
    {
        protected bool NeedChangeDetail(Type detailType)
        {
            var result = false;
            
            var navPage = Detail as NavigationPage;
            if (navPage?.CurrentPage.GetType() != detailType)
            {
                result = true;
            }
            
            return result;
        }
        
        protected void ClearLastPage()
        {
            var navPage = Detail as CustomNavigationPage;
            if (navPage != null)
            {
                if (navPage.CurrentPage is TabbedPage tabbedPage)
                {
                    foreach (var page in tabbedPage.Children)
                    {
                        ClearBindingContext(page);
                    }
                }
                else
                {
                    ClearBindingContext(navPage.CurrentPage);
                }
                
                navPage.Dispose();
            }
        }

        protected void ClearBindingContext(Page page)
        {
            if (page == null)
                return;
            
            var vm = page.BindingContext as ViewModelBase;
            if (vm != null)
            {
                vm.Dispose();
            }
            
            page.BindingContext = null;
        }
    }
}