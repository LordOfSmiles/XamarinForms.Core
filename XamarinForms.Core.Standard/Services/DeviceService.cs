using System;
using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Services
{
    public static class DeviceService
    {
        public static T OnPlatform<T>(T iOs, T android, T uwp)
        {
            T result;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    result = iOs;
                    break;
                case Device.Android:
                    result = android;
                    break;
                case Device.UWP:
                    result = uwp;
                    break;
                default:
                    result = default(T);
                    break;
            }

            return result;
        }

        public static T OnPlatform<T>(T iOs, T android)
        {
            T result;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    result = iOs;
                    break;
                case Device.Android:
                    result = android;
                    break;
                default:
                    result = default(T);
                    break;
            }

            return result;
        }

        public static T OnIdiom<T>(T phone, T tablet)
        {
            T result;

            switch (Device.Idiom)
            {
                case TargetIdiom.Phone:
                    result = phone;
                    break;
                case TargetIdiom.Tablet:
                    result = tablet;
                    break;
                default:
                    result = default(T);
                    break;
            }

            return result;
        }

        public static T OnIdiom<T>(T phone, T tablet, T desktop)
        {
            T result;

            switch (Device.Idiom)
            {
                case TargetIdiom.Phone:
                    result = phone;
                    break;
                case TargetIdiom.Tablet:
                    result = tablet;
                    break;
                case TargetIdiom.Desktop:
                    result = desktop;
                    break;
                default:
                    result = default(T);
                    break;
            }

            return result;
        }
    }
}
