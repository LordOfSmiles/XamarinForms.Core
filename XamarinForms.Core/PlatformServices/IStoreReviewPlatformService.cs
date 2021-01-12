namespace XamarinForms.Core.PlatformServices
{
    public interface IStoreReviewPlatformService
    {
        /// <summary>
        /// Opens the store listing.
        /// </summary>
        /// <param name="appId">App identifier.</param>
        void OpenStoreListing(string appId);

        /// <summary>
        /// Opens the store review page.
        /// </summary>
        /// <param name="appId">App identifier.</param>
        void OpenStoreReviewPage(string appId);

        /// <summary>
        /// Requests an app review.
        /// </summary>
        void RequestReview();
    }
}