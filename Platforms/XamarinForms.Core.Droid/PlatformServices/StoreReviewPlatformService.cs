using System;
using Android.App;
using Android.Content;
using Android.OS;
using XamarinForms.Core.Droid.PlatformServices;
using XamarinForms.Core.PlatformServices;

[assembly: Xamarin.Forms.Dependency(typeof(StoreReviewPlatformService))]
namespace XamarinForms.Core.Droid.PlatformServices
{
    public sealed class StoreReviewPlatformService : IStoreReviewPlatformService
    {
        public void OpenStoreListing(string appId) => OpenStoreReviewPage(appId);

        /// <summary>
        /// Opens the store review page.
        /// </summary>
        /// <param name="appId">App identifier.</param>
        public void OpenStoreReviewPage(string appId)
        {
            var url = $"market://details?id={appId}";

            try
            {
                var intent = GetRateIntent(url);
                Application.Context.StartActivity(intent);
                return;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to launch app store: " + ex.Message);
            }

            url = $"https://play.google.com/store/apps/details?id={appId}";

            try
            {
                var intent = GetRateIntent(url);
                Application.Context.StartActivity(intent);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to launch app store: " + ex.Message);
            }
        }

        /// <summary>
        /// Requests an app review.
        /// </summary>
        public void RequestReview()
        {
        }

        #region Private Methods

        private static Intent GetRateIntent(string url)
        {
            var intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(url));

            intent.AddFlags(ActivityFlags.NoHistory);
            intent.AddFlags(ActivityFlags.MultipleTask);

            if ((int) Build.VERSION.SdkInt >= 21)
            {
                intent.AddFlags(ActivityFlags.NewDocument);
            }
            else
            {
                intent.AddFlags(ActivityFlags.ClearWhenTaskReset);
            }

            intent.SetFlags(ActivityFlags.ClearTop);
            intent.SetFlags(ActivityFlags.NewTask);
            return intent;
        }

        #endregion
    }
}