using System;
using System.ComponentModel;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinForms.Core.iOS.Controls;
using XamarinForms.Core.iOS.Extensions;
using XamarinForms.Core.iOS.Renderers;
using XamarinForms.Core.Standard.Controls;

[assembly: ExportRenderer(typeof(FloatingActionButtonView), typeof(FloatingActionButtonRenderer))]
namespace XamarinForms.Core.iOS.Renderers
{
    public sealed class FloatingActionButtonRenderer : ViewRenderer<FloatingActionButtonView, MnFloatingActionButton>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<FloatingActionButtonView> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
			{
                var fab = new MnFloatingActionButton(false)
                {
                    Frame = new CGRect(0, 0, 24, 24)
                };

                SetNativeControl(fab);
                
                UpdateStyles();
			}

			if (e.NewElement != null)
			{
				Control.TouchUpInside += Fab_TouchUpInside;
			}

			if (e.OldElement != null)
			{
				Control.TouchUpInside -= Fab_TouchUpInside;
			}
		}

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == FloatingActionButtonView.SizeProperty.PropertyName)
            {
                SetSize();
            }
            else if (e.PropertyName == FloatingActionButtonView.ColorNormalProperty.PropertyName ||
                     e.PropertyName == FloatingActionButtonView.ColorRippleProperty.PropertyName
                     )
            {
                SetBackgroundColors();
            }
            else if (e.PropertyName == FloatingActionButtonView.HasShadowProperty.PropertyName)
            {
                SetHasShadow();
            }
            else if (e.PropertyName == FloatingActionButtonView.ImageNameProperty.PropertyName ||
                     e.PropertyName == VisualElement.WidthProperty.PropertyName ||
                     e.PropertyName == VisualElement.HeightProperty.PropertyName)
            {
                SetImage();
            }
            else if (e.PropertyName == VisualElement.IsEnabledProperty.PropertyName)
            {
                UpdateEnabled();
            }
//            else if (e.PropertyName == FloatingActionButtonView.AnimateOnSelectionProperty.PropertyName)
//            {
//                UpdateAnimateOnSelection();
//            }
            else
            {
                base.OnElementPropertyChanged(sender, e);
            }
        }

		public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
		{
			var viewSize = Element.Size == FloatingActionButtonSize.Normal 
                ? 56 
                : 40;

			return new SizeRequest(new Size(viewSize, viewSize));
		}

		private void UpdateStyles()
		{
            SetSize();

            SetBackgroundColors();

            SetHasShadow();

            SetImage();

            UpdateEnabled();
		}

        private void SetSize()
        {
            if (Control == null || Element == null)
                return;
            
            switch (Element.Size)
            {
                case FloatingActionButtonSize.Mini:
                    Control.Size = MnFloatingActionButton.FabSize.Mini;
                    break;
                case FloatingActionButtonSize.Normal:
                    Control.Size = MnFloatingActionButton.FabSize.Normal;
                    break;
            }
        }

        private void SetBackgroundColors()
        {
            if (Control == null || Element == null)
                return;
            
            Control.BackgroundColor = Element.ColorNormal.ToUIColor();
            //Control. = this.Element.Ripplecolor.ToUIColor();
        }

        private void SetHasShadow()
        {
            if (Control == null || Element == null)
                return;
            
            Control.HasShadow = Element.HasShadow;
        }

        private void SetImage()
        {
            if (Control == null || Element == null)
                return;
            
            SetImageAsync(Element.Image, Control);
        }

        private void UpdateEnabled()
        {
            if (Control == null || Element == null)
                return;
            
            Control.Enabled = Element.IsEnabled;
            SetBackgroundColors();
        }
        
        private void Fab_TouchUpInside(object sender, EventArgs e)
        {
            Element?.Command?.Execute(null);
        }

        private static async void SetImageAsync(ImageSource source, MnFloatingActionButton targetButton)
        {
            if (source != null)
            {
                var img = await source.GetImageAsync();
                targetButton.CenterImageView.Image = img;
            }
            else
            {
                targetButton.CenterImageView.Image = null;
            }
        }
    }
}