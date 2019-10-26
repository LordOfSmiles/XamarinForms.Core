using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Core.Models;

namespace Xamarin.Core.ViewModels
{
    public abstract class ViewModelBase : NotifyObject, IDisposable
    {
        public const string NeedRefreshDataKey = "NeedRefreshData";

        #region Public Methods

        public virtual Task OnAppearingAsync(IDictionary<string, object> navigationParameters)
        {
            return Task.FromResult<object>(null);
        }

        public virtual void OnDisappearing()
        {

        }

        protected virtual void OnElementPropertyChanged(string propertyName)
        {

        }

        protected void AddRemovePropertyChangedHandler(bool needEnable)
        {
            if (needEnable)
            {
                PropertyChanged += ViewModelBase_PropertyChanged;
            }
            else
            {
                PropertyChanged -= ViewModelBase_PropertyChanged;
            }
        }

        #endregion

        #region Fields

        protected bool IsNavigationInProgress = false;

        #endregion

        #region Constructor

        protected ViewModelBase()
        {

        }

        #endregion

        #region Handlers

        private void ViewModelBase_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnElementPropertyChanged(e.PropertyName);
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                AddRemovePropertyChangedHandler(false);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

