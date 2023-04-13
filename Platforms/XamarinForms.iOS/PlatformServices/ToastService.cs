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
            CornerRadius = 16,
            Color = config.BackgroundColor.ToUIColor(),
            MessageColor = config.TextColor.ToUIColor(),
            MessageFont = UIFont.SystemFontOfSize(15, UIFontWeight.Medium)
        };

        var layout = new ToastLayout()
        {
            PaddingLeading = 12,
            PaddingTrailing = 12,
            PaddingTop = 8,
            PaddingBottom = 8,
            MarginBottom = 32
        };

        Toast.MakeToast(config.Text)
             .SetAppearance(appearance)
             .SetDuration(ToastDuration.Long)
             .SetPosition(ToastPosition.Bottom)
             .SetLayout(layout)
             .SetShowShadow(!ThemeHelper.IsDarkTheme)
             .Show();
    }

    #region Private Methods

    private static bool IsDarkTheme() => Application.Current?.RequestedTheme == OSAppTheme.Dark;

    #endregion
}