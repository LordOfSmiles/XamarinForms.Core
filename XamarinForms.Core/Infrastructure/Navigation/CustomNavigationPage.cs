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

        #region Fields

        //private readonly Stack<Page> _pages = new Stack<Page>();

        #endregion

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

            //_pages.Push(root);

            if (root != null)
            {
                var vm = root.BindingContext as ViewModelBase;
                vm?.OnAppearingAsync(NavigationState.GetParametersByPageType(root.GetType()));
            }
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
                var vm = prevPage?.BindingContext as ViewModelBase;
                vm?.OnDisappearing();
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
                for (int i = Navigation.NavigationStack.Count-1; i >=0; i--)
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
                
//                while (_pages.Count != 1)
//                {
//                    var page = _pages.Pop();
//                    var vm = page.BindingContext as ViewModelBase;
//                    if (vm != null)
//                    {
//                        vm.OnDisappearing();
//                        vm.Dispose();
//                    }
//
//                    page.BindingContext = null;
//                }
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
                if (vm != null)
                    vm.OnAppearingAsync(NavigationState.GetParametersByPageType(modalPage.GetType()));
            }

            if (Navigation.NavigationStack.Any())
            {
                var page = Navigation.NavigationStack.Last();
                var vm = page.BindingContext as ViewModelBase;
                if (vm != null)
                    vm.OnDisappearing();
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

                var vm = modalPage.BindingContext as ViewModelBase;
                if (vm != null)
                {
                    vm.OnDisappearing();
                    vm.Dispose();
                }

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
                            tabbedVm?.OnDisappearing();
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
        
        #region Private Methods

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
    }
}
