using System;
using System.ComponentModel;
using System.IO;
using Android.Content.Res;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinForms.Core.Controls;
using XamarinForms.Core.Droid.Renderers;
using FAB = Android.Support.Design.Widget.FloatingActionButton;

[assembly: ExportRenderer(typeof(FloatingActionButtonView), typeof(FloatingActionButtonRenderer))]
namespace XamarinForms.Core.Droid.Renderers
{
    public class FloatingActionButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ViewRenderer<FloatingActionButtonView, FAB>
    {
        #region Fields

        private FAB _fab;

        #endregion


        protected override void OnElementChanged(ElementChangedEventArgs<FloatingActionButtonView> e)
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
            }

            // set the icon

            //var elementImage = Element.ImageName;

            //var imageFile = elementImage?.File;



            //if (imageFile != null)

            //{

            //    fab.SetImageDrawable(Context.Resources.GetDrawable(imageFile));

            //}
        }

        //protected override void OnLayout(bool changed, int l, int t, int r, int b)
        //{

        //    base.OnLayout(changed, l, t, r, b);

        //    Control.BringToFront();
        //}



        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (e.PropertyName == nameof(Element.ColorNormal))
            {
                _fab.SetBackgroundColor(Element.ColorNormal.ToAndroid());
            }

            if (e.PropertyName == nameof(Element.ImageName))
            {

                //var elementImage = Element.Image;

                //var imageFile = elementImage?.File;



                //if (imageFile != null)

                //{

                //    fab.SetImageDrawable(Context.Resources.GetDrawable(imageFile));

                //}
            }

            base.OnElementPropertyChanged(sender, e);
        }



        private void Fab_Click(object sender, EventArgs e)
        {

            // proxy the click to the element
            //((IButtonController)Element).SendClicked();
        }

    }
}