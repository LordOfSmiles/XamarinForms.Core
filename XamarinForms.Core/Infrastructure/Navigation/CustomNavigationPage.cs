using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using System;
using Xamarin.Core.ViewModels;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Application = Xamarin.Forms.Application;
using NavigationPage = Xamarin.Forms.NavigationPage;
using Page = Xamarin.Forms.Page;

namespace XamarinForms.Core.Standard.Infrastructure.Navigation
{
    public class CustomNavigationPage : NavigationPage, IDisposable
    {
        public void ChangeAppBarColor(Color backgroundColor)
        {
            BarBackgroundColor = backgroundColor;
        }

        public void ChangeAppBarTextColor(Color textColor)
        {
            BarTextColor = textColor;
        }

        public CustomNavigationPage(Page root, bool isModal = false)
            : base(root)
        {
            if (isModal)
            {
                return;
            }

            Pushed += OnPushed;
            Popped += OnPopped;
            PoppedToRoot += OnPoppedToRoot;

            if (Application.Current != null)
            {
                Application.Current.ModalPushed += CurrentOnModalPushed;
                Application.Current.ModalPopped += CurrentOnModalPopped;
            }

            AppearViewModel(root);
        }

        #region Handlers

        private void OnPushed(object sender, NavigationEventArgs e)
        {
            if (Navigation.NavigationStack.Any())
            {
                var prevPageIndex = Navigation.NavigationStack.Count - 1;
                if (prevPageIndex < 0)
                    prevPageIndex = 0;
                
                var prevPage = Navigation.NavigationStack.ElementAtOrDefault(prevPageIndex);
                if (prevPage != null)
                    DisappearViewModel(prevPage);
            }

            if (e.Page != null)
            {
                var vm = e.Page.BindingContext as ViewModelBase;
                vm?.OnAppearingAsync(NavigationState.GetParametersByPageType(e.Page.GetType()));
            }
        }

        private void OnPopped(object sender, NavigationEventArgs e)
        {
            var poppedPage = e.Page;
            ViewModelBase poppedPageVm = null;
            switch (poppedPage)
            {
                case TabbedPage tabbedPage:

                    break;
                case Page p:
                    poppedPageVm = p.BindingContext as ViewModelBase;
                    break;
            }

            if (poppedPageVm != null)
            {
                poppedPageVm.OnDisappearing();
                poppedPageVm.Dispose();
            }

            if (poppedPage != null)
                poppedPage.BindingContext = null;
            
            
            if (Navigation.NavigationStack.Any())
            {
                var pageToBack = Navigation.NavigationStack.Last();
                
                ViewModelBase vm = null;
                Type pageType = null;

                switch (pageToBack)
                {
                    case TabbedPage tabbedPage:
                        vm = tabbedPage.CurrentPage?.BindingContext as ViewModelBase;
                        pageType = tabbedPage.CurrentPage?.GetType();
                        break;
                    case Page p:
                        vm = p.BindingContext as ViewModelBase;
                        pageType = p.GetType();
                        break;
                }

                vm?.OnAppearingAsync(NavigationState.GetParametersByPageType(pageType));
            }
        }
        
        private void OnPoppedToRoot(object sender, NavigationEventArgs e)
        {
            if (Navigation.NavigationStack.Count > 1)
            {
                for (var i = Navigation.NavigationStack.Count-1; i >=0; i--)
                {
                    var page = Navigation.NavigationStack[i];
                    var vm = page.BindingContext as ViewModelBase;
                    if (vm != null)
                    {
                        vm.OnDisappearing();
                        vm.Dispose();
                    }

                    page.BindingContext = null;
                }
            }

            if (e.Page != null)
            {
                var vm = e.Page.BindingContext as ViewModelBase;
                vm?.OnAppearingAsync(NavigationState.GetParametersByPageType(e.Page.GetType()));
            }
        }

