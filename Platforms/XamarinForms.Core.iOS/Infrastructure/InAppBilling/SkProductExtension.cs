using System;
using Foundation;
using StoreKit;

namespace XamarinForms.Core.iOS.Infrastructure.InAppBilling
{
    internal static class SkProductExtension
    {
        /// <remarks>
        /// Use Apple's sample code for formatting a SKProduct price
        /// https://developer.apple.com/library/ios/#DOCUMENTATION/StoreKit/Reference/SKProduct_Reference/Reference/Reference.html#//apple_ref/occ/instp/SKProduct/priceLocale
        /// Objective-C version:
        ///    NSNumberFormatter *numberFormatter = [[NSNumberFormatter alloc] init];
        ///    [numberFormatter setFormatterBehavior:NSNumberFormatterBehavior10_4];
        ///    [numberFormatter setNumberStyle:NSNumberFormatterCurrencyStyle];
        ///    [numberFormatter setLocale:product.priceLocale];
        ///    NSString *formattedString = [numberFormatter stringFromNumber:product.price];
        /// </remarks>
        public static string LocalizedPrice(this SKProduct product)
        {
            if (product?.PriceLocale == null)
                return string.Empty;

            var formatter = new NSNumberFormatter()
            {
                FormatterBehavior = NSNumberFormatterBehavior.Version_10_4,
                NumberStyle = NSNumberFormatterStyle.Currency,
                Locale = product.PriceLocale
            };
            var formattedString = formatter.StringFromNumber(product.Price);
            Console.WriteLine(" ** formatter.StringFromNumber(" + product.Price + ") = " + formattedString + " for locale " + product.PriceLocale.LocaleIdentifier);
            return formattedString;
        }

        public static string LocalizedPrice(this SKProductDiscount product)
        {
            if (product?.PriceLocale == null)
                return string.Empty;

            var formatter = new NSNumberFormatter()
            {
                FormatterBehavior = NSNumberFormatterBehavior.Version_10_4,
                NumberStyle = NSNumberFormatterStyle.Currency,
                Locale = product.PriceLocale
            };
            var formattedString = formatter.StringFromNumber(product.Price);
            Console.WriteLine(" ** formatter.StringFromNumber(" + product.Price + ") = " + formattedString + " for locale " + product.PriceLocale.LocaleIdentifier);
            return formattedString;
        }
    }
}