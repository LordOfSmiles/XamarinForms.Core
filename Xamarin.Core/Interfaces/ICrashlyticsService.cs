using System;
using System.Collections.Generic;

namespace Xamarin.Core.Interfaces
{
    public interface ICrashlyticsService
    {
        void TrackError(Exception ex);

        void TrackError(Exception ex, IDictionary<object, object> userInfo);
    }
}