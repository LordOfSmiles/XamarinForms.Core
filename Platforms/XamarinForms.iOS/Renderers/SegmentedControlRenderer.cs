using System;
using System.Collections.Generic;
using System.ComponentModel;
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

                RemoveElementHandlers();
            }

            if (!(e.NewElement is null))
            {
                if (!(_nativeControl is null))
                {
                    _nativeControl.ValueChanged += NativeControl_SelectionChanged;
                }

                AddElementHandlers(e.NewElement);
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
                    _nativeControl.SetEnabled(children[i].IsEnabled, i);
                }

                if (!(Element is null))
                {
                    _nativeControl.SelectedSegment = Element.SelectedSegment;
                }
            }
        }

        private void AddElementHandlers(SegmentedControl element, bool addChildHandlersOnly = false)
        {
            if (!(element is null))
            {
                if (!addChildHandlersOnly)
                {
                    element.OnElementChildrenChanging += OnElementChildrenChanging;
                }

                if (!(element.Children is null))
                {
                    foreach (var child in element.Children)
                    {
                        child.PropertyChanged += SegmentPropertyChanged;
                    }
                }
            }
        }

        private void RemoveElementHandlers(bool removeChildrenHandlersOnly = false)
        {
            if (!(Element is null))
            {
                if (!removeChildrenHandlersOnly)
                {
                    Element.OnElementChildrenChanging -= OnElementChildrenChanging;
                }

                if (!(Element.Children is null))
                {
                    foreach (var child in Element.Children)
                    {
                        child.PropertyChanged -= SegmentPropertyChanged;
                    }
                }
            }
        }

        private void OnElementChildrenChanging(object sender, EventArgs e)
        {
            RemoveElementHandlers(true);
        }


        private void SegmentPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!(_nativeControl is null) && !(Element is null) && sender is SegmentedControlOption option)
            {
                var index = Element.Children.IndexOf(option);

                switch (e.PropertyName)
                {
                    case nameof(SegmentedControlOption.Text):
                        _nativeControl.SetTitle(option.Text, index);
                        break;
                    case nameof(SegmentedControlOption.IsEnabled):
                        _nativeControl.SetEnabled(option.IsEnabled, index);
                        break;
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
                    
                case nameof(SegmentedControl.Children):
                    if (!(Element.Children is null))
                    {
                        SetNativeControlSegments(Element.Children);
                        AddElementHandlers(Element, true);
                    }
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
            RemoveElementHandlers();

            base.Dispose(disposing);
        }

        public static void Initialize()
        {

        }
    }
    
    // public sealed class SegmentedControlRenderer : ViewRenderer<SegmentedControlView, UISegmentedControl>
    // {
    //     protected override void OnElementChanged(ElementChangedEventArgs<SegmentedControlView> e)
    //     {
    //         base.OnElementChanged(e);
    //
    //         if (Control == null && Element!=null)
    //         {
    //             _segmentControl = new UISegmentedControl();
    //             
    //             if (Element.Children != null)
    //             {
    //                 SetChildren();
    //             }
    //             
    //             _segmentControl.Enabled = Element.IsEnabled;
    //             SetSelectedSegmentTintColor();
    //             SetTintColor();
    //
    //             SetSelectedTextColor();
    //             _segmentControl.SelectedSegment = Element.SelectedSegment;
    //
    //             SetNativeControl(_segmentControl);
    //         }
    //
    //         if (e.OldElement != null)
    //         {
    //             if (_segmentControl != null) 
    //                 _segmentControl.ValueChanged -= SegmentControlSelectionChanged;
    //         }
    //
    //         if (e.NewElement != null)
    //         {
    //             if (_segmentControl != null) 
    //                 _segmentControl.ValueChanged += SegmentControlSelectionChanged;
    //         }
    //     }
    //
    //    
    //
    //     protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    //     {
    //         base.OnElementPropertyChanged(sender, e);
    //
    //         if (e.PropertyName == "Renderer")
    //         {
    //             Element?.RaiseSelectionChanged();
    //             return;
    //         }
    //
    //         if (_segmentControl == null || Element == null)
    //             return;
    //
    //         if (e.PropertyName == SegmentedControlView.ChildrenProperty.PropertyName)
    //         {
    //             _segmentControl.RemoveAllSegments();
    //
    //             if (Element.Children != null)
    //             {
    //                 SetChildren();
    //             }
    //         }
    //         else if (e.PropertyName == SegmentedControlView.SelectedSegmentProperty.PropertyName)
    //         {
    //             _segmentControl.SelectedSegment = Element.SelectedSegment;
    //             Element.RaiseSelectionChanged();
    //         }
    //         else if (e.PropertyName == SegmentedControlView.TintColorProperty.PropertyName)
    //         {
    //             SetTintColor();
    //             SetSelectedSegmentTintColor();
    //         }
    //         else if (e.PropertyName == VisualElement.IsEnabledProperty.PropertyName)
    //         {
    //             _segmentControl.Enabled = Element.IsEnabled;
    //             SetTintColor();
    //             SetSelectedSegmentTintColor();
    //         }
    //         else if (e.PropertyName == SegmentedControlView.SelectedTextColorProperty.PropertyName)
    //         {
    //             SetSelectedTextColor();
    //         }
    //         else if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName)
    //         {
    //             _segmentControl.BackgroundColor = Element.BackgroundColor.ToUIColor();
    //         }
    //     }
    //     
    //     #region Fields
    //
    //     private UISegmentedControl _segmentControl;
    //
    //     #endregion
    //     
    //     #region Private Methods
    //     
    //     private void SetSelectedTextColor()
    //     {
    //         var attr = new UITextAttributes
    //         {
    //             TextColor = Element.SelectedTextColor.ToUIColor()
    //         };
    //         _segmentControl.SetTitleTextAttributes(attr, UIControlState.Selected);
    //     }
    //     
    //     private void SetSelectedSegmentTintColor()
    //     {
    //         _segmentControl.SelectedSegmentTintColor = Element.IsEnabled
    //             ? Element.TintColor.ToUIColor()
    //             : Element.DisabledColor.ToUIColor();
    //     }
    //     
    //     private void SetTintColor()
    //     {
    //         _segmentControl.TintColor = Element.IsEnabled
    //             ? Element.TintColor.ToUIColor()
    //             : Element.DisabledColor.ToUIColor();
    //     }
    //     
    //     private void SetChildren()
    //     {
    //         for (var i = 0; i < Element.Children.Count; i++)
    //         {
    //             _segmentControl.InsertSegment(Element.Children[i], i, false);
    //         }
    //     }
    //
    //     private void SegmentControlSelectionChanged(object sender, EventArgs e)
    //     {
    //         Element.SelectedSegment = (int)_segmentControl.SelectedSegment;
    //     }
    //     
    //     #endregion
    //
    //     #region IDisposable
    //     
    //     protected override void Dispose(bool disposing)
    //     {
    //         if (_segmentControl != null)
    //         {
    //             _segmentControl.ValueChanged -= SegmentControlSelectionChanged;
    //             _segmentControl?.Dispose();
    //             _segmentControl = null;
    //         }
    //
    //         base.Dispose(disposing);
    //     }
    //     
    //     #endregion
    // }
    
    
}
