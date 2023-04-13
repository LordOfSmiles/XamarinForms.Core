using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using XamarinForms.Core.PlatformServices;

namespace XamarinForms.Droid.PlatformServices;

public sealed class DroidToastService : IToastService
{
    public void ShowToast(ToastConfig config)
    {
        var duration = config.IsLong
                           ? ToastLength.Long
                           : ToastLength.Short;

        var toast = Toast.MakeText(Xamarin.Essentials.Platform.AppContext, config.Text, duration);
        if (toast != null)
        {
            toast.SetGravity(GravityFlags.Bottom, 0, 128);
            toast.Show();
        }
    }

    public void ShowToast(string text)
    {
        ShowToast(new ToastConfig()
        {
            Text = text
        });
    }
}