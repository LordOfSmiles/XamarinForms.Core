using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Xamarin.Core.Standard.Infrastructure.Container
{
    [DataContract]
    public class ResolutionException : Exception
    {
        public ResolutionException(Type missingServiceType)
            : base(string.Format(CultureInfo.CurrentCulture, "Missing type", (object)missingServiceType.FullName))
        {
        }

        public ResolutionException(Type missingServiceType, string missingServiceName)
            : base(string.Format(CultureInfo.CurrentCulture, "Missing named type", (object)missingServiceType.FullName, (object)missingServiceName))
        {
        }

        public ResolutionException(string message)
            : base(message)
        {
        }
    }
}
