using System;
using System.Text;
using Android.Provider;
using Xamarin.Essentials;
using XamarinForms.Core.PlatformServices;

namespace XamarinForms.Droid.PlatformServices;

public sealed class CommonDependencyService : ICommonDependencyService
{
    public string DeviceUuid
    {
        get
        {
            var secureId = Settings.Secure.GetString(Platform.AppContext.ContentResolver, Settings.Secure.AndroidId);
            var ba = Encoding.UTF8.GetBytes(secureId);
            var hexString3 = BitConverter.ToInt32(ba);
            return hexString3.ToString();
        }
    }
}