using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Foundation;
using StoreKit;
using Xamarin.Core.Standard.Infrastructure.InAppBilling;

namespace XamarinForms.Core.iOS.Infrastructure.InAppBilling
{
    internal sealed class PaymentObserver : SKPaymentTransactionObserver
	{
		public event Action<SKPaymentTransaction, bool> TransactionCompleted;
		public event Action<SKPaymentTransaction[]> TransactionsRestored;

		private readonly List<SKPaymentTransaction> _restoredTransactions = new List<SKPaymentTransaction>();
		private readonly Action<InAppBillingPurchase> _onPurchaseSuccess;

		public PaymentObserver(Action<InAppBillingPurchase> onPurchaseSuccess = null)
		{
			_onPurchaseSuccess = onPurchaseSuccess;
		}

		public override void UpdatedTransactions(SKPaymentQueue queue, SKPaymentTransaction[] transactions)
		{
			var rt = transactions.Where(pt => pt.TransactionState == SKPaymentTransactionState.Restored);

			// Add our restored transactions to the list
			// We might still get more from the initial request so we won't raise the event until
			// RestoreCompletedTransactionsFinished is called
			if (rt?.Any() ?? false)
				_restoredTransactions.AddRange(rt);

			foreach (var transaction in transactions)
			{
				if (transaction?.TransactionState == null)
					break;

				Debug.WriteLine($"Updated Transaction | {transaction.ToStatusString()}");

				switch (transaction.TransactionState)
				{
					case SKPaymentTransactionState.Restored:
					case SKPaymentTransactionState.Purchased:
						TransactionCompleted?.Invoke(transaction, true);

						if (TransactionCompleted != null)
							_onPurchaseSuccess?.Invoke(transaction.ToIabPurchase());

						SKPaymentQueue.DefaultQueue.FinishTransaction(transaction);
						break;
					case SKPaymentTransactionState.Failed:
						TransactionCompleted?.Invoke(transaction, false);
						SKPaymentQueue.DefaultQueue.FinishTransaction(transaction);
						break;
					default:
						break;
				}
			}
		}

		public override void RestoreCompletedTransactionsFinished(SKPaymentQueue queue)
		{
			if (_restoredTransactions == null)
				return;

			// This is called after all restored transactions have hit UpdatedTransactions
			// at this point we are done with the restore request so let's fire up the event
			var allTransactions = _restoredTransactions.ToArray();

			// Clear out the list of incoming restore transactions for future requests
			_restoredTransactions.Clear();

			TransactionsRestored?.Invoke(allTransactions);

			foreach (var transaction in allTransactions)
				SKPaymentQueue.DefaultQueue.FinishTransaction(transaction);
		}

		// Failure, just fire with null
		public override void RestoreCompletedTransactionsFailedWithError(SKPaymentQueue queue, NSError error) =>
			TransactionsRestored?.Invoke(null);

	}
}