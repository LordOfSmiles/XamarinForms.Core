using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
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
            return Task.FromResult<object>(null);
        }

        public virtual void OnDisappearing()
        {
        }

        #endregion

        #region Protected Methods

        protected void ShowAnimation(bool isVisible)
        {
            IsAnimationVisible = isVisible;
            IsUserInputDisabled = isVisible;
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

        #region Constructor

        #endregion

        #region Commands

        public ICommand CloseCommand => new Command(OnClose);

        #endregion

        #region Properties

        public bool IsUserInputDisabled
        {
            get => _isUserInputDisabled;
            protected set => SetProperty(ref _isUserInputDisabled, value);
        }
        private bool _isUserInputDisabled;

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