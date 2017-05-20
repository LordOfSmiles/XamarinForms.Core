using Xamarin.Forms;
using XamarinForms.Core.ViewModels;

namespace XamarinForms.Core.Views
{
    public abstract class PageBase<T> : Page
        where T : ViewModelBase
    {
        #region Virtial Methods

        protected virtual void OnAppearingImplementation()
        {
            ViewModel?.OnAppearing();
        }

        protected virtual void OnDisappearingImplementation()
        {
            ViewModel?.OnDisappearing();
        }

        #endregion

        #region ViewModel

        public T ViewModel => _viewModel ?? (_viewModel = BindingContext as T);
        private T _viewModel;

        #endregion

        #region Navigation

        protected override void OnAppearing()
        {
            base.OnAppearing();

            OnAppearingImplementation();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            OnDisappearingImplementation();
        }

        #endregion
    }
}
