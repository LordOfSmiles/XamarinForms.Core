﻿using System;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using Xamarin.Forms;

using Android.Views;
using System.IO;
using XamarinForms.Core.Standard.Controls;
using Android.Content;
using XamarinForms.Core.Droid.Renderers;
using Android.Support.Design.Widget;
using XamarinForms.Core.Droid.Helpers;
using System.Threading.Tasks;

[assembly: ExportRenderer(typeof(FloatingActionButtonView), typeof(FloatingActionButtonViewRenderer))]

namespace XamarinForms.Core.Droid.Renderers
{
    public sealed class FloatingActionButtonViewRenderer : ViewRenderer<FloatingActionButtonView, FrameLayout>
    {
        #region fields

        private const int MARGIN_DIPS = 16;
        private const int FAB_HEIGHT_NORMAL = 56;
        private const int FAB_HEIGHT_MINI = 40;

        private const int FAB_FRAME_HEIGHT_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_NORMAL;
        private const int FAB_FRAME_WIDTH_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_NORMAL;
        private const int FAB_MINI_FRAME_HEIGHT_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_MINI;
        private const int FAB_MINI_FRAME_WIDTH_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_MINI;
        private FloatingActionButton _fab;

        #endregion

        #region Constructor

        public FloatingActionButtonViewRenderer(Context context)
            : base(context)
        {
            //float d = context.Resources.DisplayMetrics.Density;
            //var margin = (int)(MARGIN_DIPS * d); // margin in pixels

            //_fab = new FloatingActionButton(context);
            //var lp = new FrameLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
            //lp.Gravity = GravityFlags.CenterVertical | GravityFlags.CenterHorizontal;
            //lp.LeftMargin = margin;
            //lp.TopMargin = margin;
            //lp.BottomMargin = margin;
            //lp.RightMargin = margin;
            //_fab.LayoutParameters = lp;
        }

        #endregion

        /// <summary>
        /// Element Changed
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnElementChanged(ElementChangedEventArgs<FloatingActionButtonView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                float d = Context.Resources.DisplayMetrics.Density;
                var margin = (int)(MARGIN_DIPS * d); // margin in pixels

                _fab = new FloatingActionButton(Context);
                var lp = new FrameLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
                lp.Gravity = GravityFlags.CenterVertical | GravityFlags.CenterHorizontal;
                lp.LeftMargin = margin;
                lp.TopMargin = margin;
                lp.BottomMargin = margin;
                lp.RightMargin = margin;
                _fab.LayoutParameters = lp;

                Element.Show = Show;
                Element.Hide = Hide;

                SetFabSize(Element.Size);
               await SetFabImage(Element.ImageName);

                var frame = new FrameLayout(Context);
                frame.RemoveAllViews();
                frame.AddView(_fab);

                SetNativeControl(frame);
            }

            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged -= HandlePropertyChanged;
                if (_fab != null)
                    _fab.Click -= Fab_Click;
            }

            if (Element != null)
            {
                Element.PropertyChanged += HandlePropertyChanged;
                if (_fab != null)
                    _fab.Click += Fab_Click;
            }
        }

        /// <summary>
        /// Show
        /// </summary>
        /// <param name="animate"></param>
        public void Show(bool animate = true) =>
            _fab?.Show();


        /// <summary>
        /// Hide!
        /// </summary>
        /// <param name="animate"></param>
        public void Hide(bool animate = true) =>
            _fab?.Hide();


        private async void HandlePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Content")
            {
                Tracker.UpdateLayout();
            }
            //else if (e.PropertyName == FloatingActionButtonView.ColorNormalProperty.PropertyName)
            //{
            //    fab.ColorNormal = Element.ColorNormal.ToAndroid();
            //}
            //else if (e.PropertyName == FloatingActionButtonView.ColorPressedProperty.PropertyName)
            //{
            //    fab.ColorPressed = Element.ColorPressed.ToAndroid();
            //}
            //else if (e.PropertyName == FloatingActionButtonView.ColorRippleProperty.PropertyName)
            //{
            //    fab.ColorRipple = Element.ColorRipple.ToAndroid();
            //}
            else if (e.PropertyName == FloatingActionButtonView.ImageNameProperty.PropertyName)
            {
                await SetFabImage(Element.ImageName);
            }
            else if (e.PropertyName == FloatingActionButtonView.SizeProperty.PropertyName)
            {
                SetFabSize(Element.Size);
            }
            //else if (e.PropertyName == FloatingActionButtonView.HasShadowProperty.PropertyName)
            //{
            //    _fab = Element.HasShadow;
            //}
        }

        private async Task SetFabImage(ImageSource imageSource)
        {
            if (imageSource != null)
            {
                var bitmap = await ImageSourceHelper.GetBitmapAsync(imageSource, Context);
                if (bitmap != null)
                {
                    _fab.SetImageBitmap(bitmap);
                }
            }
        }

        private void SetFabSize(FloatingActionButtonSize size)
        {
            if (size == FloatingActionButtonSize.Mini)
            {
                _fab.Size = (int)(40 * Context.Resources.DisplayMetrics.Density);
                Element.WidthRequest = FAB_MINI_FRAME_WIDTH_WITH_PADDING;
                Element.HeightRequest = FAB_MINI_FRAME_HEIGHT_WITH_PADDING;
            }
            else
            {
                _fab.Size = (int)(56 * Context.Resources.DisplayMetrics.Density);
                Element.WidthRequest = FAB_FRAME_WIDTH_WITH_PADDING;
                Element.HeightRequest = FAB_FRAME_HEIGHT_WITH_PADDING;
            }
        }

        private void Fab_Click(object sender, EventArgs e)
        {
            Element?.Command?.Execute(null);
        }
    }
}
