using Xamarin.Forms.Platform.iOS;

namespace XamarinForms.iOS.Renderers;

public sealed class NoBounceScrollViewRenderer : ScrollViewRenderer
{
 
    protected override void OnElementChanged(VisualElementChangedEventArgs e)
    {
        base.OnElementChanged(e);
        UpdateScrollView();
    }
 
    private void UpdateScrollView()
    {
        ContentInset = new UIKit.UIEdgeInsets(0, 0, 0, 0);
            
        if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            ContentInsetAdjustmentBehavior = UIKit.UIScrollViewContentInsetAdjustmentBehavior.Never;
            
        Bounces = false;
            
        ScrollIndicatorInsets = new UIKit.UIEdgeInsets(0, 0, 0, 0);
    }
}