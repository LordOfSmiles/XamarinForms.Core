using UIKit;
using Xamarin.Forms;
using XamarinForms.Core.iOS.PlatformServices;
using XamarinForms.Core.PlatformServices;

[assembly:Dependency(typeof(ExtendedDevicePlatformService))]
namespace XamarinForms.Core.iOS.PlatformServices
{
    public sealed class ExtendedDevicePlatformService:IExtendedDevicePlatformService
    {
        public bool IsDeviceWithSafeArea
        {
            get
            {
                var result = false;

                if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
                {
                    if (UIScreen.MainScreen.Bounds.Height==480)
                    {
                        //iPhone 4S
                    }
                    else if (UIScreen.MainScreen.Bounds.Height==568)
                    {
                        //iPhone 5, 5S, SE
                    }
                    else if (UIScreen.MainScreen.Bounds.Height== 66)
                    {
                        //iPhone 6/6S/7/8
                    }
                    else if (UIScreen.MainScreen.Bounds.Height == 736 && UIScreen.MainScreen.Scale == 2208)
                    {
                        //iPhone 6+/6S+/7+/8+
                    }
                    else if (UIScreen.MainScreen.Bounds.Height == 812)
                    {
                        //Console.WriteLine("iPhone X, XS, 11 Pro, 12 mini");
                        //iPhone X, XS, 11
                        result = true;
                    }
                    else if (UIScreen.MainScreen.Bounds.Height  == 896)
                    {
                        //iPhone XS Max, 11 Pro Max";
                        result = true;
                    }
                }

                return result;
            }
        }
    }
}