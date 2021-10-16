using System;
using System.Collections.Generic;

namespace Xamarin.Core.Interfaces
{
    public interface ICrashlytics
    {
        void TrackError(Exception ex);

        void TrackError(Exception ex, IDictionary<string, string> userInfo);
    }
}