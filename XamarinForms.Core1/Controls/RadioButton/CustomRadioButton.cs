using System;
using Xamarin.Forms;

namespace XamarinForms.Core.Controls.RadioButton
{
    public class CustomRadioButton : View
    {
        #region Bindable Properties

        #region Checked

        public static readonly BindableProperty CheckedProperty = BindableProperty.Create("Checked", typeof(bool), typeof(CustomRadioButton), false);

        public bool Checked
        {
            get => (bool)GetValue(CheckedProperty);


            set
            {
                SetValue(CheckedProperty, value);


                var eventHandler = CheckedChanged;


                if (eventHandler != null)
                {
                    eventHandler.Invoke(this, value);
                }
            }
        }

        #endregion

        #region Text

        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(CustomRadioButton), string.Empty);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        #endregion

        #region TextColor

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create("TextColor", typeof(Color), typeof(CustomRadioButton), Color.Default);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        #endregion

        #region FontSize

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create("FontSize", typeof(double), typeof(CustomRadioButton), -1.0);

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        #endregion

        #region FontName

        public static readonly BindableProperty FontNameProperty =
            BindableProperty.Create("FontName", typeof(string), typeof(CustomRadioButton), string.Empty);

        public string FontName
        {
            get => (string)GetValue(FontNameProperty);
            set => SetValue(FontNameProperty, value);
        }

        #endregion

        #endregion

        #region Events

        public EventHandler<bool> CheckedChanged;

        #endregion


#pragma warning disable CS0108 // Member hides inherited member; missing new keyword 
        /// <summary> 
        /// Gets or sets the identifier. 
        /// </summary> 
        /// <value>The identifier.</value> 
        public int Id { get; set; }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword 
    }

}
