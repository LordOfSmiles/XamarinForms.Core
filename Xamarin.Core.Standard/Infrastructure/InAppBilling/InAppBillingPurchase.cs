using System;
using System.Collections.Generic;

namespace Xamarin.Core.Standard.Infrastructure.InAppBilling
{
	public sealed class InAppBillingPurchaseComparer : IEqualityComparer<InAppBillingPurchase>
	{
		public bool Equals(InAppBillingPurchase x, InAppBillingPurchase y) => x.Equals(y);

		public int GetHashCode(InAppBillingPurchase x) => x.GetHashCode();
	}

	/// <summary>
	/// Purchase from in app billing
	/// </summary>
	public sealed class InAppBillingPurchase : IEquatable<InAppBillingPurchase>
    {
        /// <summary>
        /// 
        /// </summary>
        public InAppBillingPurchase()
        {
        }

        /// <summary>
        /// Purchase/Order Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Trasaction date in UTC
        /// </summary>
        public DateTime TransactionDateUtc { get; set; }

        /// <summary>
        /// Product Id/Sku
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// Indicates whether the subscritpion renewes automatically. If true, the sub is active, else false the user has canceled.
        /// </summary>
        public bool AutoRenewing { get; set; }

        /// <summary>
        /// Unique token identifying the purchase for a given item
        /// </summary>
        public string PurchaseToken { get; set; }

        /// <summary>
        /// Gets the current purchase/subscription state
        /// </summary>
        public PurchaseState State { get; set; }

        /// <summary>
        /// Gets the current consumption state
        /// </summary>
        public ConsumptionState ConsumptionState { get; set; }

        /// <summary>
        /// Developer payload
        /// </summary>
        public string Payload { get; set; }

		public static bool operator ==(InAppBillingPurchase left, InAppBillingPurchase right) =>
			Equals(left, right);

		public static bool operator !=(InAppBillingPurchase left, InAppBillingPurchase right) =>
			!Equals(left, right);

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((InAppBillingPurchase) obj);
		}

	    //public bool Equals(InAppBillingPurchase other) =>(Id, ProductId, AutoRenewing, PurchaseToken, State, Payload) == (other.Id, other.ProductId, other.AutoRenewing, other.PurchaseToken, other.State, other.Payload);

	    public bool Equals(InAppBillingPurchase other)
	    {
		    if (ReferenceEquals(null, other)) return false;
		    if (ReferenceEquals(this, other)) return true;
		    return string.Equals(Id, other.Id) && TransactionDateUtc.Equals(other.TransactionDateUtc) && string.Equals(ProductId, other.ProductId) && AutoRenewing == other.AutoRenewing && string.Equals(PurchaseToken, other.PurchaseToken) && State == other.State && ConsumptionState == other.ConsumptionState && string.Equals(Payload, other.Payload);
	    }
	    
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (Id != null ? Id.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ TransactionDateUtc.GetHashCode();
				hashCode = (hashCode * 397) ^ (ProductId != null ? ProductId.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ AutoRenewing.GetHashCode();
				hashCode = (hashCode * 397) ^ (PurchaseToken != null ? PurchaseToken.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (int) State;
				hashCode = (hashCode * 397) ^ (int) ConsumptionState;
				hashCode = (hashCode * 397) ^ (Payload != null ? Payload.GetHashCode() : 0);
				return hashCode;
			}
		}

	    /// <summary>
		/// Prints out product
		/// </summary>
		/// <returns></returns>
		public override string ToString() => 
			$"ProductId:{ProductId} | AutoRenewing:{AutoRenewing} | State:{State} | Id:{Id}";

	    
    }

}
