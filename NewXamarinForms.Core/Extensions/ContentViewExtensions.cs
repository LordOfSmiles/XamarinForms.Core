using System;
using Xamarin.Forms;

namespace NewXamarinForms.Core.Extensions
{
    public static class ContentViewExtensions
    {
        public static void OnCustomIdiom(this Page page, Action phoneCustomizations, Action tabletCustomizations = null, Action desktopCustomizations = null)
        {
            switch (Device.Idiom)
            {
                case TargetIdiom.Phone:
                    phoneCustomizations?.Invoke();
                    break;
                case TargetIdiom.Tablet:
                    tabletCustomizations?.Invoke();
                    break;
                case TargetIdiom.Desktop:
                    desktopCustomizations?.Invoke();
                    break;
            }
        }

        public static void OnCustomIdiom(this ContentView view, Action phoneCustomizations, Action tabletCustomizations = null, Action desktopCustomizations = null)
        {
            switch (Device.Idiom)
            {
                case TargetIdiom.Phone:
                    phoneCustomizations?.Invoke();
                    break;
                case TargetIdiom.Tablet:
                    tabletCustomizations?.Invoke();
                    break;
                case TargetIdiom.Desktop:
                    desktopCustomizations?.Invoke();
                    break;
            }
        }

        public static void OnPlatform(this View view, Action onAndroid, Action onIos = null, Action onUwp = null)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    onAndroid?.Invoke();
                    break;
                case Device.iOS:
                    onIos?.Invoke();
                    break;
                case Device.UWP:
                    onUwp?.Invoke();
                    break;
            }
        }
    }
}
