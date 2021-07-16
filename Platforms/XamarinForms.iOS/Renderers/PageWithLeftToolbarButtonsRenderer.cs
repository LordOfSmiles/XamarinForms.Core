using System.Collections.Generic;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace XamarinForms.iOS.Renderers
{
    public abstract class PageWithLeftToolbarButtonsRenderer:PageRenderer
    {
        public new Page Element => (Page)base.Element;

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var leftNavList = new List<UIBarButtonItem>();
            var rightNavList = new List<UIBarButtonItem>();

            if (NavigationController?.TopViewController?.NavigationItem == null)
                return;
            
            var navigationItem = NavigationController.TopViewController.NavigationItem;

            if (navigationItem.RightBarButtonItems?.Length == 0)
                return;

            for (var i = 0; i < Element.ToolbarItems.Count; i++)
            {
                var reorder = Element.ToolbarItems.Count - 1;
                var itemPriority = Element.ToolbarItems[reorder - i].Priority;

                if (itemPriority == 1)
                {
                    var leftNavItems = navigationItem.RightBarButtonItems[i];
                    leftNavList.Add(leftNavItems);
                }
                else if (itemPriority == 0)
                {
                    var rightNavItems = navigationItem.RightBarButtonItems[i];
                    rightNavList.Add(rightNavItems);
                }
            }

            navigationItem.SetLeftBarButtonItems(leftNavList.ToArray(), false);
            navigationItem.SetRightBarButtonItems(rightNavList.ToArray(), false);
        }
    }
}