using System.Collections.Generic;
using Xamarin.Core.Standard.ViewModels;
using Xamarin.Forms;
using System.Linq;
using System;

namespace XamarinForms.Core.Standard.Infrastructure.Navigation
{
    public abstract class CustomNavigationPage : NavigationPage
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

        private readonly Stack<Page> _pages = new Stack<Page>();

        #endregion

        protected CustomNavigationPage(Page root, bool isModal = false)
            : base(root)
        {
            if (isModal)
                return;

            Pushed += OnPushed;
            Popped += OnPopped;
            PoppedToRoot += OnPoppedToRoot;

            if (Application.Current != null)
            {
                Application.Current.ModalPushed += CurrentOnModalPushed;
                Application.Current.ModalPopped += CurrentOnModalPopped;
            }

            _pages.Push(root);

            if (root != null)
            {
                var vm = root.BindingContext as ViewModelBase;
                vm?.OnAppearingAsync(NavigationState.GetParametersByPageType(root.GetType()));
            }
        }

        #region Handlers

        private void OnPushed(object sender, NavigationEventArgs e)
        {
            if (_pages.Any())
            {
                var vm = _pages.Peek().BindingContext as ViewModelBase;
                vm?.OnDisappearing();
            }

            if (e.Page != null)
            {
                var vm = e.Page.BindingContext as ViewModelBase;
                if (vm != null)
                    vm.OnAppearingAsync(NavigationState.GetParametersByPageType(e.Page.GetType()));

                _pages.Push(e.Page);
            }
        }

        private void OnPopped(object sender, NavigationEventArgs e)
        {
            if (_pages.Count > 1)
            {
                var page = _pages.Pop();
                var vm = page.BindingContext as ViewModelBase;
                if (vm != null)
                {
                    vm.OnDisappearing();
                    vm.OnPopped();
                    vm.Dispose();
                }

                page.BindingContext = null;
            }

            if (_pages.Any())
            {
                var page = _pages.Peek();

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

                if (vm != null)
                    vm.OnAppearingAsync(NavigationState.GetParametersByPageType(pageType));
            }
        }

        private void OnPoppedToRoot(object sender, NavigationEventArgs e)
        {
            if (_pages.Count > 1)
            {
                while (_pages.Count != 1)
                {
                    var page = _pages.Pop();
                    var vm = page.BindingContext as ViewModelBase;
                    if (vm != null)
                    {
                        vm.OnDisappearing();
                        vm.OnPopped();
                        vm.Dispose();
                    }

                    page.BindingContext = null;
                }
            }

            if (e.Page != null)
            {
                var vm = e.Page.BindingContext as ViewModelBase;
                if (vm != null)
                    vm.OnAppearingAsync(NavigationState.GetParametersByPageType(e.Page.GetType()));
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

            if (_pages.Any())
            {
                var page = _pages.Peek();
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
                    vm.OnPopped();
                    vm.Dispose();
                }

                e.Modal.BindingContext = null;
            }

            if (_pages.Any())
            {
                var page = _pages.Peek();

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

                if (vm != null)
                    vm.OnAppearingAsync(NavigationState.GetParametersByPageType(pageType));
            }
        }

        #endregion
    }
}
