using System;
using Foundation;
using StoreKit;
using Xamarin.Core.Standard.Infrastructure.InAppBilling;

namespace XamarinForms.Core.iOS.Infrastructure.InAppBilling
{
    internal static class SkTransactionExtensions
    {
        public static string ToStatusString(this SKPaymentTransaction transaction) =>
            transaction?.ToIabPurchase()?.ToString() ?? string.Empty;


        public static InAppBillingPurchase ToIabPurchase(this SKPaymentTransaction transaction)
        {
            var p = transaction?.OriginalTransaction ?? transaction;

            if (p == null)
                return null;


            return new InAppBillingPurchase
            {
                TransactionDateUtc = NsDateToDateTimeUtc(transaction.TransactionDate),
                Id = p.TransactionIdentifier,
                ProductId = p.Payment?.ProductIdentifier ?? string.Empty,
                State = p.GetPurchaseState(),
                PurchaseToken = p.TransactionReceipt?.GetBase64EncodedString(NSDataBase64EncodingOptions.None) ?? string.Empty
            };
        }

        private static DateTime NsDateToDateTimeUtc(NSDate date)
        {
            var reference = new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            return reference.AddSeconds(date?.SecondsSinceReferenceDate ?? 0);
        }

        public static PurchaseState GetPurchaseState(this SKPaymentTransaction transaction)
        {

            if (transaction?.TransactionState == null)
                return PurchaseState.Unknown;

            switch (transaction.TransactionState)
            {
                case SKPaymentTransactionState.Restored:
                    return PurchaseState.Restored;
                case SKPaymentTransactionState.Purchasing:
                    return PurchaseState.Purchasing;
                case SKPaymentTransactionState.Purchased:
                    return PurchaseState.Purchased;
                case SKPaymentTransactionState.Failed:
                    return PurchaseState.Failed;
                case SKPaymentTransactionState.Deferred:
                    return PurchaseState.Deferred;
            }

            return PurchaseState.Unknown;
        }


    }
}