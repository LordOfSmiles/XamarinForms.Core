using System;
using UIKit;

namespace XamarinForms.Core.iOS.Helpers
{
    public static class UIViewControllerHelper
    {
        public static UIViewController GetVisibleViewController()
        {
            UIViewController result = null;

            var rootController = UIApplication.SharedApplication?.KeyWindow?.RootViewController;
            if (rootController != null)
            {
                if (rootController.PresentedViewController != null)
                {
                    if (rootController.PresentedViewController is UINavigationController)
                    {
                        result = ((UINavigationController)rootController.PresentedViewController).VisibleViewController;
                    }
                    else if (rootController.PresentedViewController is UITabBarController)
                    {
                        result = ((UITabBarController)rootController.PresentedViewController).SelectedViewController;
                    }
                    else
                    {
                        result = rootController.PresentedViewController;
                    }
                }
                else
                {
                    result = rootController;
                }
            }

            return result;
        }
    }
}
