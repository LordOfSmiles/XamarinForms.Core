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
            NeedWatchPropertyChanged = true;

            return Task.FromResult<object>(null);
        }

        public virtual void OnDisappearing()
        {
            NeedWatchPropertyChanged = false;
        }

        protected virtual void OnElementPropertyChanged(string propertyName)
        {

        }

        #endregion

#region Fields

protected bool IsNavigationInProgress = false;

#endregion

        #region Constructor

        protected ViewModelBase()
        {
            PropertyChanged += ViewModelBase_PropertyChanged;
        }

        #endregion

        #region Properties

        protected bool NeedWatchPropertyChanged { get; set; } = true;

        #endregion

        #region Handlers

        private void ViewModelBase_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (NeedWatchPropertyChanged)
                OnElementPropertyChanged(e.PropertyName);
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                PropertyChanged -= ViewModelBase_PropertyChanged;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

