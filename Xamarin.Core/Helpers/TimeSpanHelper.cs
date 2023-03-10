using Xamarin.Core.Extensions;

namespace Xamarin.Core.Helpers;

public static class TimeSpanHelper
{
    public static bool IsIntersected(TimeSpan start, TimeSpan end, TimeSpan value)
    {
        return start <= value && value <= end;
    }
        
    public static bool IsIntersectedWithoutBounds(TimeSpan start, TimeSpan end, TimeSpan value)
    {
        return start < value && value < end;
    }
        
    public static bool IsIntersected(TimeSpan start, TimeSpan end, TimeSpan start2, TimeSpan end2)
    {
        if (start < start2 && start2 < end)
        {
            //start date of 2nd span within range of first span
            return true;
        }
        else if (start < end2 && end2 < end)
        {
            //end date of 2nd span within range of first span
            return true;
        }
        else
        {
            return false;
        }
    }
        
    public static TimeSpan ParseFromString(string timeString)
    {
        var hours = int.Parse(timeString[..2]);
        var minutes = int.Parse(timeString.Substring(3, 2));

        return TimeSpan.FromMinutes(hours * 60 + minutes);
    }
        
    public static string ParseToString(TimeSpan time)
    {
        if (!time.IsZero())
        {
            var hours = (int)time.TotalHours;
            var minutes = time.Minutes;

            if (time.IsLessThanZero())
            {
                hours = Math.Abs(hours);
                minutes = Math.Abs(minutes);
            }
                
            return $"{hours:D2}:{minutes:D2}";
        }
        else
        {
            return "00:00";
        }
    }
    
        
    public static TimeSpan GetDuration(DateTime start, DateTime? end)
    {
        var roundedStart = DateHelper.RoundDate(start);
        var roundedEnd = DateHelper.RoundDate(end ?? DateTime.Now);

        var result = roundedEnd - roundedStart;
        return result;
    }
}