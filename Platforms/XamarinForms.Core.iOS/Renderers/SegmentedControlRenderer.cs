﻿using System;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinForms.Core.iOS.Renderers;
using XamarinForms.Core.Standard.Controls;

[assembly: ExportRenderer(typeof(SegmentedControlView), typeof(SegmentedControlRenderer))]
namespace XamarinForms.Core.iOS.Renderers
{
    public sealed class SegmentedControlRenderer : ViewRenderer<SegmentedControlView, UISegmentedControl>
    {
        private UISegmentedControl _nativeControl;

        protected override void OnElementChanged(ElementChangedEventArgs<SegmentedControlView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                _nativeControl = new UISegmentedControl();

                foreach (var child in Element.Children.Select((value, i) => new { value, i }))
                {
                    _nativeControl.InsertSegment(Element.Children[child.i].Text, child.i, false);
                }

                _nativeControl.Enabled = Element.IsEnabled;
                _nativeControl.TintColor = Element.IsEnabled ? Element.TintColor.ToUIColor() : Element.DisabledColor.ToUIColor();
                SetSelectedTextColor();

                _nativeControl.SelectedSegment = Element.SelectedSegment;

                SetNativeControl(_nativeControl);
            }

            if (e.OldElement != null)
            {
                if (_nativeControl != null) _nativeControl.ValueChanged -= NativeControl_SelectionChanged;
                RemoveElementHandlers();
            }

            if (e.NewElement != null)
            {
                if (_nativeControl != null) _nativeControl.ValueChanged += NativeControl_SelectionChanged;
                CreateElementHandlers();
            }
        }

        private void CreateElementHandlers()
        {
            if (Element != null)
            {
                foreach (var child in Element.Children)
                {
                    child.PropertyChanged += SegmentPropertyChanged;
                }
            }
        }

        private void RemoveElementHandlers()
        {
            if (Element != null)
            {
                foreach (var child in Element.Children)
                {
                    child.PropertyChanged -= SegmentPropertyChanged;
                }
            }
        }

        private void SegmentPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (_nativeControl != null && Element != null && sender is SegmentedControlOption option && e.PropertyName == nameof(option.Text))
            {
                var index = Element.Children.IndexOf(option);
                _nativeControl.SetTitle(Element.Children[index].Text, index);
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

            if (_nativeControl == null || Element == null) return;

            switch (e.PropertyName)
            {
                case "SelectedSegment":
                    _nativeControl.SelectedSegment = Element.SelectedSegment;
                    Element.RaiseSelectionChanged();
                    break;
                case "TintColor":
                    _nativeControl.TintColor = Element.IsEnabled ? Element.TintColor.ToUIColor() : Element.DisabledColor.ToUIColor();
                    break;
                case "IsEnabled":
                    _nativeControl.Enabled = Element.IsEnabled;
                    _nativeControl.TintColor = Element.IsEnabled ? Element.TintColor.ToUIColor() : Element.DisabledColor.ToUIColor();
                    break;
                case "SelectedTextColor":
                    SetSelectedTextColor();
                    break;
                default:
                    break;
            }
        }

        private void SetSelectedTextColor()
        {
            var attr = new UITextAttributes { TextColor = Element.SelectedTextColor.ToUIColor() };
            _nativeControl.SetTitleTextAttributes(attr, UIControlState.Selected);
        }

        private void NativeControl_SelectionChanged(object sender, EventArgs e)
        {
            Element.SelectedSegment = (int)_nativeControl.SelectedSegment;
        }

        protected override void Dispose(bool disposing)
        {
            if (_nativeControl != null)
            {
                _nativeControl.ValueChanged -= NativeControl_SelectionChanged;
                _nativeControl?.Dispose();
                _nativeControl = null;
            }

            RemoveElementHandlers();

            base.Dispose(disposing);
        }

        public static void Initialize()
        {

        }
    }


}
