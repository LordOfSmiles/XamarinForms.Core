using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForms.Core.Infrastructure.Navigation;
using XamarinForms.Core.Interfaces;
using XamarinForms.Core.ViewModels;

namespace XamarinForms.Core.Views
{
    public abstract class ContentPageBase : ContentPage
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
            vm?.OnDisappearingAsync();

            OnDisappearingImplementationAsync();

            base.OnDisappearing();
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
