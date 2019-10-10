using Android.OS;
using Android.Views;

namespace XamarinForms.Core.Droid.Helpers
{
    public static class VersionHelper
    {
        public static bool IsAndroid8 => Build.VERSION.SdkInt >= BuildVersionCodes.O;

    }
}