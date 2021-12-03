using System;
using System.Collections.Generic;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using XamarinForms.Core.Controls;

namespace XamarinForms.iOS.Renderers
{
    public class SegmentedControlRenderer : ViewRenderer<SegmentedControl, UISegmentedControl>
    {
        private UISegmentedControl _nativeControl;

        protected override void OnElementChanged(ElementChangedEventArgs<SegmentedControl> e)
        {
            base.OnElementChanged(e);

            if (Control is null && Element != null)
            {
                _nativeControl = new UISegmentedControl();
                SetNativeControlSegments(Element.Children);
                _nativeControl.Enabled = Element.IsEnabled;

                SetNativeControl(_nativeControl);

                SetEnabledStateColor();
                SetFont();
                SetSelectedTextColor();
                SetTextColor();
            }

            if (!(e.OldElement is null))
            {
                if (!(_nativeControl is null))
                {
                    _nativeControl.ValueChanged -= NativeControl_SelectionChanged;
                }
            }

            if (!(e.NewElement is null))
            {
                if (!(_nativeControl is null))
                {
                    _nativeControl.ValueChanged += NativeControl_SelectionChanged;
                }
            }
        }

        private void SetNativeControlSegments(IList<SegmentedControlOption> children)
        {
            if (!(_nativeControl is null))
            {
                if (_nativeControl.NumberOfSegments > 0)
                {
                    _nativeControl.RemoveAllSegments();
                }
                
                for (var i = 0; i < children.Count; i++)
                {
                    _nativeControl.InsertSegment(children[i].Text, i, false);
                    //_nativeControl.SetEnabled(children[i].IsEnabled, i);
                }

                if (!(Element is null))
                {
                    _nativeControl.SelectedSegment = Element.SelectedSegment;
                }
            }
        }
        
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "Renderer")
            {
                Element?.RaiseSelectionChanged();
                return;
            }

            if (_nativeControl is null || Element is null)
            {
                return;
            }

            switch (e.PropertyName)
            {
                case nameof(SegmentedControl.SelectedSegment):
                    _nativeControl.SelectedSegment = Element.SelectedSegment;
                    Element.RaiseSelectionChanged();
                    break;

                case nameof(SegmentedControl.TintColor):
                    SetEnabledStateColor();
                    break;

                case nameof(SegmentedControl.IsEnabled):
                    _nativeControl.Enabled = Element.IsEnabled;
                    SetEnabledStateColor();
                    break;

                case nameof(SegmentedControl.SelectedTextColor):
                    SetSelectedTextColor();
                    break;

                case nameof(SegmentedControl.TextColor):
                    SetTextColor();
                    break;
                
                case nameof(SegmentedControl.FontSize):
                case nameof(SegmentedControl.FontFamily):
                    SetFont();
                    break;
            }
        }

        private void SetEnabledStateColor()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                _nativeControl.SelectedSegmentTintColor = Element.IsEnabled 
                    ? Element.TintColor.ToUIColor() 
                    : Element.DisabledColor.ToUIColor();
            }
            else
            {
                _nativeControl.TintColor = Element.IsEnabled 
                    ? Element.TintColor.ToUIColor() 
                    : Element.DisabledColor.ToUIColor();
            }
        }

        private void SetFont()
        {
            var uiTextAttribute = _nativeControl.GetTitleTextAttributes(UIControlState.Normal);

            var font = string.IsNullOrEmpty(Element.FontFamily) 
                ? UIFont.SystemFontOfSize((nfloat)Element.FontSize) 
                : UIFont.FromName(Element.FontFamily, (nfloat)Element.FontSize);

            uiTextAttribute.Font = font;

           _nativeControl.SetTitleTextAttributes(uiTextAttribute, UIControlState.Normal);
        }

        private void SetTextColor()
        {
            var uiTextAttribute = _nativeControl.GetTitleTextAttributes(UIControlState.Normal);

            uiTextAttribute.TextColor = Element.TextColor.ToUIColor();

            _nativeControl.SetTitleTextAttributes(uiTextAttribute, UIControlState.Normal);
        }

        private void SetSelectedTextColor()
        {
            var uiTextAttribute = _nativeControl.GetTitleTextAttributes(UIControlState.Normal);

            uiTextAttribute.TextColor = Element.SelectedTextColor.ToUIColor();

            _nativeControl.SetTitleTextAttributes(uiTextAttribute, UIControlState.Selected);
        }

        private void NativeControl_SelectionChanged(object sender, EventArgs e)
        {
            Element.SelectedSegment = (int)_nativeControl.SelectedSegment;
        }

        protected override void Dispose(bool disposing)
        {
            if (!(_nativeControl is null))
            {
                _nativeControl.ValueChanged -= NativeControl_SelectionChanged;
                _nativeControl?.Dispose();
                _nativeControl = null;
            }

            base.Dispose(disposing);
        }
    }
}
