namespace Xamarin.Core.Extensions;

public static class StringExtensions
{
    public static string FirstToUpper(this string input)
    {
        if (!string.IsNullOrWhiteSpace(input))
        {
            return char.ToUpper(input[0]) + input.Substring(1);
        }
        else
        {
            return input;
        }
    }
}