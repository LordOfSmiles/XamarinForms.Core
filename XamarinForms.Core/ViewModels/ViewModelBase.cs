﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Core.Infrastructure.Container;
using Xamarin.Core.Interfaces;
using Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinForms.Core.ViewModels
{
    public abstract class ViewModelBase : NotifyObject, IDisposable
    {
        public const string NeedRefreshDataKey = "NeedRefreshData";
        protected const string GoBackKey = "BackNavigation";

        #region Public Methods

        public virtual Task OnAppearingAsync(IDictionary<string, object> navigationParameters)
        {
            return Task.CompletedTask;
        }

        public virtual void OnDisappearing()
        {
        }

        #endregion

        #region Protected Methods

        protected void ShowAnimation(bool isVisible = true)
        {
            IsAnimationVisible = isVisible;
            InputTransparent = isVisible;
        }

        protected void HideAnimation()
        {
            IsAnimationVisible = false;
            InputTransparent = false;
        }

        protected async Task DisplayAlert(string title, string message, string cancel)
        {
            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.DisplayAlert(title, message, cancel);
            }
        }

        protected async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            var result = false;

            if (Application.Current?.MainPage != null)
            {
                result = await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
            }

            return result;
        }

        protected async Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            var result = string.Empty;

            if (Application.Current?.MainPage != null)
            {
                result = await Application.Current.MainPage.DisplayActionSheet(title, cancel, destruction, buttons);
            }

            return result;
        }

        protected virtual async void OnClose()
        {
            if (Shell.Current?.Navigation != null)
            {
                await Shell.Current.Navigation.PopAsync();
            }
        }

        #endregion

        #region Fields

        protected bool IsInitCompleted;

        #endregion

        #region Dependencies

        protected readonly ICrashlytics Crashlytics;
        protected readonly IAnalyticsService Analytics;

        #endregion

        #region Constructor

        protected ViewModelBase()
        {
            Crashlytics = FastContainer.TryResolve<ICrashlytics>();
            Analytics = FastContainer.TryResolve<IAnalyticsService>();
        }

        #endregion

        #region Commands

        public ICommand CloseCommand => new Command(OnClose);

        #endregion

        #region Properties

        public bool InputTransparent
        {
            get => _inputTransparent;
            protected set => SetProperty(ref _inputTransparent, value);
        }
        private bool _inputTransparent;

        public bool IsAnimationVisible
        {
            get => _isAnimationVisible;
            protected set => SetProperty(ref _isAnimationVisible, value);
        }
        private bool _isAnimationVisible;

        #endregion

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
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