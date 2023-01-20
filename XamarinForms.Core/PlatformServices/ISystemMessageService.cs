using XamarinForms.Core.Infrastructure.Interfaces;

namespace XamarinForms.Core.PlatformServices;

public interface ISystemMessageService
{
    void ShowToast(string text, bool isLong = true);

    void ShowSnackbar(string text, Color backgroundColor);
}