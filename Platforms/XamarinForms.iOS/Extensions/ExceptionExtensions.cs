using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;

namespace XamarinForms.iOS.Extensions
{
    public static class ExceptionExtensions
    {
        public static NSError ToNsError(this Exception ex)
        {
            var crashInfo = new Dictionary<object, object>
            {
                [NSError.LocalizedDescriptionKey] = ex.Message ?? string.Empty,
                ["StackTrace"] = ex.StackTrace ?? string.Empty
            };

            var error = new NSError(new NSString(ex.GetType().FullName),
                -1,
                NSDictionary.FromObjectsAndKeys(crashInfo.Values.ToArray(), crashInfo.Keys.ToArray(), crashInfo.Count));

            return error;
        }
    }
}