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

        #endregion

        #region Constructor

        protected ViewModelBase()
        {

        }

        #endregion
        
        #region Properties

        public bool IsAsyncOperationInProgress
        {
            get => _isAsyncOperationInProgress;
            protected set => SetProperty(ref _isAsyncOperationInProgress, value);
        }
        private bool _isAsyncOperationInProgress;
        
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

