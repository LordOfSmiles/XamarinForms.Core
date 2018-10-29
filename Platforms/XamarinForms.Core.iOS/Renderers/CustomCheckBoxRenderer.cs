using System;
using System.ComponentModel;
using CoreGraphics;
using Dmb.iOS.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinForms.Core.iOS.Extensions;
using XamarinForms.Core.Standard.Controls;

[assembly: ExportRenderer(typeof(CheckBox), typeof(CustomCheckBoxRenderer))]
namespace Dmb.iOS.Renderers
{
    //
    public class CustomCheckBoxRenderer : ViewRenderer<CheckBox, CustomCheckBoxView>
    {
        /// <summary> 
        /// Called when [element changed]. 
        /// </summary> 
        /// <param name="e">The e.</param> 
        protected override void OnElementChanged(ElementChangedEventArgs<CheckBox> e)
        {
            base.OnElementChanged(e);


            if (Element == null)
                return;


            BackgroundColor = Element.BackgroundColor.ToUIColor();
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    var checkBox = new CustomCheckBoxView(Bounds);
                    checkBox.TouchUpInside += (s, args) => Element.Checked = Control.Checked;


                    SetNativeControl(checkBox);
                }
                Control.LineBreakMode = UILineBreakMode.TailTruncation;
                Control.VerticalAlignment = UIControlContentVerticalAlignment.Center;
                Control.CheckedTitle = string.IsNullOrEmpty(e.NewElement.CheckedText) ? e.NewElement.DefaultText : e.NewElement.CheckedText;
                Control.UncheckedTitle = string.IsNullOrEmpty(e.NewElement.UncheckedText) ? e.NewElement.DefaultText : e.NewElement.UncheckedText;
                Control.Checked = e.NewElement.Checked;
                Control.SetTitleColor(e.NewElement.TextColor.ToUIColor(), UIControlState.Normal);
                Control.SetTitleColor(e.NewElement.TextColor.ToUIColor(), UIControlState.Selected);
            }


            Control.Frame = Frame;
            Control.Bounds = Bounds;


            UpdateFont();
        }


        /// <summary> 
        /// Resizes the text. 
        /// </summary> 
        private void ResizeText()
        {
            if (Element == null)
                return;


            var text = Element.Checked ? string.IsNullOrEmpty(Element.CheckedText) ? Element.DefaultText : Element.CheckedText :
                string.IsNullOrEmpty(Element.UncheckedText) ? Element.DefaultText : Element.UncheckedText;


            var bounds = Control.Bounds;


            var width = Control.TitleLabel.Bounds.Width;

            var height = text.StringHeight(Control.Font, width);


            var minHeight = string.Empty.StringHeight(Control.Font, width);


            var requiredLines = Math.Round(height / minHeight, MidpointRounding.AwayFromZero);


            var supportedLines = Math.Round(bounds.Height / minHeight, MidpointRounding.ToEven);


            if (!supportedLines.Equals(requiredLines))
            {
                bounds.Height += (float)(minHeight * (requiredLines - supportedLines));
                Control.Bounds = bounds;
                Element.HeightRequest = bounds.Height;
            }
        }


        /// <summary> 
        /// Draws the specified rect. 
        /// </summary> 
        /// <param name="rect">The rect.</param> 
        public override void Draw(CoreGraphics.CGRect rect)
        {
            base.Draw(rect);
            ResizeText();
        }


        /// <summary> 
        /// Updates the font. 
        /// </summary> 
        private void UpdateFont()
        {
            if (!string.IsNullOrEmpty(Element.FontName))
            {
                var font = UIFont.FromName(Element.FontName, (Element.FontSize > 0) ? (float)Element.FontSize : 12.0f);
                if (font != null)
                {
                    Control.Font = font;
                }
            }
            else if (Element.FontSize > 0)
            {
                var font = UIFont.FromName(Control.Font.Name, (float)Element.FontSize);
                if (font != null)
                {
                    Control.Font = font;
                }
            }
        }


        /// <summary> 
        /// Handles the <see cref="E:ElementPropertyChanged" /> event. 
        /// </summary> 
        /// <param name="sender">The sender.</param> 
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param> 
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);


            switch (e.PropertyName)
            {
                case "Checked":
                    Control.Checked = Element.Checked;
                    break;
                case "TextColor":
                    Control.SetTitleColor(Element.TextColor.ToUIColor(), UIControlState.Normal);
                    Control.SetTitleColor(Element.TextColor.ToUIColor(), UIControlState.Selected);
                    break;
                case "CheckedText":
                    Control.CheckedTitle = string.IsNullOrEmpty(Element.CheckedText) ? Element.DefaultText : Element.CheckedText;
                    break;
                case "UncheckedText":
                    Control.UncheckedTitle = string.IsNullOrEmpty(Element.UncheckedText) ? Element.DefaultText : Element.UncheckedText;
                    break;
                case "FontSize":
                    UpdateFont();
                    break;
                case "FontName":
                    UpdateFont();
                    break;
                case "Element":
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine("Property change for {0} has not been implemented.", e.PropertyName);
                    return;
            }
        }
    }

    [Register("CustomCheckBoxView")]
    public class CustomCheckBoxView : UIButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckBoxView"/> class.
        /// </summary>
        public CustomCheckBoxView()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckBoxView"/> class.
        /// </summary>
        /// <param name="bounds">The bounds.</param>
        public CustomCheckBoxView(CGRect bounds)
            : base(bounds)
        {
            Initialize();
        }

        /// <summary>
        /// Sets the checked title.
        /// </summary>
        /// <value>The checked title.</value>
        public string CheckedTitle
        {
            set
            {
                SetTitle(value, UIControlState.Selected);
            }
        }

        /// <summary>
        /// Sets the unchecked title.
        /// </summary>
        /// <value>The unchecked title.</value>
        public string UncheckedTitle
        {
            set
            {
                SetTitle(value, UIControlState.Normal);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CheckBoxView"/> is checked.
        /// </summary>
        /// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
        public bool Checked
        {
            set { Selected = value; }
            get { return Selected; }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            AdjustEdgeInsets();
            ApplyStyle();

            TouchUpInside += (sender, args) => Selected = !Selected;
        }

        /// <summary>
        /// Adjusts the edge insets.
        /// </summary>
        private void AdjustEdgeInsets()
        {
            const float Inset = 8f;

            HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            ImageEdgeInsets = new UIEdgeInsets(0f, Inset, 0f, 0f);
            TitleEdgeInsets = new UIEdgeInsets(0f, Inset * 2, 0f, 0f);
        }

        /// <summary>
        /// Applies the style.
        /// </summary>
        private void ApplyStyle()
        {
            SetImage(UIImage.FromBundle("Images/checked_checkbox.png"), UIControlState.Selected);
            SetImage(UIImage.FromBundle("Images/unchecked_checkbox.png"), UIControlState.Normal);
        }
    }
}

