using Android.Graphics;
using Google.Android.Material.Snackbar;
using Xamarin.Forms.Platform.Android;
using XamarinForms.Core.Helpers;
using XamarinForms.Core.PlatformServices;

namespace XamarinForms.Droid.PlatformServices;

public sealed class DroidSnackService : IDroidSnackService
{
    public void ShowSnack(ToastConfig config)
    {
        var view = Xamarin.Essentials.Platform.CurrentActivity.FindViewById(Android.Resource.Id.Content);
        var snack = Snackbar.Make(view, config.Text, BaseTransientBottomBar.LengthLong);

        snack.SetBackgroundTint(config.BackgroundColor.ToAndroid());

        snack.SetTextColor(config.BackgroundColor.IsDark()
                               ? Color.White
                               : Color.Black);

        snack.Show();
    }
}