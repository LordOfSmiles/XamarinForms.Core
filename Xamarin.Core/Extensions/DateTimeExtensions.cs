namespace Xamarin.Core.Extensions;

public static class DateTimeExtensions
{
    public static DateTime ToNow(this DateTime? date)
    {
        return date ?? DateTime.Now;
    }
}