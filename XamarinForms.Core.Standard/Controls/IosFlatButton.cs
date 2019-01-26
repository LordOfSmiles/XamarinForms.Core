using System;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinForms.Core.Standard.Extensions;

namespace XamarinForms.Core.Standard.Controls
{
    public sealed class IosFlatButton : StackLayout
    {
        #region Fields

        private View _viewContent;
        private Label _lbl;
        private Image _img;
        private Image _imgArrow;

        #endregion

        public IosFlatButton()
        {
            Spacing = 0;
            BackgroundColor = Color.Transparent;

            {
                var bxTop = new BoxView()
                {
                    HeightRequest = 1,
                    Color = Color.FromHex("#D7D7D7")
                };
                Children.Add(bxTop);
            }

            {
                var stkContent = new Grid()
                {
                    BackgroundColor = Color.White,
                    Padding = new Thickness(8, 6, 8, 6),
                    ColumnSpacing = 0
                };
                stkContent.ColumnDefinitions.Add(new ColumnDefinition() {Width = GridLength.Auto});
                stkContent.ColumnDefinitions.Add(new ColumnDefinition());
                stkContent.ColumnDefinitions.Add(new ColumnDefinition() {Width = GridLength.Auto});

                _img = new Image()
                {
                    HeightRequest = 25,
                    WidthRequest = 25,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = new Thickness(8, 0, 16, 0),
                    IsVisible = false
                };
                Grid.SetColumn(_img, 0);
                stkContent.Children.Add(_img);

                _lbl = new Label()
                {
                    TextColor = Color.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };
                Grid.SetColumn(_lbl, 1);
                stkContent.Children.Add(_lbl);
               
                
                _imgArrow=new Image()
                {
                    HeightRequest = 20,
                    WidthRequest = 20,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = new Thickness(8,0,8,0),
                    IsVisible = false
                };
                Grid.SetColumn(_imgArrow, 2);
                stkContent.Children.Add(_imgArrow);
                
                Children.Add(stkContent);
                _viewContent = stkContent;
            }


            {
                var bxBottom = new BoxView()
                {
                    HeightRequest = 1,
                    Color = Color.FromHex("#D7D7D7")
                };
                Children.Add(bxBottom);
            }

            {
                var gesture = new TapGestureRecognizer();
                gesture.Tapped += GestureOnTapped;
                GestureRecognizers.Add(gesture);
            }
           
        }

        #region Bindable Properties

        #region Text

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(IosFlatButton), string.Empty, propertyChanged: OnTextChanded);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void OnTextChanded(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as IosFlatButton;
            if (ctrl == null)
                return;

            ctrl._lbl.Text = newvalue?.ToString() ?? string.Empty;
        }

        #endregion

        #region TextColor

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(IosFlatButton), Color.Black, propertyChanged: OnTextColorChanged);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        private static void OnTextColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as IosFlatButton;
            if (ctrl == null)
                return;

            ctrl._lbl.TextColor = (Color)newvalue;
        }

        #endregion

        #region Image

        public static readonly BindableProperty ImageProperty = BindableProperty.Create(nameof(Image), typeof(ImageSource), typeof(IosFlatButton), null, propertyChanged: OnImageChanged);

        public ImageSource Image
        {
            get => (ImageSource)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        private static void OnImageChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as IosFlatButton;
            if (ctrl == null)
                return;

            var image = newvalue as ImageSource;
            if (image != null)
            {
                ctrl._img.Source = image;
                ctrl._img.IsVisible = true;
                ctrl._lbl.HorizontalOptions = LayoutOptions.Start;
            }
            else
            {
                ctrl._img.Source = null;
                ctrl._img.IsVisible = false;
                ctrl._lbl.HorizontalOptions = LayoutOptions.Center;
            }
        }

        #endregion
        
        #region BackImage
        
        public static readonly BindableProperty BackImageProperty = BindableProperty.Create(nameof(BackImage), typeof(ImageSource), typeof(IosFlatButton), null, propertyChanged: OnBackImageChanged);

        public ImageSource BackImage
        {
            get => (ImageSource)GetValue(BackImageProperty);
            set => SetValue(BackImageProperty, value);
        }

        private static void OnBackImageChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as IosFlatButton;
            if (ctrl == null)
                return;

            var image = newvalue as ImageSource;
            if (image != null)
            {
                ctrl._imgArrow.Source = image;
                ctrl._imgArrow.IsVisible = true;
                ctrl._lbl.HorizontalOptions = LayoutOptions.Start;
            }
            else
            {
                ctrl._imgArrow.Source = null;
                ctrl._imgArrow.IsVisible = false;
                ctrl._lbl.HorizontalOptions = LayoutOptions.FillAndExpand;
            }
        }
        
        #endregion

        #region Command

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(IosFlatButton));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        #endregion

        #region CommandParameter

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(IosFlatButton));

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        #endregion

        #endregion

        #region Handlers

        private async void GestureOnTapped(object sender, EventArgs e)
        {
            await _viewContent.ColorTo(Color.FromHex("#D7D7D7"), 100);
            Command?.Execute(CommandParameter);
            await _viewContent.ColorTo(Color.White, 100);
        }

        #endregion
    }
}