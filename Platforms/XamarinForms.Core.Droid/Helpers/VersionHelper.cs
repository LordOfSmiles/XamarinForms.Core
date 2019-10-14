using Android.OS;

namespace XamarinForms.Core.Droid.Helpers
{
    public static class VersionHelper
    {
        public static bool IsAndroid10 => Build.VERSION.SdkInt > BuildVersionCodes.P;
        public static bool IsAndroid9 => Build.VERSION.SdkInt >= BuildVersionCodes.P;
        public static bool IsAndroid8 => Build.VERSION.SdkInt >= BuildVersionCodes.O;
    }
}