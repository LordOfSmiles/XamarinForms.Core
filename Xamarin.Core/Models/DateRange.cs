namespace Xamarin.Core.Models;

public readonly struct DateRange
{
    public DateRange(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public readonly DateTime Start { get; }
    public readonly DateTime End { get; }
}