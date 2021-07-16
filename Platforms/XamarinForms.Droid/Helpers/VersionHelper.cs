using Android.OS;

namespace XamarinForms.Droid.Helpers
{
    public static class VersionHelper
    {
        public static bool IsAndroid10AndHigher => Build.VERSION.SdkInt >= BuildVersionCodes.Q;
        public static bool IsAndroid9AndHigher => Build.VERSION.SdkInt >= BuildVersionCodes.P;
        public static bool IsAndroid8AndHigher => Build.VERSION.SdkInt >= BuildVersionCodes.O;
        public static bool IsAndroid6AndHigher => Build.VERSION.SdkInt >= BuildVersionCodes.M;
        public static bool IsAndroid5AndHigher => Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop;
    }
}