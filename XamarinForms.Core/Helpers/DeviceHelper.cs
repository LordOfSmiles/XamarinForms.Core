namespace XamarinForms.Core.Helpers;

public static class DeviceHelper
{
    public static T OnPlatform<T>(T iOs, T android)
    {
        T result = Device.RuntimePlatform switch
        {
            Device.iOS => iOs,
            Device.Android => android,
            _ => default
        };

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

    public static T OnIdiom<T>(T phone, T tablet)
    {
        var result = Device.Idiom switch
        {
            TargetIdiom.Phone => phone,
            _ => tablet
        };

        return result;
    }
}