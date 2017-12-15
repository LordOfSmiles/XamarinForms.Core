using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinForms.Core.Droid.Helpers;
using XamarinForms.Core.Droid.Renderers;
using XamarinForms.Core.Standard.Controls;
using FAB = Android.Support.Design.Widget.FloatingActionButton;

[assembly: ExportRenderer(typeof(FloatingActionButtonView), typeof(FloatingActionButtonRenderer))]
namespace XamarinForms.Core.Droid.Renderers
{
    public sealed class FloatingActionButtonRenderer : ViewRenderer<FloatingActionButtonView, FAB>
    {
        #region Fields

        private FAB _fab;

        #endregion

        #region Constructor

        public FloatingActionButtonRenderer(Context context)
            : base(context)
        {
        }

        #endregion

        protected override async void OnElementChanged(ElementChangedEventArgs<FloatingActionButtonView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                _fab = new FAB(Context);
                _fab.SetBackgroundColor(Element.ColorNormal.ToAndroid());
                _fab.RippleColor = Element.ColorRipple.ToAndroid();

                SetNativeControl(_fab);
            }


            if (e.OldElement != null)
            {
                _fab.Click -= Fab_Click;
            }


            if (e.NewElement != null)
            {
                _fab.Click += Fab_Click;

                await UpdateImage();
            }

            // set the icon
            //var elementImage = Element.Icon;
            //var imageFile = elementImage?.File;



            //if (imageFile != null)

            //{

            //    fab.SetImageDrawable(Context.Resources.GetDrawable(imageFile));

            //}
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Element == null)
                return;

            if (e.PropertyName == nameof(Element.ColorNormal))
            {
                _fab.SetBackgroundColor(Element.ColorNormal.ToAndroid());
            }
            else if (e.PropertyName == nameof(Element.ColorRipple))
            {
                _fab.RippleColor = Element.ColorRipple.ToAndroid();
            }
            else if (e.PropertyName == nameof(Element.Icon))
            {
                UpdateImage();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        #region Handlers

        private void Fab_Click(object sender, EventArgs e)
        {
            Element?.Command?.Execute(null);
        }

        #endregion

        #region Private Methods

        private async Task UpdateImage()
        {
            if (Element.Icon == null)
                return;

            var bitmap = await ImageSourceHelper.GetBitmapAsync(Element.Icon);
            if (bitmap != null)
                _fab.SetImageBitmap(bitmap);
        }

        #endregion
    }
}