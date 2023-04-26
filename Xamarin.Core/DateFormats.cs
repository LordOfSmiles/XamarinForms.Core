namespace Xamarin.Core;

public static class DateFormats
{
    public static string dd_MMMM => string.Intern("dd MMMM");
    public static string dd_MMM => string.Intern("dd MMM");
    public static string ddMMyyyy => string.Intern("dd.MM.yyyy");
}

public static class TimeFormats
{
    public static string HH_mm => string.Intern("HH:mm");
}