using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinForms.Core.Infrastructure
{
    public interface IIapService
    {
        Task<NativePurchaseResult> PurchaseAsync(string productId);

        Task<IEnumerable<InAppProduct>> GetProductsAsync(IEnumerable<string> productsIds);

        Task<IEnumerable<PurchaseInfo>> GetPurchasesAsync();
    }

    public class NativePurchaseResult
    {
        public string Receipt { get; set; }
        public string PaymentId { get; set; }
    }

    public class InAppProduct
    {
        public string ProductId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }

        #region Presentation Properties

        public double PresentationPrice
        {
            get
            {
                double result = double.NaN;

                if (!string.IsNullOrEmpty(Price))
                {
                    result = double.Parse(Price);
                }

                return result;
            }
        }

        #endregion
    }

    public sealed class PurchaseInfo
    {
        public string DeveloperPayload { get; set; }
        public string OrderId { get; set; }
        public string PackageName { get; set; }
        public string ProductId { get; set; }
        public int PurchaseState { get; set; }
        public long PurchaseTime { get; set; }
        public string PurchaseToken { get; set; }
    }
}
