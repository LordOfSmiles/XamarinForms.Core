using Xamarin.Forms;

namespace XamarinForms.Core.Controls
{
    public sealed class CircleImageView:Frame
    {
        protected override void OnSizeAllocated(double width, double height)
        {
            if (!width.Equals(-1) && !height.Equals(-1))
            {
                CornerRadius = (float) (width / 2);
            }
            
            base.OnSizeAllocated(width, height);
        }

        #region Fields

        private readonly Image _img;
        
        #endregion
        
        public CircleImageView()
        {
            IsClippedToBounds = true;
            HasShadow = false;
            Padding = 0;

            _img = new Image()
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Aspect = Aspect.AspectFill
            };
            Content = _img;
        }
        
        #region Bindable Properties
        
        #region Image

        public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source),
            typeof(ImageSource),
            typeof(CircleImageView),
            null,
            propertyChanged: OnImageSourceChanged);

        public ImageSource Source
        {
            get => (ImageSource) GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        private static void OnImageSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as CircleImageView;
            if (ctrl == null)
                return;

            ctrl._img.Source = newValue as ImageSource;
        }

        #endregion

        #endregion
    }
}