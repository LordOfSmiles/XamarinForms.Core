using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using StoreKit;
using UIKit;
using Xamarin.Core.Standard.Infrastructure.InAppBilling;
using XamarinForms.Core.iOS.Infrastructure.InAppBilling;

[assembly:Xamarin.Forms.Dependency(typeof(InAppBillingImplementation))]
namespace XamarinForms.Core.iOS.Infrastructure.InAppBilling
{
	/// <summary>
	/// Implementation for InAppBilling
	/// </summary>
	public sealed class InAppBillingImplementation : BaseInAppBilling
	{
		#region IInAppBilling

		/// <summary>
		/// Gets or sets if in testing mode. Only for UWP
		/// </summary>
		public override bool InTestingMode { get; set; }

		/// <summary>
		/// Connect to billing service
		/// </summary>
		/// <returns>If Success</returns>
		public override Task<bool> ConnectAsync(ItemType itemType = ItemType.InAppPurchase) => Task.FromResult(true);

		/// <summary>
		/// Disconnect from the billing service
		/// </summary>
		/// <returns>Task to disconnect</returns>
		public override Task DisconnectAsync() => Task.CompletedTask;

		/// <summary>
		/// Get product information of a specific product
		/// </summary>
		/// <param name="productIds">Sku or Id of the product(s)</param>
		/// <param name="itemType">Type of product offering</param>
		/// <returns></returns>
		public override async Task<IEnumerable<InAppBillingProduct>> GetProductInfoAsync(ItemType itemType, params string[] productIds)
		{
			var products = await GetProductAsync(productIds);

			return products.Select(p => new InAppBillingProduct
			{
				LocalizedPrice = p.LocalizedPrice(),
				MicrosPrice = (long) (p.Price.DoubleValue * 1000000d),
				Name = p.LocalizedTitle,
				ProductId = p.ProductIdentifier,
				Description = p.LocalizedDescription,
				CurrencyCode = p.PriceLocale?.CurrencyCode ?? string.Empty,
				LocalizedIntroductoryPrice = IsiOs112 ? (p.IntroductoryPrice?.LocalizedPrice() ?? string.Empty) : string.Empty,
				MicrosIntroductoryPrice = IsiOs112 ? (long) ((p.IntroductoryPrice?.Price?.DoubleValue ?? 0) * 1000000d) : 0
			});
		}

		/// <summary>
		/// Get all current purchase for a specifiy product type.
		/// </summary>
		/// <param name="itemType">Type of product</param>
		/// <param name="verifyPurchase">Interface to verify purchase</param>
		/// <returns>The current purchases</returns>
		public override async Task<IEnumerable<InAppBillingPurchase>> GetPurchasesAsync(ItemType itemType, IInAppBillingVerifyPurchase verifyPurchase = null)
		{
			var purchases = await RestoreAsync();

			if (purchases == null)
				return null;

			var comparer = new InAppBillingPurchaseComparer();
			var converted = purchases
				.Where(p => p != null)
				.Select(p2 => p2.ToIabPurchase())
				.Distinct(comparer);

			//try to validate purchases
			var valid = await ValidateReceipt(verifyPurchase, string.Empty, string.Empty);

			return valid ? converted : null;
		}

		/// <summary>
		/// Purchase a specific product or subscription
		/// </summary>
		/// <param name="productId">Sku or ID of product</param>
		/// <param name="itemType">Type of product being requested</param>
		/// <param name="payload">Developer specific payload</param>
		/// <param name="verifyPurchase">Interface to verify purchase</param>
		/// <returns></returns>
		public override async Task<InAppBillingPurchase> PurchaseAsync(string productId, ItemType itemType, string payload, IInAppBillingVerifyPurchase verifyPurchase = null)
		{
			var p = await PurchaseAsync(productId);

			var reference = new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

			var purchase = new InAppBillingPurchase
			{
				TransactionDateUtc = reference.AddSeconds(p.TransactionDate.SecondsSinceReferenceDate),
				Id = p.TransactionIdentifier,
				ProductId = p.Payment?.ProductIdentifier ?? string.Empty,
				State = p.GetPurchaseState(),
				PurchaseToken = p.TransactionReceipt?.GetBase64EncodedString(NSDataBase64EncodingOptions.None) ?? string.Empty
			};

			if (verifyPurchase == null)
				return purchase;

			var validated = await ValidateReceipt(verifyPurchase, purchase.ProductId, purchase.Id);

			return validated ? purchase : null;
		}

