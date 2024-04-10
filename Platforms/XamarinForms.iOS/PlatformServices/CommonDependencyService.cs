using XamarinForms.Core.PlatformServices;

namespace XamarinForms.iOS.PlatformServices;

public sealed class CommonDependencyService : ICommonDependencyService
{
    public string DeviceUuid => UIDevice.CurrentDevice.IdentifierForVendor.ToString();
}