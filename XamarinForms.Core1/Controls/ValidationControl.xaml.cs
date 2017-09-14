using Xamarin.Forms;

namespace XamarinForms.Core.Controls
{
    public partial class ValidationControl : ContentView
    {
        public ValidationControl()
        {
            InitializeComponent();

            //stkRoot.HeightRequest = GetHeight();

            //brdError.BackgroundColor = Device.OnPlatform(Color.FromHex("D40A14"), Color.FromHex("F22832"), Color.FromHex("F22832"));
            //   txtError.TextColor = Device.OnPlatform(Color.FromHex("D40A14"), Color.FromHex("F22832"), Color.FromHex("F22832"));
        }

        #region Bindable Properties

        //#region ErrorMEssage Property


        //public static readonly BindableProperty ErrorMessageProperty =
        //    BindableProperty.Create("ErrorMessage", typeof(string), typeof(ValidationControl), null, BindingMode.OneWay, propertyChanged: OnErrorMessageChanged);

        //public string ErrorMessage
        //{
        //    get { return (string)GetValue(ErrorMessageProperty); }
        //    set { SetValue(ErrorMessageProperty, value); }
        //}

        //private static void OnErrorMessageChanged(BindableObject bo, object oldValue, object newValue)
        //{
        //    var ctrl = bo as ValidationControl;
        //    if (ctrl == null)
        //        return;

        //    //ctrl.brdError.IsVisible = false;
        //    //ctrl.brdValid.IsVisible = false;
        //    ctrl.txtError.Text = "";


        //    string newString = "";
        //    if (newValue != null)
        //        newString = newValue.ToString();

        //    if (string.IsNullOrEmpty(newString))
        //    {
        //        //ctrl.brdValid.IsVisible = true;
        //    }
        //    else
        //    {
        //        //ctrl.brdError.IsVisible = true;
        //        ctrl.txtError.Text = newString;
        //    }

        //}

        //#endregion

        #region IsValid

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(ValidationControl), true, propertyChanged: OnIsValidChanged);

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        private static void OnIsValidChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as ValidationControl;
            if (ctrl == null)
                return;

            var isValid = (bool)newvalue;
            if (isValid)
            {
                ctrl.cnvFail.IsVisible = false;
                ctrl.cnvSuccess.IsVisible = true;
            }
            else
            {
                ctrl.cnvFail.IsVisible = true;
                ctrl.cnvSuccess.IsVisible = false;
            }
        }

        #endregion

        #region ValidTemplate

        public static readonly BindableProperty ValidTemplateProperty = BindableProperty.Create(nameof(ValidTemplate), typeof(DataTemplate), typeof(ValidationControl), null, propertyChanged: OnSuccessTemplateChanged);

        public DataTemplate ValidTemplate
        {
            get => (DataTemplate)GetValue(ValidTemplateProperty);
            set => SetValue(ValidTemplateProperty, value);
        }

        private static void OnSuccessTemplateChanged(BindableObject bo, object oldValue, object newValue)
        {
            var ctrl = bo as ValidationControl;
            if (ctrl == null)
                return;

            ctrl.cnvSuccess.ContentTemplate = newValue as DataTemplate;
        }

        #endregion

        #region FailedTemplate

        public static readonly BindableProperty FailedTemplateProperty = BindableProperty.Create(nameof(FailedTemplate), typeof(DataTemplate), typeof(ValidationControl), null, propertyChanged: OnFailedTemplateChanged);

        public DataTemplate FailedTemplate
        {
            get => (DataTemplate)GetValue(FailedTemplateProperty);
            set => SetValue(FailedTemplateProperty, value);
        }

        private static void OnFailedTemplateChanged(BindableObject bo, object oldValue, object newValue)
        {
            var ctrl = bo as ValidationControl;
            if (ctrl == null)
                return;

            ctrl.cnvFail.ContentTemplate = newValue as DataTemplate;
        }

        #endregion

        #endregion

        #region Private Methods

        static int GetHeight()
        {
            int result = 0;

            switch (Device.Idiom)
            {
                case TargetIdiom.Phone:
                    result = 25;
                    break;
                case TargetIdiom.Tablet:
                    result = 35;
                    break;
            }

            return result;
        }

        #endregion
    }
}