		/// <summary>
		/// Consume a purchase with a purchase token.
		/// </summary>
		/// <param name="productId">Id or Sku of product</param>
		/// <param name="purchaseToken">Original Purchase Token</param>
		/// <returns>If consumed successful</returns>
		/// <exception cref="InAppBillingPurchaseException">If an error occures during processing</exception>
		public override Task<InAppBillingPurchase> ConsumePurchaseAsync(string productId, string purchaseToken) =>
			null;

		/// <summary>
		/// Consume a purchase
		/// </summary>
		/// <param name="productId">Id/Sku of the product</param>
		/// <param name="payload">Developer specific payload of original purchase</param>
		/// <param name="itemType">Type of product being consumed.</param>
		/// <param name="verifyPurchase">Verify Purchase implementation</param>
		/// <returns>If consumed successful</returns>
		/// <exception cref="InAppBillingPurchaseException">If an error occures during processing</exception>
		public override Task<InAppBillingPurchase> ConsumePurchaseAsync(string productId, ItemType itemType, string payload, IInAppBillingVerifyPurchase verifyPurchase = null) =>
			null;

		public override Task<bool> FinishTransaction(InAppBillingPurchase purchase) =>
			FinishTransaction(purchase?.Id);

		public override async Task<bool> FinishTransaction(string purchaseId)
		{
			if (string.IsNullOrWhiteSpace(purchaseId))
				throw new ArgumentException("PurchaseId must be valid", nameof(purchaseId));

			var purchases = await RestoreAsync();

			if (purchases == null)
				return false;

			var transaction = purchases.FirstOrDefault(p => p.TransactionIdentifier == purchaseId);
			if (transaction == null)
				return false;

			SKPaymentQueue.DefaultQueue.FinishTransaction(transaction);

			return true;
		}

		#endregion

		#region Fields

		private PaymentObserver _paymentObserver;

		#endregion

		#region Constrcutor

