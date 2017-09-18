using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForms.Core.Infrastructure.Navigation;
using XamarinForms.Core.Interfaces;
using XamarinForms.Core.ViewModels;

namespace XamarinForms.Core.Views
{
    public abstract class TabbedPageBase : TabbedPage
    {
        #region Overrides

        /// <inheritdoc />
        protected override void OnAppearing()
        {
            var vm = BindingContext as ViewModelBase;
            if (vm != null)
            {
                var currentType = GetType();

                var parameters = NavigationState.GetParametersByPageType(currentType);

                vm.OnAppearingAsync(parameters);
            }

            OnAppearingImplementationAsync();

            base.OnAppearing();
        }

        /// <inheritdoc />
        protected override void OnDisappearing()
        {
            var vm = BindingContext as ViewModelBase;
            vm?.OnDisappearing();

            OnDisappearingImplementationAsync();

            base.OnDisappearing();
        }

        #endregion

        #region Constrcutor

        protected TabbedPageBase()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    BackgroundColor = Color.White;
                    break;
                case Device.iOS:
                    NavigationPage.SetBackButtonTitle(this, "");
                    break;
            }
        }

        #endregion

        #region Virtual Methods

        public virtual Task OnAppearingImplementationAsync()
        {
            return Task.FromResult<object>(null);
        }

        public virtual Task OnDisappearingImplementationAsync()
        {
            return Task.FromResult<object>(null);
        }

        #endregion
    }
}
