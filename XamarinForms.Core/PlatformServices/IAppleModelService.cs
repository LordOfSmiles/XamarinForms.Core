namespace XamarinForms.Core.PlatformServices;

public interface IAppleModelService
{
    bool IsProMax { get; }
    
    bool Is5SOrSE2016 { get; }
}