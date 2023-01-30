using XamarinForms.Core.Infrastructure.Interfaces;

namespace XamarinForms.Core.PlatformServices;

public interface IToastService
{
    void ShowToast(string text, bool isLong = true);
}