		public InAppBillingImplementation()
		{
			_paymentObserver = new PaymentObserver(OnPurchaseComplete);

			SKPaymentQueue.DefaultQueue.AddTransactionObserver(_paymentObserver);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets a callback for out of band purchases to complete.
		/// </summary>
		public static Action<InAppBillingPurchase> OnPurchaseComplete { get; set; } = null;

		private static bool IsiOs112 => UIDevice.CurrentDevice.CheckSystemVersion(11, 2);

		#endregion

		#region Private methods

		private Task<IEnumerable<SKProduct>> GetProductAsync(string[] productId)
		{
			var productIdentifiers = NSSet.MakeNSObjectSet(productId.Select(i => new NSString(i)).ToArray());

			var productRequestDelegate = new ProductRequestDelegate();

			//set up product request for in-app purchase
			var productsRequest = new SKProductsRequest(productIdentifiers)
			{
				Delegate = productRequestDelegate // SKProductsRequestDelegate.ReceivedResponse
			};
			productsRequest.Start();

			return productRequestDelegate.WaitForResponse();
		}

		private Task<SKPaymentTransaction[]> RestoreAsync()
		{
			var tcsTransaction = new TaskCompletionSource<SKPaymentTransaction[]>();

			var allTransactions = new List<SKPaymentTransaction>();

			Action<SKPaymentTransaction[]> handler = null;
			handler = new Action<SKPaymentTransaction[]>(transactions =>
			{

				// Unsubscribe from future events
				_paymentObserver.TransactionsRestored -= handler;

				if (transactions == null)
				{
					if (allTransactions.Count == 0)
						tcsTransaction.TrySetException(new InAppBillingPurchaseException(PurchaseError.RestoreFailed, "Restore Transactions Failed"));
					else
						tcsTransaction.TrySetResult(allTransactions.ToArray());
				}
				else
				{
					allTransactions.AddRange(transactions);
					tcsTransaction.TrySetResult(allTransactions.ToArray());
				}
			});

			_paymentObserver.TransactionsRestored += handler;

			foreach (var trans in SKPaymentQueue.DefaultQueue.Transactions)
			{
				var original = FindOriginalTransaction(trans);
				if (original == null)
					continue;

				allTransactions.Add(original);
			}

			// Start receiving restored transactions
			SKPaymentQueue.DefaultQueue.RestoreCompletedTransactions();

			return tcsTransaction.Task;
		}


		private static SKPaymentTransaction FindOriginalTransaction(SKPaymentTransaction transaction)
		{
			if (transaction == null)
				return null;

			if (transaction.TransactionState == SKPaymentTransactionState.Purchased 
			    || transaction.TransactionState == SKPaymentTransactionState.Purchasing)
				return transaction;

			if (transaction.OriginalTransaction != null)
				return FindOriginalTransaction(transaction.OriginalTransaction);

			return transaction;

		}

		private Task<bool> ValidateReceipt(IInAppBillingVerifyPurchase verifyPurchase, string productId, string transactionId)
		{
			if (verifyPurchase == null)
				return Task.FromResult(true);

			// Get the receipt data for (server-side) validation.
			// See: https://developer.apple.com/library/content/releasenotes/General/ValidateAppStoreReceipt/Introduction.html#//apple_ref/doc/uid/TP40010573
			NSData receiptUrl = null;
			
			if (NSBundle.MainBundle.AppStoreReceiptUrl != null)
				receiptUrl = NSData.FromUrl(NSBundle.MainBundle.AppStoreReceiptUrl);

			var receipt = receiptUrl?.GetBase64EncodedString(NSDataBase64EncodingOptions.None);

			return verifyPurchase.VerifyPurchase(receipt, string.Empty, productId, transactionId);
		}


		private Task<SKPaymentTransaction> PurchaseAsync(string productId)
		{
			var tcsTransaction = new TaskCompletionSource<SKPaymentTransaction>();

			Action<SKPaymentTransaction, bool> handler = null;
			handler = new Action<SKPaymentTransaction, bool>((tran, success) =>
			{
				if (tran?.Payment == null)
					return;

				// Only handle results from this request
				if (productId != tran.Payment.ProductIdentifier)
					return;

				// Unsubscribe from future events
				_paymentObserver.TransactionCompleted -= handler;

				if (success)
				{
					tcsTransaction.TrySetResult(tran);
					return;
				}

				var errorCode = tran?.Error?.Code ?? -1;
				var description = tran?.Error?.LocalizedDescription ?? string.Empty;
				var error = PurchaseError.GeneralError;
				switch (errorCode)
				{
					case (int) SKError.PaymentCancelled:
						error = PurchaseError.UserCancelled;
						break;
					case (int) SKError.PaymentInvalid:
						error = PurchaseError.PaymentInvalid;
						break;
					case (int) SKError.PaymentNotAllowed:
						error = PurchaseError.PaymentNotAllowed;
						break;
					case (int) SKError.ProductNotAvailable:
						error = PurchaseError.ItemUnavailable;
						break;
					case (int) SKError.Unknown:
						error = PurchaseError.GeneralError;
						break;
					case (int) SKError.ClientInvalid:
						error = PurchaseError.BillingUnavailable;
						break;
				}

				tcsTransaction.TrySetException(new InAppBillingPurchaseException(error, description));

			});

			_paymentObserver.TransactionCompleted += handler;

			var payment = SKPayment.CreateFrom(productId);
			SKPaymentQueue.DefaultQueue.AddPayment(payment);

			return tcsTransaction.Task;
		}

		private static DateTime NsDateToDateTimeUtc(NSDate date)
		{
			var reference = new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

			return reference.AddSeconds(date?.SecondsSinceReferenceDate ?? 0);
		}

		#endregion

		#region IDisposable

		private bool _disposed;

		protected override void Dispose(bool disposing)
		{
			if (_disposed)
			{
				base.Dispose(disposing);
				return;
			}

			_disposed = true;

			if (!disposing)
			{
				base.Dispose(disposing);
				return;
			}

			if (_paymentObserver != null)
			{
				SKPaymentQueue.DefaultQueue.RemoveTransactionObserver(_paymentObserver);
				_paymentObserver.Dispose();
				_paymentObserver = null;
			}

			base.Dispose(disposing);
		}

		#endregion
	}

}
