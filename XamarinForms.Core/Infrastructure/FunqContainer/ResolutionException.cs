using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XamarinForms.Core.Infrastructure.FunqContainer
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
