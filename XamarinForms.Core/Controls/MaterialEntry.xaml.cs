using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinForms.Core.Controls
{
    public partial class MaterialEntry
    {
        #region Public Methods

        public void SetFocus()
        {
            txt?.Focus();
        }
        
        #endregion

        public MaterialEntry()
        {
            InitializeComponent();

            //Todo убрать
            txt.BindingContext = this;

            txt.Focused += OnTxtFocused;
            txt.Unfocused += OnTxtUnfocused;

            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => txt.Focus())
            });
        }

        public event EventHandler<TappedEventArgs> TrailingIconTapped;

        #region Bindable Properties

        #region Text

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(MaterialEntry),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: OnTextChanged);

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as MaterialEntry;
            if (ctrl == null)
                return;

            if (newValue != null)
            {
                var input = newValue.ToString();
                if (!string.IsNullOrEmpty(input))
                {
                    ctrl.txt.IsVisible = true;
                    ctrl.lblSmallPlaceholder.IsVisible = true;
                    ctrl.lblBigPlaceholder.IsVisible = false;
                }
                else
                {
                    ctrl.txt.IsVisible = false;
                    ctrl.lblSmallPlaceholder.IsVisible = false;
                    ctrl.lblBigPlaceholder.IsVisible = true;
                }
            }
            else
            {
                ctrl.txt.IsVisible = false;
                ctrl.lblSmallPlaceholder.IsVisible = false;
                ctrl.lblBigPlaceholder.IsVisible = true;
            }
        }

        #endregion

        #region Placeholder

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(MaterialEntry),
            propertyChanged: OnPlaceholderChanged);

        public string Placeholder
        {
            get => (string) GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        private static void OnPlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as MaterialEntry;
            if (ctrl == null)
                return;

            var placeholder = newValue.ToString() ?? "";

            ctrl.lblBigPlaceholder.Text = placeholder;
            ctrl.lblSmallPlaceholder.Text = placeholder;
        }

        #endregion

        #region AccentColor

        public static readonly BindableProperty AccentColorProperty = BindableProperty.Create(
            nameof(AccentColor),
            typeof(Color),
            typeof(MaterialEntry),
            Color.Black);

        public Color AccentColor
        {
            get => (Color) GetValue(AccentColorProperty);
            set => SetValue(AccentColorProperty, value);
        }

        private static void OnAccentColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as MaterialEntry;
            if (ctrl == null)
                return;

            ctrl.brdFocused.Color = (Color) newValue;
        }

        #endregion

        #region EntryFontSize

        public static readonly BindableProperty EntryFontSizeProperty = BindableProperty.Create(
            nameof(EntryFontSize),
            typeof(double),
            typeof(MaterialEntry),
            16d,
            propertyChanged: OnEntryFontSizeChanged);

        public double EntryFontSize
        {
            get => (double) GetValue(EntryFontSizeProperty);
            set => SetValue(EntryFontSizeProperty, value);
        }

        private static void OnEntryFontSizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as MaterialEntry;
            if (ctrl == null)
                return;

            var fontSize = (double) newValue;

            ctrl.lblBigPlaceholder.FontSize = fontSize;
            ctrl.txt.FontSize = fontSize;
        }

        #endregion

        #region EntryMaxLength

        public static readonly BindableProperty EntryMaxLengthProperty = BindableProperty.Create(
            nameof(EntryMaxLength),
            typeof(int),
            typeof(MaterialEntry),
            int.MaxValue,
            propertyChanged: OnEntryMaxLengthChanged);

        public int EntryMaxLength
        {
            get => (int) GetValue(EntryMaxLengthProperty);
            set => SetValue(EntryMaxLengthProperty, value);
        }

        private static void OnEntryMaxLengthChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as MaterialEntry;
            if (ctrl == null)
                return;

            ctrl.txt.MaxLength = (int) newValue;
        }

        #endregion

        #region HasValidationErrors

        public static readonly BindableProperty HasValidationErrorsProperty = BindableProperty.Create(
            nameof(HasValidationErrors),
            typeof(bool),
            typeof(MaterialEntry),
            false,
            propertyChanged: OnHasValidationErrorsChanged);

        public bool HasValidationErrors
        {
            get => (bool) GetValue(HasValidationErrorsProperty);
            set => SetValue(HasValidationErrorsProperty, value);
        }

        private static void OnHasValidationErrorsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as MaterialEntry;
            if (ctrl == null)
                return;

            var hasErrors = (bool) newValue;
            if (!hasErrors)
            {
                ctrl.brdFocused.Color = ctrl.AccentColor;
            }
            else
            {
                ctrl.brdFocused.Color = Color.Red;
            }
        }

        #endregion
        
        #region ReturnKeyCommand

        public static readonly BindableProperty ReturnCommandProperty=BindableProperty.Create(
            nameof(ReturnCommand),
            typeof(ICommand),
            typeof(MaterialEntry),
            propertyChanged:OnReturnCommandChanged);

        public ICommand ReturnCommand
        {
            get => (ICommand) GetValue(ReturnCommandProperty);
            set => SetValue(ReturnCommandProperty, value);
        }

        private static void OnReturnCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as MaterialEntry;
            if (ctrl == null)
                return;

            ctrl.txt.ReturnCommand = newValue as ICommand;
        }
        
        #endregion

        #endregion

        #region Properties
        
        public Keyboard Keyboard
        {
            set => txt.Keyboard = value;
        }

        public ReturnType ReturnType
        {
            set => txt.ReturnType = value;
        }

        #endregion

        #region Handlers

        private async void OnTxtFocused(object sender, FocusEventArgs e)
        {
            lblSmallPlaceholder.IsVisible = true;
            lblBigPlaceholder.IsVisible = false;

            if (string.IsNullOrEmpty(txt.Text))
            {
                txt.Placeholder = null;
                txt.IsVisible = true;

                // animate both at the same time
                await Task.WhenAll(
                    brdFocused.LayoutTo(new Rectangle(brdUnfocused.X, brdUnfocused.Y, brdUnfocused.Width, brdUnfocused.Height), 200),
                    lblSmallPlaceholder.FadeTo(1, 60),
                    lblSmallPlaceholder.TranslateTo(lblSmallPlaceholder.TranslationX, txt.Y - txt.Height + 4, 200, Easing.CubicInOut)
                );
            }
            else
            {
                await brdFocused.LayoutTo(new Rectangle(brdUnfocused.X, brdUnfocused.Y, brdUnfocused.Width, brdUnfocused.Height), 200);
            }
        }

        private async void OnTxtUnfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(txt.Text))
            {
                txt.Placeholder = Placeholder;
                txt.IsVisible = false;
                lblBigPlaceholder.IsVisible = true;

                var animations = new List<Task>();

                if (!HasValidationErrors)
                {
                    animations.Add(brdFocused.LayoutTo(new Rectangle(brdUnfocused.X, brdUnfocused.Y, 0, brdUnfocused.Height), 200));
                }

                animations.Add(lblSmallPlaceholder.FadeTo(0, 180));
                animations.Add(lblSmallPlaceholder.TranslateTo(lblSmallPlaceholder.TranslationX, txt.Y, 200, Easing.CubicInOut));
                
                // animate both at the same time
                await Task.WhenAll(animations);
            }
            else
            {
                if (!HasValidationErrors)
                {
                    await brdFocused.LayoutTo(new Rectangle(brdUnfocused.X, brdUnfocused.Y, 0, brdUnfocused.Height), 200);
                }
            }
        }

        #endregion
    }
}