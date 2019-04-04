using System;

namespace Xamarin.Core.Standard.Helpers
{
    public static class DateHelper
    {
        public static DateTime Min(DateTime date1, DateTime date2)
        {
            return date1 < date2
                ? date1
                : date2;
        }

        public static DateTime Max(DateTime date1, DateTime date2)
        {
            return date1 > date2
                ? date1
                : date2;
        }

        public static bool IsPeriodsIntersected(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return start2 <= end1 && start1 <= end2;
        }
    }
}