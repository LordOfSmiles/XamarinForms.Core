using Xamarin.Forms.Platform.iOS;
using XamarinForms.Core.PlatformServices;
using XamarinForms.iOS.Controls.Toast;

namespace XamarinForms.iOS.PlatformServices;

public sealed class ToastService : IToastService
{
    public void Show(string text, bool isLong = true)
    {
        var duration = isLong
                           ? ToastDuration.Long
                           : ToastDuration.Regular;

        var appearance = new ToastAppearance();

        if (Application.Current?.RequestedTheme == OSAppTheme.Dark)
        {
            appearance.MessageColor = UIColor.White;
            appearance.Color = Color.FromRgb(70, 64, 107).ToUIColor();
        }
        else
        {
            appearance.MessageColor = UIColor.Black;
            appearance.Color = Color.FromRgb(245, 245, 245).ToUIColor();
        }

        Toast.MakeToast(text)
             .SetAppearance(appearance)
             .SetPosition(ToastPosition.Bottom)
             .SetDuration(duration)
             .SetShowShadow(true)
             .Show();
    }
}