        private void CurrentOnModalPushed(object sender, ModalPushedEventArgs e)
        {
            if (e.Modal != null)
            {
                Page modalPage = null;

                if (e.Modal is NavigationPage navPage)
                {
                    modalPage = navPage.CurrentPage;
                }
                else
                {
                    modalPage = e.Modal;
                }

                var vm = modalPage.BindingContext as ViewModelBase;
                vm?.OnAppearingAsync(NavigationState.GetParametersByPageType(modalPage.GetType()));
            }

            if (Navigation.NavigationStack.Any())
            {
                var lastPageOnScreen = Navigation.NavigationStack.Last();
                if (lastPageOnScreen is TabbedPage tabbedPage)
                {
                    foreach (var tabbedPageChild in tabbedPage.Children)
                    {
                        DisappearViewModel(tabbedPageChild);
                    }
                }
               
                DisappearViewModel(lastPageOnScreen);
            }
        }

        private void CurrentOnModalPopped(object sender, ModalPoppedEventArgs e)
        {
            if (e.Modal != null)
            {
                Page modalPage = null;

                if (e.Modal is NavigationPage navPage)
                {
                    modalPage = navPage.CurrentPage;
                }
                else
                {
                    modalPage = e.Modal;
                }

                ClearBindingContext(modalPage);
                e.Modal.BindingContext = null;
            }

            if (Navigation.NavigationStack.Any())
            {
                var page = Navigation.NavigationStack.Last();

                ViewModelBase vm = null;
                Type pageType = null;

                switch (page)
                {
                    case TabbedPage tabbedPage:
                        vm = tabbedPage.CurrentPage?.BindingContext as ViewModelBase;
                        pageType = tabbedPage.CurrentPage?.GetType();
                        break;
                    case Page p:
                        vm = page.BindingContext as ViewModelBase;
                        pageType = page.GetType();
                        break;
                }

                vm?.OnAppearingAsync(NavigationState.GetParametersByPageType(pageType));
            }
        }

        #endregion
        
        #region Private Methods

        private static void AppearViewModel(Page page)
        {
            if (page == null)
                return;

            if (page is NavigationPage navPage)
            {
                AppearViewModel(navPage.CurrentPage);
            }
            else if (page is MasterDetailPage masterDetailPage)
            {
                AppearViewModel(masterDetailPage.Detail);
                AppearViewModel(masterDetailPage.Master);
            }
            else if (page is TabbedPage tabbedPage)
            {
                if (tabbedPage.Children.Any())
                {
                    AppearViewModel(tabbedPage.Children.First());
                }
            }
            else
            {
                var viewModel = page.BindingContext as ViewModelBase;
                viewModel?.OnAppearingAsync(NavigationState.GetParametersByPageType(page.GetType()));
            }
        }

        private static void DisappearViewModel(Page page)
        {
            var vm = page.BindingContext as ViewModelBase;
            vm?.OnDisappearing();
        }

        private static void ClearBindingContext(Page page)
        {
            var vm = page.BindingContext as ViewModelBase;
            if (vm != null)
            {
                vm.OnDisappearing();
                vm.Dispose();
            }
            page.BindingContext = null;
        }
        
        #endregion

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (Navigation?.NavigationStack != null)
            {
                foreach (var page in Navigation.NavigationStack)
                {
                    switch (page)
                    {
                        case TabbedPage tabbedPage:
                            foreach (var tabbedPageChild in tabbedPage.Children)
                            {
                                ClearBindingContext(tabbedPageChild);
                            }
                            var tabbedVm = tabbedPage.BindingContext as ViewModelBase;
                            if (tabbedVm != null)
                            {
                                tabbedVm.OnDisappearing();
                                tabbedVm.Dispose();
                            }
                            tabbedPage.BindingContext = null;
                            break;
                        case Page p:
                            ClearBindingContext(p);
                            break;
                    }
                }
            }

            if (disposing)
            {
                Pushed -= OnPushed;
                Popped -= OnPopped;
                PoppedToRoot -= OnPoppedToRoot;

                if (Application.Current != null)
                {
                    Application.Current.ModalPushed -= CurrentOnModalPushed;
                    Application.Current.ModalPopped -= CurrentOnModalPopped;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
