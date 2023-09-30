namespace Xamarin.Core.Extensions;

public static class TimeSpanExtensions
{
    public static bool IsZero(this TimeSpan timeSpan)
    {
        return timeSpan == TimeSpan.Zero;
    }

    public static bool IsGreatThanZero(this TimeSpan timeSpan)
    {
        return timeSpan > TimeSpan.Zero;
    }

    public static bool IsGreatOrEqualsThanZero(this TimeSpan timeSpan)
    {
        return timeSpan >= TimeSpan.Zero;
    }

    public static bool IsLessThanZero(this TimeSpan timeSpan)
    {
        return timeSpan < TimeSpan.Zero;
    }

    public static bool IsLessOrEqualsThanZero(this TimeSpan timeSpan)
    {
        return timeSpan <= TimeSpan.Zero;
    }

    public static bool HasHours(this TimeSpan timeSpan, int hours = 0) => (int)timeSpan.TotalHours > hours;

    public static bool HasMinutes(this TimeSpan timeSpan, int minutes = 0) => (int)timeSpan.TotalMinutes > minutes;
}