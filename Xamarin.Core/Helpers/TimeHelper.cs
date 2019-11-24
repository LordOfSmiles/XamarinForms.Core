using System;

namespace Xamarin.Core.Helpers
{
    public static class TimeHelper
    {
        public static bool IsTimeIntersected(TimeSpan bottomBound, TimeSpan topBound, TimeSpan value)
        {
            var result = false;

            if (bottomBound <= value && value <= topBound)
            {
                result = true;
            }
            
            return result;
        }
    }
}