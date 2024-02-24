using NodaTime;
using NodaTime.Extensions;
using NodaTime.TimeZones;

namespace Xamarin.Core.Helpers;

public static class NodaTimeHelper
{
    public static DateTime Now
    {
        get
        {
            var timeZone = DateTimeZoneProviders.Tzdb.GetSystemDefault();
            var clock = SystemClock.Instance.InZone(timeZone);
            var nowDateTime = clock.GetCurrentZonedDateTime();
            var result = nowDateTime.ToDateTimeUnspecified();

            return DateTime.SpecifyKind(result, DateTimeKind.Local);
        }
    }

    public static DateTime UtcNow
    {
        get
        {
            var timeZone = DateTimeZoneProviders.Tzdb.GetSystemDefault();
            var clock = SystemClock.Instance.InZone(timeZone);
            var nowDateTime = clock.GetCurrentZonedDateTime();
            return nowDateTime.ToDateTimeUtc();
        }
    }

    public static DateTime Today
    {
        get
        {
            var timeZone = DateTimeZoneProviders.Tzdb.GetSystemDefault();
            var clock = SystemClock.Instance.InZone(timeZone);
            var nowDateTime = clock.GetCurrentDate();
            var result = nowDateTime.ToDateTimeUnspecified().Date;
            return DateTime.SpecifyKind(result, DateTimeKind.Local);
        }
    }

    public static bool IsToday(DateTime date) => date.Date == Today;

    public static bool IsLessToday(DateTime date) => date.Date < Today;

    public static bool IsLessOrEqualsToday(DateTime date) => date.Date <= Today;

    public static bool IsGreatToday(DateTime date) => date.Date > Today;

    public static bool IsGreatOrEqualsToday(DateTime date) => date.Date >= Today;

    public static Period GetPeriod(DateTime start, DateTime end, PeriodUnits units)
    {
        var periodStart = LocalDateTime.FromDateTime(start);
        var periodEnd = LocalDateTime.FromDateTime(end);

        return Period.Between(periodStart, periodEnd, units);
    }

    public static Period GetPeriod(TimeSpan start, TimeSpan end, PeriodUnits units)
    {
        var periodStart = new LocalTime(start.Hours, start.Minutes, start.Seconds);
        var periodEnd = new LocalTime(end.Hours, end.Minutes, end.Seconds);

        return Period.Between(periodStart, periodEnd, units);
    }

    public static (string Id, TimeSpan Offset) GetCurrentTimeZoneOffset()
    {
        var timeZone = DateTimeZoneProviders.Tzdb.GetSystemDefault();

        var utcNow = UtcNow;

        var nextJanuary = Instant.FromUtc(utcNow.Year + 1, 1, 1, 0, 0);
        var nextJune = Instant.FromUtc(utcNow.Month < 6
                                           ? utcNow.Year
                                           : utcNow.Year + 1,
                                       6,
                                       1,
                                       0,
                                       0);

        return (timeZone.Id, GetOffset(nextJanuary, nextJune, timeZone).ToTimeSpan());
    }

    public static IEnumerable<(string Id, TimeSpan Offset)> GetCanonicalOffsets(DateTime utcNow)
    {
        if (utcNow.Kind != DateTimeKind.Utc)
            throw new ArgumentException($"DateTimeKind must be Utc.", nameof(utcNow));

        var nextJanuary = Instant.FromUtc(utcNow.Year + 1, 1, 1, 0, 0);
        var nextJune = Instant.FromUtc(utcNow.Month < 6
                                           ? utcNow.Year
                                           : utcNow.Year + 1,
                                       6,
                                       1,
                                       0,
                                       0);

        var result = TzdbDateTimeZoneSource.Default
                                           .GetIds()
                                           .Aggregate(new HashSet<string>(),
                                                      (canonicalIds, id) =>
                                                      {
                                                          // Find the canonical zone id, as the list includes aliases:
                                                          var canonicalId = TzdbDateTimeZoneSource.Default.CanonicalIdMap[id];

                                                          // Deduplicate, as `Add` will ignore duplicates:
                                                          canonicalIds.Add(canonicalId);

                                                          return canonicalIds;
                                                      })
                                           .Where(id =>
                                           {
                                               // Filter out zones we don't want to display:
                                               return !id.StartsWith("Etc/") && id.Contains('/');
                                           })
                                           .Select(id =>
                                           {
                                               var dtz = DateTimeZoneProviders.Tzdb.GetZoneOrNull(id);

                                               return (dtz.Id, Offset: GetOffset(nextJanuary, nextJune, dtz).ToTimeSpan());
                                           })
                                           .OrderBy(x => x.Offset)
                                           .ThenBy(x => x.Id);

        return result;
    }

    #region Private Methods

    private static Offset GetOffset(Instant nextJanuary, Instant nextJune, DateTimeZone dtz)
    {
        var january = new ZonedDateTime(nextJanuary, dtz);
        if (!january.IsDaylightSavingTime())
        {
            return january.Offset;
        }

        var june = new ZonedDateTime(nextJune, dtz);
        if (!june.IsDaylightSavingTime())
        {
            return june.Offset;
        }

        // Edge case:
        // Zone is probably on permanent daylight savings.
        // Subtract an hour to get most likely UTC offset.
        return june.Offset - Offset.FromHours(1);
    }

    #endregion
}