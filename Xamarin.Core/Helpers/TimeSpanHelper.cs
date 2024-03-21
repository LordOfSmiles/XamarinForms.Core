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
        if (start < start2
            && start2 < end)
        {
            //start date of 2nd span within range of first span
            return true;
        }
        else if (start < end2
                 && end2 < end)
        {
            //end date of 2nd span within range of first span
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 02:34 входной формат
    /// </summary>
    /// <param name="timeString"></param>
    /// <returns></returns>
    public static TimeSpan FromString(string timeString)
    {
        var result = TimeSpan.Zero;

        if (!string.IsNullOrWhiteSpace(timeString))
        {
            string hoursString;
            string minutesString;

            var isNegative = false;

            if (timeString.Length == 6
                && timeString[0] == '-')
            {
                isNegative = true;

                hoursString = timeString.Substring(1, 2);
                minutesString = timeString.Substring(4, 2);
            }
            else
            {
                hoursString = timeString.Substring(0, 2);
                minutesString = timeString.Substring(3, 2);
            }

            if (int.TryParse(hoursString, out var hours)
                && int.TryParse(minutesString, out var minutes))
            {
                result = TimeSpan.FromMinutes(hours * 60 + minutes);

                if (isNegative)
                {
                    result = result.Negate();
                }
            }
        }

        return result;
    }

    public static string ToSumString(TimeSpan time, string placeholder = "")
    {
        if (time.IsZero())
        {
            return placeholder;
        }
        else
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
    }

    public static string ToTimeString(TimeSpan time, string placeholder = "")
    {
        if (time.IsZero())
        {
            return placeholder;
        }
        else
        {
            return $"{time.Hours:D2}:{time.Minutes:D2}";
        }
    }

    public static string ToString(TimeSpan time, bool withPlus = false)
    {
        var hours = Math.Abs(time.Hours);
        var minutes = Math.Abs(time.Minutes);

        var result = time.IsLessThanZero()
                         ? "-"
                         : string.Empty;

        result += $"{hours:D2}:{minutes:D2}";

        return result;
    }


    public static TimeSpan GetDuration(DateTime start, DateTime? end)
    {
        var roundedStart = DateHelper.RoundDate(start);
        var roundedEnd = DateHelper.RoundDate(end ?? NodaTimeHelper.Now);

        var result = roundedEnd - roundedStart;
        return result;
    }

    public static bool IsLess(DateTime start, DateTime? end, TimeSpanHelperEnum comparison, int count)
    {
        var duration = GetDuration(start, end);

        if (comparison == TimeSpanHelperEnum.Hours)
        {
            return duration.TotalHours < count;
        }
        else
        {
            return duration.TotalDays < count;
        }
    }

    public static bool IsGreat(DateTime start, DateTime? end, TimeSpanHelperEnum comparison, int count)
    {
        var duration = GetDuration(start, end);

        if (comparison == TimeSpanHelperEnum.Hours)
        {
            return duration.TotalHours > count;
        }
        else
        {
            return duration.TotalDays > count;
        }
    }
}

public enum TimeSpanHelperEnum
{
    Hours,
    Days
}