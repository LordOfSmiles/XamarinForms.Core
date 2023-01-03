namespace XamarinForms.Core.PlatformServices;

public interface IToastService
{
    void Show(string text, bool isLong = true);
}