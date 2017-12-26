using System;
using System.ComponentModel;
using System.Linq;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace XamarinForms.Core.iOS.Renderers
{
    public sealed class FloatingEntryRenderer : EntryRenderer
    {
        private IElementController ElementController => Element;

        private LabeledTextField Label => Control as LabeledTextField;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            if (Control == null)
            {
                var field = new LabeledTextField(CGRect.Empty);
                field.BorderStyle = UITextBorderStyle.None;
                field.EditingChanged += OnEditingChanged;
                field.ShouldReturn = OnShouldReturn;
                field.EditingDidBegin += OnEditingBegan;
                field.EditingDidEnd += OnEditingEnded;

                SetNativeControl(field);
                ApplyStyle(Element.StyleClass?.FirstOrDefault());
            }

            if (e.NewElement != null)
            {
                UpdatePlaceholder();
                UpdateEnabled();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Entry.PlaceholderColorProperty.PropertyName)
            {
                UpdatePlaceholder();
            }
            else if (e.PropertyName == VisualElement.IsEnabledProperty.PropertyName)
                UpdateEnabled();
        }

        void UpdateEnabled()
        {
            Label.ReadOnly = !Element.IsEnabled;
        }

        private void UpdatePlaceholder()
        {
            if (Label != null && Element.PlaceholderColor != Color.Default)
            {
                Label.TitleColor = Element.PlaceholderColor.ToUIColor();
            }
        }

        private void OnEditingBegan(object sender, EventArgs e)
        {
            ElementController.SetValueFromRenderer(VisualElement.IsFocusedProperty, true);
        }

        private void OnEditingChanged(object sender, EventArgs eventArgs)
        {
            ElementController.SetValueFromRenderer(Entry.TextProperty, Control.Text);
        }

        private void OnEditingEnded(object sender, EventArgs e)
        {
            ElementController.SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
        }

        private bool OnShouldReturn(UITextField view)
        {
            // ISSUE: reference to a compiler-generated method
            Control.ResignFirstResponder();
            ((IEntryController)Element).SendCompleted();
            return true;
        }

        private void ApplyStyle(string style)
        {
            if (style == null || style == "Default")
            {
                Element.TextColor = Color.FromHex("#2E3641");
                Label.TitleColor = Color.FromHex("#9FA3A8").ToUIColor();
                Label.SelectedTitleColor = Color.FromHex("#9FA3A8").ToUIColor();
                Label.LineColor = Color.FromHex("#9EA1A7").ToUIColor();
                Label.SelectedLineColor = Color.FromHex("#9EA1A7").ToUIColor();
            }
            else
            {
                if (style == "Light")
                {
                    Element.TextColor = Color.White;
                    Label.TextColor = Color.White.ToUIColor();
                    Label.TitleColor = Color.White.ToUIColor();
                    Label.SelectedTitleColor = Color.White.ToUIColor();
                    Label.LineColor = Color.White.ToUIColor();
                    Label.SelectedLineColor = Color.White.ToUIColor();
                }
            }
            Label.LineHeight = 0.8f;
            Label.SelectedLineHeight = 0.8f;
        }
    }
}