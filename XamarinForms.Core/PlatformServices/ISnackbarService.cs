namespace XamarinForms.Core.PlatformServices;

public interface ISnackbarService
{
    void ShowSnackbar(string text);

    void ShowSnackbar(string text, Color backgroundColor);
}