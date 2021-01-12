using System;
using System.Diagnostics;
using Foundation;
using StoreKit;
using UIKit;
using XamarinForms.Core.iOS.Helpers;
using XamarinForms.Core.iOS.PlatformServices;
using XamarinForms.Core.PlatformServices;

[assembly: Xamarin.Forms.Dependency(typeof(StoreReviewPlatformService))]
namespace XamarinForms.Core.iOS.PlatformServices
{
    public sealed class StoreReviewPlatformService:IStoreReviewPlatformService
    {
        public void OpenStoreListing(string appId)
        {
#if __IOS__
            var url = $"itms-apps://itunes.apple.com/app/id{appId}";
#elif __TVOS__
    			var url = $"com.apple.TVAppStore://itunes.apple.com/app/id{appId}";
#endif
            try
            {
                UIApplication.SharedApplication.OpenUrl(new NSUrl(url));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to launch app store: " + ex.Message);
            }
        }

        /// <summary>
        /// Opens the store review page.
        /// </summary>
        /// <param name="appId">App identifier.</param>
        public void OpenStoreReviewPage(string appId)
        {
#if __IOS__
            var url = $"itms-apps://itunes.apple.com/app/id{appId}?action=write-review";
#elif __TVOS__
    			var url = $"com.apple.TVAppStore://itunes.apple.com/app/id{appId}?action=write-review";
#endif
            try
            {
                UIApplication.SharedApplication.OpenUrl(new NSUrl(url));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to launch app store: " + ex.Message);
            }
        }

        /// <summary>
        /// Requests an app review.
        /// </summary>
        public void RequestReview()
        {
#if __IOS__
            if (VersionHelper.IsOs13OrHigher)
            {
                SKStoreReviewController.RequestReview();
            }
#endif
        }
    }
}