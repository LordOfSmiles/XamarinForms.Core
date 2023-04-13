namespace XamarinForms.Core.PlatformServices;

public interface IToastService
{
    void ShowToast(ToastConfig config);
}

public sealed class ToastConfig
{
    public string Text { get; set; }
    public bool IsLong { get; set; } = true;
    
    public Color TextColor { get; set; }
    public Color BackgroundColor { get; set; }
}
