using Google.Android.Material.Snackbar;
using Xamarin.Forms.Platform.Android;
using XamarinForms.Core.PlatformServices;

namespace XamarinForms.Droid.PlatformServices;

public sealed class SnackbarService:ISnackbarService
{
    public void ShowSnackbar(string text)
    {
        var view = Xamarin.Essentials.Platform.CurrentActivity.FindViewById(Android.Resource.Id.Content);
        var snack = Snackbar.Make(view, text, Snackbar.LengthLong);
        snack.Show();
    }

    public void ShowSnackbar(string text, Xamarin.Forms.Color backgroundColor)
    {
        var view = Xamarin.Essentials.Platform.CurrentActivity.FindViewById(Android.Resource.Id.Content);
        var snack = Snackbar.Make(view, text, Snackbar.LengthLong);
        snack.SetBackgroundTint(backgroundColor.ToAndroid());
        snack.Show();
    }
}