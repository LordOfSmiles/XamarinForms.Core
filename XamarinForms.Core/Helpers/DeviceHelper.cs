using System;
using Xamarin.Forms;

namespace BabyDream.Infrastructure.Helpers
{
    public static class DeviceHelper
    {
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

        public static void OnPlatform(Action iOs, Action android)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    iOs.Invoke();
                    break;
                case Device.Android:
                    android.Invoke();
                    break;
            }
        }

        public static void OniOs(Action action)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                action.Invoke();
            }
        }
        
        public static void OnAndroid(Action action)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                action.Invoke();
            }
        }
    }
}
