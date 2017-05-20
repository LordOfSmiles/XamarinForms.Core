using Xamarin.Forms;

namespace XamarinForms.Core.Controls
{
    public class ContentControl : ContentView
    {
        public static readonly BindableProperty ContentTemplateProperty = BindableProperty.Create(nameof(ContentTemplate), typeof(DataTemplate), typeof(ContentControl), null, propertyChanged: OnContentTemplateChanged);

        public DataTemplate ContentTemplate
        {
            get
            {
                return (DataTemplate)GetValue(ContentTemplateProperty);
            }
            set
            {
                SetValue(ContentTemplateProperty, value);
            }
        }

        private static void OnContentTemplateChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var cp = (ContentControl)bindable;

            var template = cp.ContentTemplate;
            if (template != null)
            {
                cp.Content = (View)template.CreateContent();
            }
            else
            {
                cp.Content = null;
            }
        }

    }

}
