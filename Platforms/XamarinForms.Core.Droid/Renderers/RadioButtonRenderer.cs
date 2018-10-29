using System;
using System.ComponentModel;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinForms.Core.Droid.Renderers;
using XamarinForms.Core.Standard.Controls.RadioButton;

[assembly: ExportRenderer(typeof(RadioButtonView), typeof(RadioButtonRenderer))]
namespace XamarinForms.Core.Droid.Renderers
{
    public sealed class RadioButtonRenderer : ViewRenderer<RadioButtonView, RadioButton>
    {
        #region Fields
        
        private ColorStateList _defaultTextColor;
        
        #endregion
        
        public RadioButtonRenderer(Context context)
            : base(context)
        {

        }

        
        /// <summary> 
        /// Called when [element changed]. 
        /// </summary> 
        /// <param name="e">The e.</param> 
        protected override void OnElementChanged(ElementChangedEventArgs<RadioButtonView> e)
        {
            base.OnElementChanged(e);


            if (Control == null)
            {
                var radButton = new RadioButton(Context);
                _defaultTextColor = radButton.TextColors;


                radButton.CheckedChange += radButton_CheckedChange;


                SetNativeControl(radButton);
            }


            Control.Text = e.NewElement.Text;
            //Control.TextSize = 14; 
            Control.Checked = e.NewElement.Checked;
            UpdateTextColor();


            if (e.NewElement.FontSize > 0)
            {
                Control.TextSize = (float)e.NewElement.FontSize;
            }


            if (!string.IsNullOrEmpty(e.NewElement.FontName))
            {
                Control.Typeface = TrySetFont(e.NewElement.FontName);
            }
        }


        private void radButton_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            Element.Checked = e.IsChecked;
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);


            switch (e.PropertyName)
            {
                case "Checked":
                    Control.Checked = Element.Checked;
                    break;
                case "Text":
                    Control.Text = Element.Text;
                    break;
                case "TextColor":
                    UpdateTextColor();
                    break;
                case "FontName":
                    if (!string.IsNullOrEmpty(Element.FontName))
                    {
                        Control.Typeface = TrySetFont(Element.FontName);
                    }
                    break;
                case "FontSize":
                    if (Element.FontSize > 0)
                    {
                        Control.TextSize = (float)Element.FontSize;
                    }
                    break;
            }
        }


        /// <summary> 
        ///     Tries the set font. 
        /// </summary> 
        /// <param name="fontName">Name of the font.</param> 
        /// <returns>Typeface.</returns> 
        private Typeface TrySetFont(string fontName)
        {
            var tf = Typeface.Default;


            try
            {
                tf = Typeface.CreateFromAsset(Context.Assets, fontName);


                return tf;
            }
            catch (Exception ex)
            {
                Console.Write("not found in assets {0}", ex);
                try
                {
                    tf = Typeface.CreateFromFile(fontName);


                    return tf;
                }
                catch (Exception ex1)
                {
                    Console.Write(ex1);


                    return Typeface.Default;
                }
            }
        }


        /// <summary> 
        /// Updates the color of the text 
        /// </summary> 
        private void UpdateTextColor()
        {
            if (Control == null || Element == null)
                return;

            if (Element.TextColor == Xamarin.Forms.Color.Default)
                Control.SetTextColor(_defaultTextColor);
            else
                Control.SetTextColor(Element.TextColor.ToAndroid());
        }
    }

}