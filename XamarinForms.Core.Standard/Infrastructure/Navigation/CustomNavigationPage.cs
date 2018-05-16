using System.Collections.Generic;
using Xamarin.Core.Standard.ViewModels;
using Xamarin.Forms;
using System.Linq;
using System;

namespace XamarinForms.Core.Standard.Infrastructure.Navigation
{
    public class CustomNavigationPage : NavigationPage
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

        private DateTime _lastPop;

        #endregion

        public CustomNavigationPage(Page root)
            : base(root)
        {
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
            //костыль. Popped срабатывает 2 раза на версии 2.5.1. Проверить на других версиях
            if ((DateTime.Now - _lastPop).TotalSeconds <= 1)
                return;

            _lastPop = DateTime.Now;

            if (_pages.Count > 1)
            {
                var vm = _pages.Pop().BindingContext as ViewModelBase;
                if (vm != null)
                {
                    vm.OnDisappearing();
                    vm.OnPopped();
                }
            }

            if (_pages.Any())
            {
                var page = _pages.Peek();
                var vm = page.BindingContext as ViewModelBase;
                if (vm != null)
                {
                    vm.OnAppearingAsync(NavigationState.GetParametersByPageType(page.GetType()));
                }
            }


        }

        private void OnPoppedToRoot(object sender, NavigationEventArgs e)
        {
            if (_pages.Count > 1)
            {
                while (_pages.Count != 1)
                {
                    var vm = _pages.Pop().BindingContext as ViewModelBase;
                    vm?.OnDisappearing();
                    vm?.OnPopped();
                }
            }

            if (e.Page != null)
            {
                var vm = e.Page.BindingContext as ViewModelBase;
                if (vm != null)
                    vm.OnAppearingAsync(NavigationState.GetParametersByPageType(e.Page.GetType()));
            }
        }

        private void CurrentOnModalPopped(object sender, ModalPoppedEventArgs e)
        {
            if (e.Modal != null)
            {
                var vm = e.Modal.BindingContext as ViewModelBase;
                vm?.OnDisappearing();
                vm?.OnPopped();
            }
        }

        private void CurrentOnModalPushed(object sender, ModalPushedEventArgs e)
        {
            if (e.Modal != null)
            {
                var vm = e.Modal.BindingContext as ViewModelBase;
                if (vm != null)
                    vm.OnAppearingAsync(NavigationState.GetParametersByPageType(e.Modal.GetType()));
            }
        }

        #endregion
    }
}
