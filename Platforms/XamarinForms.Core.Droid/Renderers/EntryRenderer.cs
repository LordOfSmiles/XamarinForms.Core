using System;
using System.ComponentModel;
using System.Linq;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Java.Lang;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinForms.Core.Droid.Helpers;
using EntryRenderer = XamarinForms.Core.Droid.Renderers.EntryRenderer;
using LinearLayout = Android.Widget.LinearLayout;

[assembly: ExportRenderer(typeof(Entry), typeof(EntryRenderer))]
namespace XamarinForms.Core.Droid.Renderers
{
    public sealed class EntryRenderer : Xamarin.Forms.Platform.Android.AppCompat.ViewRenderer<Entry, TextInputLayout>, ITextWatcher, TextView.IOnEditorActionListener
    {
        #region Overrides

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var layout = CreateNativeControl();

                layout.Hint = Element.Placeholder;
                EditText.Text = Element.Text;
                EditText.SetSingleLine(true);
                EditText.ImeOptions = ImeAction.Done;
                EditText.AddTextChangedListener(this);
                EditText.SetOnEditorActionListener(this);

                SetNativeControl(layout);
            }

            if (e.OldElement != null)
            {

            }

            if (e.NewElement != null)
            {
                UpdateInputType();
                UpdateColor();
                UpdateAlignment();
                UpdateFont();
                UpdatePlaceholderColor();
                UpdateEnabled();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Entry.PlaceholderProperty.PropertyName)
                Control.Hint = Element.Placeholder;
            else if (e.PropertyName == Entry.IsPasswordProperty.PropertyName)
                UpdateInputType();
            else if (e.PropertyName == Entry.TextProperty.PropertyName)
            {
                if (EditText.Text != Element.Text)
                {
                    EditText.Text = Element.Text;
                    if (Control.IsFocused)
                    {
                        EditText.SetSelection(EditText.Text.Length);
                        KeyboardHelper.ShowKeyboard(Control);
                    }
                }
            }
            else if (e.PropertyName == Entry.TextColorProperty.PropertyName)
                UpdateColor();
            else if (e.PropertyName == InputView.KeyboardProperty.PropertyName)
                UpdateInputType();
            else if (e.PropertyName == Entry.HorizontalTextAlignmentProperty.PropertyName)
                UpdateAlignment();
            else if (e.PropertyName == Entry.FontAttributesProperty.PropertyName)
                UpdateFont();
            else if (e.PropertyName == Entry.FontFamilyProperty.PropertyName)
                UpdateFont();
            else if (e.PropertyName == Entry.FontSizeProperty.PropertyName)
                UpdateFont();
            else if (e.PropertyName == Entry.PlaceholderColorProperty.PropertyName)
                UpdatePlaceholderColor();
            else if (e.PropertyName == VisualElement.IsEnabledProperty.PropertyName)
                UpdateEnabled();

