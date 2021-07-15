using System.Linq;

namespace Xamarin.Core.Helpers
{
    public static class IntHelper
    {
        public static bool IsIntersected(int start, int end, int value)
        {
            return start <= value && value <= end;
        }

        public static bool IsIntersected(int start, int end, int start2, int end2)
        {
            var firstRange = Enumerable.Range(start, end - start);
            var secondRange = Enumerable.Range(start2, end2 - start2);

            return firstRange.Intersect(secondRange).Any();
        }

        public static int Max(int a, int b)
        {
            return a > b
                ? a
                : b;
        }

        public static int? Max(int? a, int? b)
        {
            return a > b
                ? a
                : b;
        }
    }
}