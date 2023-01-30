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

public sealed class ToastService : IToastService
{
    public void ShowToast(string text, bool isLong = true)
    {
        var length = isLong
                         ? ToastLength.Long
                         : ToastLength.Short;

        var toast = Toast.MakeText(Xamarin.Essentials.Platform.AppContext, text, length);
        toast?.Show();
    }

   

    #region Private Methods

    private static bool IsDarkTheme() => Application.Current?.RequestedTheme == OSAppTheme.Dark;

    #endregion
}