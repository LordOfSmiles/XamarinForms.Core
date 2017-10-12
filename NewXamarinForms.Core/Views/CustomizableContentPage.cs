using Xamarin.Forms;

namespace NewXamarinForms.Core.Views
{
    public abstract class CustomizableContentPage : ContentPage
    {
        #region Protected Methods

        protected void OnCustomIdiom()
        {
            switch (Device.Idiom)
            {
                case TargetIdiom.Phone:
                    PhoneIdiom();
                    break;
                case TargetIdiom.Tablet:
                    TabletIdiom();
                    break;
                case TargetIdiom.Desktop:
                    DesktopIdiom();
                    break;
            }
        }

        #endregion

        #region Virtual Methods

        protected virtual void OnPhone()
        {

        }

        protected virtual void OnTablet()
        {

        }

        protected virtual void OnDesktop()
        {

        }

        #endregion

        #region Private Methods

        private void PhoneIdiom()
        {
            OnPhone();
        }

        private void TabletIdiom()
        {
            OnTablet();
        }

        private void DesktopIdiom()
        {
            OnDesktop();
        }

        #endregion
    }


}
