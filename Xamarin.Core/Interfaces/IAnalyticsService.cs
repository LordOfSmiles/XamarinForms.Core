using System.Collections.Generic;

namespace Xamarin.Core.Interfaces
{
    public interface IAnalyticsService
    {
        void TrackEvent(string name);

        void TrackEvent(string name, IDictionary<string, string> parameters);
    }
}