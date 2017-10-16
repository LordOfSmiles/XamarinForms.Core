using Xamarin.Forms;

namespace NewXamarinForms.Core.Controls
{
    public partial class ValidationControlNew : ContentView
    {
        #region Fields

        private readonly ContentControl _cnvSuccess;
        private readonly ContentControl _cnvFail;

        #endregion

        public ValidationControlNew()
        {
            var grd = new Grid();

            _cnvSuccess = new ContentControl();
            _cnvFail = new ContentControl();
            grd.Children.Add(_cnvSuccess);
            grd.Children.Add(_cnvFail);

            Content = grd;
        }

        #region Bindable Properties

        #region IsValid

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(ValidationControlNew), true, propertyChanged: OnIsValidChanged);

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        private static void OnIsValidChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as ValidationControlNew;
            if (ctrl == null)
                return;

            var isValid = (bool)newvalue;
            if (isValid)
            {
                ctrl._cnvFail.IsVisible = false;
                ctrl._cnvSuccess.IsVisible = true;
            }
            else
            {
                ctrl._cnvFail.IsVisible = true;
                ctrl._cnvSuccess.IsVisible = false;
            }
        }

        #endregion

        #region ValidTemplate

        public static readonly BindableProperty ValidTemplateProperty = BindableProperty.Create(nameof(ValidTemplate), typeof(DataTemplate), typeof(ValidationControlNew), null, propertyChanged: OnSuccessTemplateChanged);

        public DataTemplate ValidTemplate
        {
            get => (DataTemplate)GetValue(ValidTemplateProperty);
            set => SetValue(ValidTemplateProperty, value);
        }

        private static void OnSuccessTemplateChanged(BindableObject bo, object oldValue, object newValue)
        {
            var ctrl = bo as ValidationControlNew;
            if (ctrl == null)
                return;

            ctrl._cnvSuccess.ContentTemplate = newValue as DataTemplate;
        }

        #endregion

        #region FailedTemplate

        public static readonly BindableProperty FailedTemplateProperty = BindableProperty.Create(nameof(FailedTemplate), typeof(DataTemplate), typeof(ValidationControlNew), null, propertyChanged: OnFailedTemplateChanged);

        public DataTemplate FailedTemplate
        {
            get => (DataTemplate)GetValue(FailedTemplateProperty);
            set => SetValue(FailedTemplateProperty, value);
        }

        private static void OnFailedTemplateChanged(BindableObject bo, object oldValue, object newValue)
        {
            var ctrl = bo as ValidationControlNew;
            if (ctrl == null)
                return;

            ctrl._cnvFail.ContentTemplate = newValue as DataTemplate;
        }

        #endregion

        #endregion
    }
}

