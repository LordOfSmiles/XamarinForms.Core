using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Behaviors
{
    public sealed class TapBehavior:BehaviorBase<View>
    {
        #region BehaviorBase

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);
            
            _tapGesture=new TapGestureRecognizer();
            _tapGesture.Tapped += OnTapped;
            bindable.GestureRecognizers.Add(_tapGesture);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            base.OnDetachingFrom(bindable);

            if (_tapGesture != null)
            {
                _tapGesture.Tapped -= OnTapped;
                bindable.GestureRecognizers.Remove(_tapGesture);
                _tapGesture = null;
            }
        }

        #endregion
        
        #region Fields

        private TapGestureRecognizer _tapGesture;

        #endregion
        
        #region Handlers
        
        private async void OnTapped(object sender, EventArgs e)
        {
            var view = sender as View;
            if (view == null)
                return;

            await view.ScaleTo(0.95);
            await view.ScaleTo(1);
        }
        
        #endregion
    }
}