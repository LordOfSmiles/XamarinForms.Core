using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinForms.Core.Controls
{
    public sealed class FloatingActionButtonView : View
    {
        #region Bindable Proeprties

        #region Command

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(FloatingActionButtonView));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        #endregion

        #region Icon

        public static readonly BindableProperty ImageProperty = BindableProperty.Create(
            nameof(Image), 
            typeof(ImageSource),
            typeof(FloatingActionButtonView));

        public ImageSource Image
        {
            get => (ImageSource)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        #endregion

        #region ColorNormal

        public static readonly BindableProperty ColorNormalProperty = BindableProperty.Create("ColorNormal", typeof(Color), typeof(FloatingActionButtonView), Color.White);

        public Color ColorNormal
        {
            get => (Color)GetValue(ColorNormalProperty);
            set => SetValue(ColorNormalProperty, value);
        }

        #endregion

        #region ColorPressed

        public static readonly BindableProperty ColorPressedProperty = BindableProperty.Create("ColorPressed", typeof(Color), typeof(FloatingActionButtonView), Color.White);

        public Color ColorPressed
        {
            get => (Color)GetValue(ColorPressedProperty);
            set => SetValue(ColorPressedProperty, value);
        }

        #endregion

        #region ColorRipple

        public static readonly BindableProperty ColorRippleProperty = BindableProperty.Create("ColorRipple", typeof(Color), typeof(FloatingActionButtonView), Color.White);

        public Color ColorRipple
        {
            get => (Color)GetValue(ColorRippleProperty);
            set => SetValue(ColorRippleProperty, value);
        }

        #endregion

        #region Size

        public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(FloatingActionButtonSize), typeof(FloatingActionButtonView), FloatingActionButtonSize.Normal);

        public FloatingActionButtonSize Size
        {
            get => (FloatingActionButtonSize)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        #endregion

        #region HasShadow

        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create("HasShadow", typeof(bool), typeof(FloatingActionButtonView), true);

        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        #endregion

        #endregion

        #region Delegates

        public delegate void ShowHideDelegate(bool animate = true);
        public delegate void AttachToListViewDelegate(ListView listView);

        public ShowHideDelegate Show { get; set; }
        public ShowHideDelegate Hide { get; set; }
        public Action<object, EventArgs> Clicked { get; set; }

        #endregion
    }

    public enum FloatingActionButtonSize
    {
        Normal,
        Mini
    }


}
