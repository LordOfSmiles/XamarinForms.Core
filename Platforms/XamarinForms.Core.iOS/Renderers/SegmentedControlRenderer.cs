using System;
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
        protected override void OnElementChanged(ElementChangedEventArgs<SegmentedControlView> e)
        {
            base.OnElementChanged(e);

            if (Control == null && Element!=null)
            {
                _nativeControl = new UISegmentedControl();
                
                if (Element.Children != null)
                {
                    SetChildren();
                }
                
                _nativeControl.Enabled = Element.IsEnabled;
                
                _nativeControl.TintColor = Element.IsEnabled 
                    ? Element.TintColor.ToUIColor() 
                    : Element.DisabledColor.ToUIColor();
                
                SetSelectedTextColor();

                _nativeControl.SelectedSegment = Element.SelectedSegment;

                SetNativeControl(_nativeControl);
            }

            if (e.OldElement != null)
            {
                if (_nativeControl != null) 
                    _nativeControl.ValueChanged -= NativeControl_SelectionChanged;
            }

            if (e.NewElement != null)
            {
                if (_nativeControl != null) 
                    _nativeControl.ValueChanged += NativeControl_SelectionChanged;
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

            if (_nativeControl == null || Element == null)
                return;

            if (e.PropertyName == SegmentedControlView.ChildrenProperty.PropertyName)
            {
                _nativeControl.RemoveAllSegments();

                if (Element.Children != null)
                {
                    SetChildren();
                }
            }
            else if (e.PropertyName == SegmentedControlView.SelectedSegmentProperty.PropertyName)
            {
                _nativeControl.SelectedSegment = Element.SelectedSegment;
                Element.RaiseSelectionChanged();
            }
            else if (e.PropertyName == SegmentedControlView.TintColorProperty.PropertyName)
            {
                _nativeControl.TintColor = Element.IsEnabled
                    ? Element.TintColor.ToUIColor()
                    : Element.DisabledColor.ToUIColor();
            }
            else if (e.PropertyName == SegmentedControlView.IsEnabledProperty.PropertyName)
            {
                _nativeControl.Enabled = Element.IsEnabled;
                _nativeControl.TintColor = Element.IsEnabled
                    ? Element.TintColor.ToUIColor()
                    : Element.DisabledColor.ToUIColor();
            }
            else if (e.PropertyName == SegmentedControlView.SelectedTextColorProperty.PropertyName)
            {
                SetSelectedTextColor();
            }
        }
        
        #region Fields

        private UISegmentedControl _nativeControl;

        #endregion
        
        #region Private Methods
        
        private void SetSelectedTextColor()
        {
            var attr = new UITextAttributes
            {
                TextColor = Element.SelectedTextColor.ToUIColor()
            };
            _nativeControl.SetTitleTextAttributes(attr, UIControlState.Selected);
        }
        
        private void SetChildren()
        {
            for (var i = 0; i < Element.Children.Count; i++)
            {
                _nativeControl.InsertSegment(Element.Children[i], i, false);
            }
        }

        private void NativeControl_SelectionChanged(object sender, EventArgs e)
        {
            Element.SelectedSegment = (int)_nativeControl.SelectedSegment;
        }
        
        #endregion

        #region IDisposable
        
        protected override void Dispose(bool disposing)
        {
            if (_nativeControl != null)
            {
                _nativeControl.ValueChanged -= NativeControl_SelectionChanged;
                _nativeControl?.Dispose();
                _nativeControl = null;
            }

            base.Dispose(disposing);
        }
        
        #endregion
    }
}
