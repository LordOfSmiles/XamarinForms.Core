using System;
using Xamarin.Forms;

namespace XamarinForms.Core.Controls
{
    public sealed class CheckBox : View
    {
        #region Bindable Properties

        #region Checked

        public static readonly BindableProperty CheckedProperty = BindableProperty.Create("Checked", typeof(bool), typeof(CheckBox), false, BindingMode.TwoWay, propertyChanged: OnCheckedPropertyChanged);

        public bool Checked
        {
            get => (bool)GetValue(CheckedProperty);
            set
            {
                SetValue(CheckedProperty, value);
                CheckedChanged?.Invoke(this, value);
            }
        }

        private static void OnCheckedPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var checkBox = bindable as CheckBox;
            if (checkBox == null)
                return;

            var value = (bool)newvalue;

            checkBox.Checked = value;
        }

        #endregion

        #region CheckedText

        public static readonly BindableProperty CheckedTextProperty = BindableProperty.Create("CheckedText", typeof(string), typeof(CheckBox), string.Empty, BindingMode.TwoWay);

        public string CheckedText
        {
            get => (string)GetValue(CheckedTextProperty);
            set => SetValue(CheckedTextProperty, value);
        }

        #endregion

        #region UncheckedText

        public static readonly BindableProperty UncheckedTextProperty = BindableProperty.Create("UncheckedText", typeof(string), typeof(CheckBox), string.Empty, BindingMode.TwoWay);

        public string UncheckedText
        {
            get => (string)GetValue(UncheckedTextProperty);
            set => SetValue(UncheckedTextProperty, value);
        }

        #endregion

        #region DefaultText

        public static readonly BindableProperty DefaultTextProperty = BindableProperty.Create("DefaultText", typeof(string), typeof(CheckBox), string.Empty);

        public string DefaultText
        {
            get => (string)GetValue(DefaultTextProperty);
            set => SetValue(DefaultTextProperty, value);
        }

        #endregion

        #region TextColor

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create("TextColor", typeof(Color), typeof(CheckBox), Color.Default);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        #endregion

        #region FontSize

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create("FontSize", typeof(double), typeof(CheckBox), -1.0);

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        #endregion

        #region FontName

        public static readonly BindableProperty FontNameProperty = BindableProperty.Create("FontName", typeof(string), typeof(CheckBox), string.Empty);

        public string FontName
        {
            get => (string)GetValue(FontNameProperty);
            set => SetValue(FontNameProperty, value);
        }

        #endregion

        #endregion

        #region Events

        public event EventHandler<bool> CheckedChanged;

        #endregion

        #region Properties

        public string Text => Checked
            ? (string.IsNullOrEmpty(CheckedText) ? DefaultText : CheckedText)
            : (string.IsNullOrEmpty(UncheckedText) ? DefaultText : UncheckedText);

