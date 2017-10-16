using System.Threading.Tasks;
using NewXamarinForms.Core.Infrastructure.Navigation;
using Xamarin.Core.ViewModels;
using Xamarin.Forms;

namespace NewXamarinForms.Core.Views
{
    public class MasterDetailPageBase : MasterDetailPage
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
