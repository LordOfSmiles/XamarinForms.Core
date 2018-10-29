
using Foundation;
using UIKit;
using XamarinForms.Core.iOS.Helpers;
using XamarinForms.Core.iOS.Infrastructure;
using XamarinForms.Core.Standard.Infrastructure.Interfaces;

[assembly:Xamarin.Forms.Dependency(typeof(ToastService))]  
namespace XamarinForms.Core.iOS.Infrastructure
{
    public sealed class ToastService:IToast
    {
        public void ShowAlert(string title, string message)
        {
            ShowAlert(title, message, LongDelay);
        }

        public void ShowAlert(string message)
        {
            ShowAlert(null, message, LongDelay);
        }

        #region Fields
        
        private const double LongDelay = 2.5;


        private NSTimer _alertDelay;
        private UIAlertController _alert;
        
        #endregion
        
        #region Private Methods

        private void ShowAlert(string title,string message, double seconds)  
        {  
            _alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>  
            {  
                DismissMessage();  
            });  
            _alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            
            var visibleViewController = UIViewControllerHelper.GetVisibleViewController();
            visibleViewController.PresentViewController(_alert, true, null); 
        }

        private void DismissMessage()  
        {
            _alert?.DismissViewController(true, null);
            _alertDelay?.Dispose();
        }  
        
        #endregion
    }
}