        #endregion
    }

    //[assembly: ExportRenderer(typeof(CheckBox), typeof(CustomCheckBoxRenderer))]

    //   public class CustomCheckBoxRenderer : ViewRenderer<CheckBox, Android.Widget.CheckBox> 
    //{ 
    //       private ColorStateList defaultTextColor; 

    //       /// <summary> 
    //	/// Called when [element changed]. 
    //	/// </summary> 
    //	/// <param name="e">The e.</param> 
    //	protected override void OnElementChanged(ElementChangedEventArgs<CheckBox> e)
    //	{ 
    //		base.OnElementChanged(e); 


    //		if (this.Control == null) 
    //		{ 
    //			var checkBox = new Android.Widget.CheckBox(this.Context); 
    //			checkBox.CheckedChange += CheckBoxCheckedChange; 


    //		    defaultTextColor = checkBox.TextColors; 
    //			this.SetNativeControl(checkBox); 
    //		} 


    //		Control.Text = e.NewElement.Text; 
    //		Control.Checked = e.NewElement.Checked; 
    //           UpdateTextColor(); 


    //		if (e.NewElement.FontSize > 0) 
    //		{ 
    //			Control.TextSize = (float)e.NewElement.FontSize; 
    //		} 


    //		if (!string.IsNullOrEmpty(e.NewElement.FontName)) 
    //		{ 
    //			Control.Typeface = TrySetFont(e.NewElement.FontName); 
    //		} 
    //	} 


    //	/// <summary> 
    //	/// Handles the <see cref="E:ElementPropertyChanged" /> event. 
    //	/// </summary> 
    //	/// <param name="sender">The sender.</param> 
    //	/// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param> 
    //	protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    //	{ 
    //		base.OnElementPropertyChanged(sender, e); 


    //		switch (e.PropertyName) 
    //		{ 
    //			case "Checked": 
    //				Control.Text = Element.Text; 
    //				Control.Checked = Element.Checked; 
    //				break; 
    //			case "TextColor": 
    //		        UpdateTextColor(); 
    //				break; 
    //			case "FontName": 
    //				if (!string.IsNullOrEmpty(Element.FontName)) 
    //				{ 
    //					Control.Typeface = TrySetFont(Element.FontName); 
    //				} 
    //				break; 
    //			case "FontSize": 
    //				if (Element.FontSize > 0) 
    //				{ 
    //					Control.TextSize = (float)Element.FontSize; 
    //				} 
    //				break; 
    //			case "CheckedText": 
    //			case "UncheckedText": 
    //				Control.Text = Element.Text; 
    //				break; 
    //			default: 
    //				System.Diagnostics.Debug.WriteLine("Property change for {0} has not been implemented.", e.PropertyName); 
    //				break; 
    //		} 
    //	} 


    //	/// <summary> 
    //	/// CheckBoxes the checked change. 
    //	/// </summary> 
    //	/// <param name="sender">The sender.</param> 
    //		/// <param name="e">The <see cref="Android.Widget.CompoundButton.CheckedChangeEventArgs"/> instance containing the event data.</param> 
    //		void CheckBoxCheckedChange(object sender, Android.Widget.CompoundButton.CheckedChangeEventArgs e)
    //		{ 
    //			this.Element.Checked = e.IsChecked; 
    //		} 


    //		/// <summary> 
    //		/// Tries the set font. 
    //		/// </summary> 
    //		/// <param name="fontName">Name of the font.</param> 
    //		/// <returns>Typeface.</returns> 
    //		private Typeface TrySetFont(string fontName)
    //		{ 
    //			Typeface tf = Typeface.Default; 
    //			try 
    //			{ 
    //				tf = Typeface.CreateFromAsset(Context.Assets, fontName); 
    //				return tf; 
    //			} 
    //			catch (Exception ex) 
    //			{ 
    //				Console.Write("not found in assets {0}", ex); 
    //				try 
    //				{ 
    //					tf = Typeface.CreateFromFile(fontName); 
    //					return tf; 
    //				} 
    //				catch (Exception ex1) 
    //				{ 
    //					Console.Write(ex1); 
    //					return Typeface.Default; 
    //				} 
    //			} 
    //		} 


    //        /// <summary> 
    //        /// Updates the color of the text 
    //        /// </summary> 
    //        private void UpdateTextColor()
    //        { 
    //            if (Control == null || Element == null) 
    //                return; 


    //            if (Element.TextColor == Xamarin.Forms.Color.Default) 
    //                Control.SetTextColor(defaultTextColor); 
    //            else 
    //                Control.SetTextColor(Element.TextColor.ToAndroid()); 
    //        } 
    //	} 



    // [assembly: ExportRenderer(typeof(CheckBox), typeof(CustomCheckBoxRenderer))]
    //public class CheckBoxRenderer : ViewRenderer<CheckBox, CheckBoxView> 
    //{ 
    //    /// <summary> 
    //    /// Called when [element changed]. 
    //    /// </summary> 
    //    /// <param name="e">The e.</param> 
    //    protected override void OnElementChanged(ElementChangedEventArgs<CheckBox> e)
    //     { 
    //        base.OnElementChanged(e); 


    //        if (Element == null) return; 


    //        BackgroundColor = Element.BackgroundColor.ToUIColor(); 
    //        if (e.NewElement != null) 
    //        { 
    //            if (Control == null) 
    //            { 
    //                var checkBox = new CheckBoxView(Bounds); 
    //                checkBox.TouchUpInside += (s, args) => Element.Checked = Control.Checked; 


    //                SetNativeControl(checkBox); 
    //            } 
    //            Control.LineBreakMode = UILineBreakMode.CharacterWrap; 
    //            Control.VerticalAlignment = UIControlContentVerticalAlignment.Top; 
    //            Control.CheckedTitle = string.IsNullOrEmpty(e.NewElement.CheckedText) ? e.NewElement.DefaultText : e.NewElement.CheckedText; 
    //            Control.UncheckedTitle = string.IsNullOrEmpty(e.NewElement.UncheckedText) ? e.NewElement.DefaultText : e.NewElement.UncheckedText; 
    //            Control.Checked = e.NewElement.Checked; 
    //            Control.SetTitleColor (e.NewElement.TextColor.ToUIColor (), UIControlState.Normal); 
    //            Control.SetTitleColor (e.NewElement.TextColor.ToUIColor (), UIControlState.Selected); 
    //        } 


    //        Control.Frame = Frame; 
    //        Control.Bounds = Bounds; 


    //        UpdateFont(); 
    //    } 


    //    /// <summary> 
    //    /// Resizes the text. 
    //    /// </summary> 
    //    private void ResizeText()
    //    { 
    //        if (Element == null) 
    //            return; 


    //        var text = Element.Checked ? string.IsNullOrEmpty(Element.CheckedText) ? Element.DefaultText : Element.CheckedText :
    //            string.IsNullOrEmpty(Element.UncheckedText) ? Element.DefaultText : Element.UncheckedText; 


    //        var bounds = Control.Bounds; 


    //        var width = Control.TitleLabel.Bounds.Width; 


    //        var height = text.StringHeight(Control.Font, width); 


    //        var minHeight = string.Empty.StringHeight(Control.Font, width); 


    //        var requiredLines = Math.Round(height / minHeight, MidpointRounding.AwayFromZero); 


    //        var supportedLines = Math.Round(bounds.Height / minHeight, MidpointRounding.ToEven); 


    //        if (supportedLines != requiredLines) 
    //        { 
    //            bounds.Height += (float)(minHeight* (requiredLines - supportedLines)); 
    //            Control.Bounds = bounds; 
    //            Element.HeightRequest = bounds.Height; 
    //        } 
    //    } 


    //    /// <summary> 
    //    /// Draws the specified rect. 
    //    /// </summary> 
    //    /// <param name="rect">The rect.</param> 
    //    public override void Draw(CoreGraphics.CGRect rect)
    //    { 
    //        base.Draw(rect); 
    //        ResizeText(); 
    //    } 


    //    /// <summary> 
    //     /// Updates the font. 
    //     /// </summary> 
    //     private void UpdateFont()
    //     { 
    //         if (!string.IsNullOrEmpty(Element.FontName)) { 
    //             var font = UIFont.FromName(Element.FontName, (Element.FontSize > 0) ? (float)Element.FontSize : 12.0f); 
    //             if (font != null) { 
    //                 Control.Font = font; 
    //             } 
    //         } else if (Element.FontSize > 0) { 
    //             var font = UIFont.FromName(Control.Font.Name, (float)Element.FontSize); 
    //             if (font != null) { 
    //                 Control.Font = font; 
    //             } 
    //         } 
    //     } 


    //     /// <summary> 
    //     /// Handles the <see cref="E:ElementPropertyChanged" /> event. 
    //     /// </summary> 
    //     /// <param name="sender">The sender.</param> 
    //     /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param> 
    //     protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    //     { 
    //         base.OnElementPropertyChanged(sender, e); 


    //         switch (e.PropertyName) 
    //         { 
    //             case "Checked": 
    //                 Control.Checked = Element.Checked; 
    //                 break; 
    //             case "TextColor": 
    //                 Control.SetTitleColor(Element.TextColor.ToUIColor(), UIControlState.Normal); 
    //                 Control.SetTitleColor(Element.TextColor.ToUIColor(), UIControlState.Selected); 
    //                 break; 
    //             case "CheckedText": 
    //                 Control.CheckedTitle = string.IsNullOrEmpty(Element.CheckedText) ? Element.DefaultText : Element.CheckedText; 
    //                 break; 
    //             case "UncheckedText": 
    //                 Control.UncheckedTitle = string.IsNullOrEmpty(Element.UncheckedText) ? Element.DefaultText : Element.UncheckedText; 
    //                 break; 
    //             case "FontSize": 
    //                 UpdateFont(); 
    //                 break; 
    //             case "FontName": 
    //                 UpdateFont(); 
    //                 break; 
    //             case "Element": 
    //                 break; 
    //             default: 
    //                 System.Diagnostics.Debug.WriteLine("Property change for {0} has not been implemented.", e.PropertyName); 
    //                 return; 
    //         } 
    //     } 
    // } 

}
