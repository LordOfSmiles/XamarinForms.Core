using Xamarin.Core.Interfaces;

namespace Xamarin.Core.Helpers;

public static class SupportOrientationHelper
{
    public static void RaiseOrientationChange(IEnumerable<ISupportOrientation> items)
    {
        foreach (var supportOrientation in items)
        {
            supportOrientation.RaiseOrientationChange();
        }
    }
}