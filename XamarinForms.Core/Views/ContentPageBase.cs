using Xamarin.Forms;
using XamarinForms.Core.Interfaces;
using XamarinForms.Core.ViewModels;

namespace XamarinForms.Core.Views
{
    public abstract class ContentPageBase : ContentPage, IPageBase
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

        protected virtual void BuildControls()
        {

        }

        #endregion

        #region Constrcutor

        protected ContentPageBase()
        {
            BuildControls();
        }

        #endregion

        #region ViewModel

        public ViewModelBase ViewModel => _viewModel ?? (_viewModel = BindingContext as ViewModelBase);
        private ViewModelBase _viewModel;

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
