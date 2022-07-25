using System;
using System.Reflection;
using Android.Text;
using Xamarin.Forms;
using View = Android.Views.View;


namespace XamarinForms.Droid.Helpers;

public static class KeyboardHelper
{
    private const string KeyboardExtensions = "Xamarin.Forms.Platform.Android.KeyboardExtensions, Xamarin.Forms.Platform.Android";

    private const string KeyboardExtensionsToInputType = "ToInputType";

    private const string KeyboardManager = "Xamarin.Forms.Platform.Android.KeyboardManager, Xamarin.Forms.Platform.Android";

    private const string KeyboardManagerHideKeyboard = "HideKeyboard";

    private const string KeyboardManagerShowKeyboard = "ShowKeyboard";

    private const BindingFlags StaticMethodBindingFlags = BindingFlags.NonPublic | BindingFlags.Static;

    public static InputTypes GetInputType(Keyboard keyboard)
    {
        var type = Type.GetType(KeyboardExtensions, true);
        if (type == null)
            throw new NullReferenceException(nameof(type));

        var method = type.GetMethod(KeyboardExtensionsToInputType);
        if (method == null)
            throw new MissingMethodException(type.Name, KeyboardExtensionsToInputType);

        return (InputTypes)method.Invoke(null, new object[] { keyboard });
    }

    public static void HideKeyboard(View view)
    {
        var type = Type.GetType(KeyboardManager, true);
        if (type == null)
            return;

        var method = type.GetMethod(KeyboardManagerHideKeyboard, StaticMethodBindingFlags);

        if (method == null)
        {
            throw new MissingMethodException(type.Name, KeyboardManagerHideKeyboard);
        }

        method.Invoke(null, new object[] { view });
    }

    public static void ShowKeyboard(View view)
    {
        var type = Type.GetType(KeyboardManager, true);
        if (type == null)
            return;

        var method = type.GetMethod(KeyboardManagerShowKeyboard, StaticMethodBindingFlags);

        if (method == null)
        {
            throw new MissingMethodException(type.Name, KeyboardManagerShowKeyboard);
        }

        method.Invoke(null, new object[] { view });
    }
}