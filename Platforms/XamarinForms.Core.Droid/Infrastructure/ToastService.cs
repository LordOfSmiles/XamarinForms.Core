
using Android.Widget;
using XamarinForms.Core.Droid.Infrastructure;
using XamarinForms.Core.Standard.Infrastructure.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(ToastService))]
namespace XamarinForms.Core.Droid.Infrastructure
{
    public sealed class ToastService:IToast
    {
        public void ShowAlert(string title, string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }

        public void ShowAlert(string message)
        {
            ShowAlert(null, message);
        }
    }
}