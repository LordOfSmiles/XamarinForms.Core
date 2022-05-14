namespace Xamarin.Core.Helpers;

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

    public static bool IsDateIntersected(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
    {
        return start2 <= end1 && start1 <= end2;
    }

    public static bool IsDateIntersected(DateTime start, DateTime end, DateTime date)
    {
        return start <= date && date <= end;
    }

    public static DateTime RoundDate(DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);
    }
        
    public static string ParseToTimeString(DateTime date)
    {
        return TimeSpanHelper.ParseToString(date.TimeOfDay);
    }
}