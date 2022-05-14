namespace XamarinForms.iOS.Helpers;

public static class UIViewControllerHelper
{
    public static UIViewController GetRootViewController()
    {
        UIViewController result = null;

        var windows = UIApplication.SharedApplication.Windows;
        foreach (var window in windows)
        {
            if (window.RootViewController != null)
            {
                result = window.RootViewController;
                break;
            }
        }

        return result;
    }

    public static UIViewController GetVisibleViewController(UIViewController controller = null)
    {
        controller = controller ?? UIApplication.SharedApplication.KeyWindow.RootViewController;

        if (controller.PresentedViewController == null)
            return controller;

        if (controller.PresentedViewController is UINavigationController)
        {
            return ((UINavigationController)controller.PresentedViewController).VisibleViewController;
        }

        if (controller.PresentedViewController is UITabBarController)
        {
            return ((UITabBarController)controller.PresentedViewController).SelectedViewController;
        }

        return GetVisibleViewController(controller.PresentedViewController);
    }
}