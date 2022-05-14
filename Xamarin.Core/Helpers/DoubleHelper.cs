namespace Xamarin.Core.Helpers;

public static class DoubleHelper
{
    public static double Max(double a, double b)
    {
        return a > b
            ? a
            : b;
    }
}