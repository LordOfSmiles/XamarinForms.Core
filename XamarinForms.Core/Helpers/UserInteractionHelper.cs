namespace XamarinForms.Core.Helpers;

public static class UserInteractionHelper
{
    public static Task<bool> ShowConfirmationAsync(string title, string message, string accept, string cancel)
    {
        return Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
    }

    public static Task<string> ShowDestructiveConfirmationAsync(string text, string destruction, string cancel)
    {
        return Application.Current.MainPage.DisplayActionSheet(text, cancel, destruction);
    }

    public static Task ShowAlertAsync(string title, string message, string cancel)
    {
        return Application.Current.MainPage.DisplayAlert(title, message, cancel);
    }
}