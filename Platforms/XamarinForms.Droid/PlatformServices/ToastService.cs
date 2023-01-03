using Android.Widget;
using XamarinForms.Core.PlatformServices;

namespace XamarinForms.Droid.PlatformServices;

public sealed class ToastService : IToastService
{
    public void Show(string text, bool isLong)
    {
        var length = isLong
                         ? ToastLength.Long
                         : ToastLength.Short;

        var toast = Toast.MakeText(Xamarin.Essentials.Platform.AppContext, text, length);
        toast?.Show();
    }
}