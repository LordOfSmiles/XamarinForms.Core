namespace XamarinForms.Core.PlatformServices
{
    public interface IToastPlatformService
    {
        void ShowAlert(string title, string message);
        void ShowAlert(string message);
    }
}