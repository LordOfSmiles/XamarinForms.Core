﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForms.Core.ViewModels;

namespace XamarinForms.Core.Infrastructure.Navigation
{
    public sealed class CustomNavigationPage : NavigationPage
    {
        #region Fields

        private Page _lastPage;

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


            _lastPage = root;

            if (root != null)
            {
                var vm = root.BindingContext as ViewModelBase;
                vm?.OnAppearingAsync(NavigationState.GetParametersByPageType(root.GetType()));
            }
        }




        #region Handlers

        private void OnPushed(object sender, NavigationEventArgs e)
        {
            if (_lastPage != null)
            {
                var vm = _lastPage.BindingContext as ViewModelBase;
                vm?.OnDisappearingAsync();
            }

            if (e.Page != null)
            {
                var vm = e.Page.BindingContext as ViewModelBase;
                vm?.OnAppearingAsync(NavigationState.GetParametersByPageType(e.Page.GetType()));

                _lastPage = e.Page;
            }
        }

        private void OnPopped(object sender, NavigationEventArgs e)
        {
            if (e.Page != null)
            {
                var vm = e.Page.BindingContext as ViewModelBase;
                if (vm != null)
                {
                    vm.OnDisappearingAsync();
                    vm.OnPoppedAsync();
                }

                _lastPage = e.Page;
            }
        }

        private void OnPoppedToRoot(object sender, NavigationEventArgs e)
        {
            if (_lastPage != null)
            {
                var vm = _lastPage.BindingContext as ViewModelBase;
                vm?.OnDisappearingAsync();
                vm?.OnPoppedAsync();
            }

            if (e.Page != null)
            {
                var vm = e.Page.BindingContext as ViewModelBase;
                vm?.OnAppearingAsync(NavigationState.GetParametersByPageType(e.Page.GetType()));

                _lastPage = e.Page;
            }
        }

        private void CurrentOnModalPopped(object sender, ModalPoppedEventArgs e)
        {
            if (e.Modal != null)
            {
                var vm = e.Modal.BindingContext as ViewModelBase;
                vm?.OnDisappearingAsync();
                vm?.OnPoppedAsync();
            }
        }

        private void CurrentOnModalPushed(object sender, ModalPushedEventArgs e)
        {
            if (e.Modal != null)
            {
                var vm = e.Modal.BindingContext as ViewModelBase;
                vm?.OnAppearingAsync(NavigationState.GetParametersByPageType(e.Modal.GetType()));
            }
        }

        #endregion
    }
}