            base.OnElementPropertyChanged(sender, e);
        }

        protected override TextInputLayout CreateNativeControl()
        {
            var context = Context;
            var themeId = ThemeHelper.GetTheme<EditText>(Element.StyleClass?.FirstOrDefault());
            if (themeId.HasValue)
            {
                context = new ContextThemeWrapper(Context, themeId.Value);
            }

            EditText = new AppCompatEditText(context);
            EditText.LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent);

            var layout = new BugFreeTextInputLayout(context);
            layout.LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent);
            layout.AddView(EditText);

            return layout;
        }

        #endregion

        private ColorStateList _hintTextColorDefault;
        private ColorStateList _textColorDefault;

        #region Constructor

        public EntryRenderer(Context context)
            : base(context)
        {
            AutoPackage = false;
        }

        #endregion

        private AppCompatEditText EditText { get; set; }

        #region TextView.IOnEditorActionListener

        bool TextView.IOnEditorActionListener.OnEditorAction(TextView v, ImeAction actionId, KeyEvent e)
        {
            // Fire Completed and dismiss keyboard for hardware / physical keyboards
            if (actionId == ImeAction.Done || (actionId == ImeAction.ImeNull && e.KeyCode == Keycode.Enter))
            {
                EditText.ClearFocus();
                Context.HideKeyboard(v);
                Element.SendCompleted();
            }

            return false;
        }

        #endregion

        #region ITextWatcher

        void ITextWatcher.AfterTextChanged(IEditable s)
        {
        }

        void ITextWatcher.BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
        }

        void ITextWatcher.OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            if (string.IsNullOrEmpty(Element.Text) && s.Length() == 0)
                return;

            ((IElementController)Element).SetValueFromRenderer(Entry.TextProperty, s.ToString());
        }

        #endregion

        #region Private Methods

        private void UpdateEnabled()
        {
            EditText.Focusable = Element.IsEnabled;
            EditText.FocusableInTouchMode = Element.IsEnabled;
            EditText.Clickable = Element.IsEnabled;
            EditText.LongClickable = Element.IsEnabled;
            EditText.SetCursorVisible(Element.IsEnabled);
        }

        private void UpdateAlignment()
        {
            var gravity = GravityFlags.Left;

            switch (Element.HorizontalTextAlignment)
            {
                case Xamarin.Forms.TextAlignment.Center:
                    gravity = GravityFlags.CenterHorizontal;
                    break;
                case Xamarin.Forms.TextAlignment.End:
                    gravity = GravityFlags.Right;
                    break;
            }

            EditText.Gravity = gravity;
        }

        private void UpdateColor()
        {
            if (Element.TextColor == Color.Default)
            {
                if (_textColorDefault == null)
                {
                    // This control has always had the default colors; nothing to update
                    return;
                }

                // This control is being set back to the default colors
                EditText.SetTextColor(_textColorDefault);
            }
            else
            {
                if (_textColorDefault == null)
                {
                    // Keep track of the default colors so we can return to them later
                    // and so we can preserve the default disabled color
                    _textColorDefault = EditText.TextColors;
                }

                EditText.SetTextColor(Element.TextColor.ToAndroidPreserveDisabled(_textColorDefault));
            }
        }

        private void UpdateFont()
        {
            var font = Font.OfSize(Element.FontFamily, Element.FontSize).WithAttributes(Element.FontAttributes);
            EditText.Typeface = font.ToTypeface();
            EditText.SetTextSize(ComplexUnitType.Sp, (float)Element.FontSize);
        }

        private void UpdateInputType()
        {
            EditText.InputType = KeyboardHelper.GetInputType(Element.Keyboard);

            if (Element.IsPassword && ((EditText.InputType & InputTypes.ClassText) == InputTypes.ClassText))
                EditText.InputType |= InputTypes.TextVariationPassword;

            if (Element.IsPassword && ((EditText.InputType & InputTypes.ClassNumber) == InputTypes.ClassNumber))
                EditText.InputType |= InputTypes.NumberVariationPassword;
        }

        private void UpdatePlaceholderColor()
        {
            Color placeholderColor = Element.PlaceholderColor;

            if (placeholderColor == Color.Default)
            {
                if (_hintTextColorDefault == null)
                {
                    // This control has always had the default colors; nothing to update
                    return;
                }

                // This control is being set back to the default colors
                EditText.SetHintTextColor(_hintTextColorDefault);
            }
            else
            {
                if (_hintTextColorDefault == null)
                {
                    // Keep track of the default colors so we can return to them later
                    // and so we can preserve the default disabled color
                    _hintTextColorDefault = EditText.HintTextColors;
                }

                EditText.SetHintTextColor(placeholderColor.ToAndroidPreserveDisabled(_hintTextColorDefault));
            }
        }

        #endregion
    }

    public class BugFreeTextInputLayout : TextInputLayout
    {
        protected BugFreeTextInputLayout(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public BugFreeTextInputLayout(Context context, IAttributeSet attrs, int defStyleAttr)
            : base(context, attrs, defStyleAttr)
        {
        }

        public BugFreeTextInputLayout(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public BugFreeTextInputLayout(Context context) : base(context)
        {
        }

        public override ICharSequence ErrorFormatted
        {
            get { return base.ErrorFormatted; }
            set
            {
                var editText = EditText;
                if ((editText != null && editText.Background != null) && (Android.OS.Build.VERSION.SdkInt == BuildVersionCodes.LollipopMr1 || Build.VERSION.SdkInt == BuildVersionCodes.Lollipop))
                {
#pragma warning disable 618
                    editText.SetBackgroundDrawable(editText.Background.GetConstantState().NewDrawable());
#pragma warning restore 618
                }

                base.ErrorFormatted = value;
            }
        }
    }
}