using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Core.Standard.Models;
using Xamarin.Core.Standard.ViewModels.ErrorValidation;

namespace Xamarin.Core.Standard.ViewModels
{
    public abstract class ViewModelBase : NotifyObject
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

        public virtual void OnPopped()
        {
            NeedWatchPropertyChanged = false;
        }

        protected virtual void OnElementPropertyChanged(string propertyName)
        {

        }

        #endregion

        #region Constructor

        protected ViewModelBase()
        {
            PropertyChanged += ViewModelBase_PropertyChanged;
        }

        ~ViewModelBase()
        {
            PropertyChanged -= ViewModelBase_PropertyChanged;
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
    }
}

