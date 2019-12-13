using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinForms.Core.Controls
{
    public partial class MaterialEntry
	{
        public MaterialEntry()
		{
            InitializeComponent();

            entryField.BindingContext = this;
            assistiveText.BindingContext = this;

            if (string.IsNullOrEmpty(Text))
            {
                hiddenLabel.TranslationY = 10;
            }
            
            entryField.Focused += async (s, a) =>
            {
                hiddenBottomBorder.Color = AccentColor;
                hiddenLabel.TextColor = Color.Gray;
                hiddenLabel.IsVisible = true;

                if (string.IsNullOrEmpty(entryField.Text))
                {
                    entryField.Placeholder = null;
                    entryField.IsVisible = true;

                    // animate both at the same time
                    await Task.WhenAll(
                        hiddenBottomBorder.LayoutTo(new Rectangle(bottomBorder.X, bottomBorder.Y, bottomBorder.Width, bottomBorder.Height), 200),
                        hiddenLabel.FadeTo(1, 60),
                        hiddenLabel.TranslateTo(hiddenLabel.TranslationX, entryField.Y - entryField.Height + 4, 200, Easing.CubicInOut)
                     );
                }
                else
                {
                    await hiddenBottomBorder.LayoutTo(new Rectangle(bottomBorder.X, bottomBorder.Y, bottomBorder.Width, bottomBorder.Height), 200);
                }
            };

            entryField.Unfocused += async (s, a) =>
            {
                hiddenLabel.TextColor = Color.Gray;

                if (string.IsNullOrEmpty(entryField.Text))
                {
                    entryField.Placeholder = Placeholder;
                    entryField.IsVisible = false;

                    // animate both at the same time
                    await Task.WhenAll(
                        hiddenBottomBorder.LayoutTo(new Rectangle(bottomBorder.X, bottomBorder.Y, 0, bottomBorder.Height), 200),
                        hiddenLabel.FadeTo(0, 180),
                        hiddenLabel.TranslateTo(hiddenLabel.TranslationX, entryField.Y, 200, Easing.CubicInOut)
                     );
                }
                else
                {
                    await hiddenBottomBorder.LayoutTo(new Rectangle(bottomBorder.X, bottomBorder.Y, 0, bottomBorder.Height), 200);
                }
            };

            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    entryField.Focus();
                })
            });

           
        }
        
        public static BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(MaterialEntry),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal)=>
            {
                if (!(bindable is MaterialEntry view))
                {
                    return;
                }

                string newValue = (string)newVal;
                
                view.entryField.IsVisible = true;
                view.hiddenLabel.IsVisible = true;
            });

        public static BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(MaterialEntry),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (!(bindable is MaterialEntry view))
                {
                    return;
                }

                view.entryField.Placeholder = (string)newVal;
                view.hiddenLabel.Text = (string)newVal;
            });

      

        public static BindableProperty KeyboardProperty = BindableProperty.Create(
            nameof(Keyboard),
            typeof(Keyboard),
            typeof(MaterialEntry),
            defaultValue: Keyboard.Default,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (!(bindable is MaterialEntry view))
                {
                    return;
                }

                view.entryField.Keyboard = (Keyboard)newVal;
            });

        public static BindableProperty AccentColorProperty = BindableProperty.Create(
            nameof(AccentColor),
            typeof(Color),
            typeof(MaterialEntry),
            defaultValue: Color.Accent);

        public static BindableProperty AssistiveTextProperty = BindableProperty.Create(
            nameof(AssistiveText),
            typeof(string),
            typeof(MaterialEntry),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (!(bindable is MaterialEntry view))
                {
                    return;
                }

                view.assistiveText.Text = (string)newVal;
            });

        public static BindableProperty AssistiveTextIsVisibleProperty = BindableProperty.Create(
            nameof(AssistiveTextIsVisible),
            typeof(bool),
            typeof(MaterialEntry),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal)=>
            {
                if (!(bindable is MaterialEntry view))
                {
                    return;
                }

                SetAssistiveTextBackground(view);
            });

        public static BindableProperty ParentColorProperty = BindableProperty.Create(
            nameof(ParentColor),
            typeof(Color),
            typeof(MaterialEntry),
            defaultValue: Color.Transparent,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (!(bindable is MaterialEntry view))
                {
                    return;
                }

                SetAssistiveTextBackground(view);
            });

        public static BindableProperty EntryTextAlignmentProperty = BindableProperty.Create(
            nameof(EntryTextAlignment),
            typeof(TextAlignment),
            typeof(MaterialEntry),
            defaultValue: TextAlignment.Start,
            defaultBindingMode: BindingMode.TwoWay);

        public static BindableProperty EntryFontSizeProperty = BindableProperty.Create(
            nameof(EntryFontSize),
            typeof(double),
            typeof(MaterialEntry),
            defaultValue: 16d,
            defaultBindingMode: BindingMode.TwoWay);

        public static BindableProperty EntryMaxLengthProperty = BindableProperty.Create(
            nameof(EntryMaxLength),
            typeof(int),
            typeof(MaterialEntry),
            defaultValue: int.MaxValue,
            defaultBindingMode: BindingMode.TwoWay);
        

        public static BindableProperty UnderlineColorProperty = BindableProperty.Create(
            nameof(UnderlineColor),
            typeof(Color),
            typeof(MaterialEntry),
            defaultValue: Color.DodgerBlue,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (!(bindable is MaterialEntry view))
                {
                    return;
                }

                view.bottomBorder.Color = (Color)newVal;
            });
        
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }
        
        public Keyboard Keyboard
        {
            get { return (Keyboard)GetValue(KeyboardProperty); }
            set { SetValue(KeyboardProperty, value); }
        }

        public Color AccentColor
        {
            get { return (Color)GetValue(AccentColorProperty); }
            set { SetValue(AccentColorProperty, value); }
        }

        public string AssistiveText
        {
            get { return (string)GetValue(AssistiveTextProperty); }
            set { SetValue(AssistiveTextProperty, value); }
        }

        public bool AssistiveTextIsVisible
        {
            get { return (bool)GetValue(AssistiveTextIsVisibleProperty); }
            set { SetValue(AssistiveTextIsVisibleProperty, value); }
        }

        public Color ParentColor
        {
            get { return (Color)GetValue(ParentColorProperty); }
            set { SetValue(ParentColorProperty, value); }
        }

        public TextAlignment EntryTextAlignment
        {
            get { return (TextAlignment)GetValue(EntryTextAlignmentProperty); }
            set { SetValue(EntryTextAlignmentProperty, value); }
        }

        public double EntryFontSize
        {
            get { return (double)GetValue(EntryFontSizeProperty); }
            set { SetValue(EntryFontSizeProperty, value); }
        }

        public int EntryMaxLength
        {
            get { return (int)GetValue(EntryMaxLengthProperty); }
            set { SetValue(EntryMaxLengthProperty, value); }
        }
        
        public Color UnderlineColor
        {
            get { return (Color)GetValue(UnderlineColorProperty); }
            set { SetValue(UnderlineColorProperty, value); }
        }

        private static void SetAssistiveTextBackground(MaterialEntry view)
        {
            view.assistiveTextBlock.BackgroundColor = /*view.AssistiveTextIsVisible ? Color.White : */view.ParentColor;
        }
    }
}