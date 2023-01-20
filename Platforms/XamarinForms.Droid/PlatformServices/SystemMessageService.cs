using Android.Content;
using Android.Content.Res;
using Android.Views;
using Android.Widget;
using Google.Android.Material.Snackbar;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinForms.Core.Helpers;
using XamarinForms.Core.Infrastructure.Interfaces;
using XamarinForms.Core.PlatformServices;
using Color = Android.Graphics.Color;
using View = Android.Views.View;

namespace XamarinForms.Droid.PlatformServices;

public sealed class SystemMessageService : ISystemMessageService
{
    public void ShowToast(string text, bool isLong = true)
    {
        var length = isLong
                         ? ToastLength.Long
                         : ToastLength.Short;

        var toast = Toast.MakeText(Xamarin.Essentials.Platform.AppContext, text, length);
        toast?.Show();
    }

    public void ShowSnackbar(string text, Xamarin.Forms.Color backgroundColor)
    {
        var view = Xamarin.Essentials.Platform.CurrentActivity.FindViewById(Android.Resource.Id.Content);
        var snack = Snackbar.Make(view, text, Snackbar.LengthLong);
        snack.SetBackgroundTint(backgroundColor.ToAndroid());
        snack.Show();
    }

    #region Private Methods

    private static bool IsDarkTheme() => Application.Current?.RequestedTheme == OSAppTheme.Dark;

    #endregion
}