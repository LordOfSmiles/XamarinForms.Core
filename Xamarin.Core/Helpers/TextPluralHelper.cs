using System;
using System.Globalization;

namespace Xamarin.Core.Helpers
{
    public static class TextPluralHelper
    {
        private static readonly int[] PluralCases = { 2, 0, 1, 1, 1, 2 };

        public static string GetPluralForm(CultureInfo culture, string format, long number, params string[] words)
        {
            return string.Format(culture, format, number, GetPluralForm(number, words));
        }

        public static string GetPluralForm(long number, params string[] words)
        {
            if (number < 0)
                number = 0;

            var result = string.Empty;

            var remainder100 = number % 100;
            var wordIndex = remainder100 is > 4 and < 20
                ? 2 
                : PluralCases[Math.Min(number % 10, 5)];

            if (words != null && words.Length > wordIndex)
                result = words[wordIndex];

            return result;
        }
    }
}
