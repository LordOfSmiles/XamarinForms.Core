namespace XamarinForms.Core.Standard.Infrastructure.Interfaces
{
    public interface IToast
    {
        void ShowAlert(string title, string message);
        void ShowAlert(string message);
    }
}