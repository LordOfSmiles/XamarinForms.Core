using Xamarin.Forms.Platform.iOS;
using XamarinForms.Core.Helpers;
using XamarinForms.Core.PlatformServices;
using XamarinForms.iOS.Controls.Toast;

namespace XamarinForms.iOS.PlatformServices;

public sealed class ToastService : IToastService
{
    public void ShowToast(ToastConfig config)
    {
        var appearance = new ToastAppearance()
        {
            Color = config.BackgroundColor.ToUIColor(),
            CornerRadius = 20,
            MessageColor = config.BackgroundColor.IsDark()
                               ? UIColor.White
                               : UIColor.Black,
            MessageFont = UIFont.SystemFontOfSize(15, UIFontWeight.Medium)
        };

        var layout = new ToastLayout()
        {
            PaddingLeading = 16,
            PaddingTrailing = 16,
            PaddingTop = 12,
            PaddingBottom = 12
        };

        Toast.MakeToast(config.Text)
             .SetAppearance(appearance)
             .SetDuration(ToastDuration.Long)
             .SetPosition(ToastPosition.Center)
             .SetLayout(layout)
             .SetShowShadow(false)
             .Show();
    }

    #region Private Methods

    private static bool IsDarkTheme() => Application.Current?.RequestedTheme == OSAppTheme.Dark;

    #endregion
}