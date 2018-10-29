using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;
using StoreKit;
using Xamarin.Core.Standard.Infrastructure.InAppBilling;

namespace XamarinForms.Core.iOS.Infrastructure.InAppBilling
{
    internal sealed class ProductRequestDelegate : NSObject, ISKProductsRequestDelegate, ISKRequestDelegate
    {
        private readonly TaskCompletionSource<IEnumerable<SKProduct>> _tcsResponse = new TaskCompletionSource<IEnumerable<SKProduct>>();

        public Task<IEnumerable<SKProduct>> WaitForResponse() =>_tcsResponse.Task;


        [Export("request:didFailWithError:")]
        public void RequestFailed(SKRequest request, NSError error) =>_tcsResponse.TrySetException(new InAppBillingPurchaseException(PurchaseError.ProductRequestFailed, error.LocalizedDescription));


        public void ReceivedResponse(SKProductsRequest request, SKProductsResponse response)
        {
            var product = response.Products;

            if (product != null)
            {
                _tcsResponse.TrySetResult(product);
                return;
            }

            _tcsResponse.TrySetException(new InAppBillingPurchaseException(PurchaseError.InvalidProduct, "Invalid Product"));
        }
    }
}