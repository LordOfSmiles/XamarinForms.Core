using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;

namespace XamarinForms.iOS.Controls;

public class MnFloatingActionButton : UIControl
{
    /// <summary>
    /// Flags for hiding/showing shadow
    /// </summary>
    public enum ShadowState
    {
        ShadowStateShown,
        ShadowStateHidden
    }

    /// <summary>
    /// FAB Size options.
    /// </summary>
    public enum FabSize
    {
        Mini,
        Normal
    }

    private readonly nfloat _animationDuration;
    private readonly nfloat _animationScale;
    private readonly nfloat _shadowOpacity;
    private readonly nfloat _shadowRadius;

    private FabSize _size = FabSize.Normal;

    /// <summary>
    /// Size to render the FAB -- Normal or <ini
    /// </summary>
    /// <value>The size.</value>
    public FabSize Size
    {
        get => _size;
        set
        {
            if (_size == value)
                return;

            _size = value;
            UpdateBackground();
        }
    }

    UIImageView _centerImageView;

    /// <summary>
    /// The image to display int the center of the button
    /// </summary>
    /// <value>The center image view.</value>
    public UIImageView CenterImageView
    {
        get
        {
            if (_centerImageView == null)
            {
                _centerImageView = new UIImageView();
            }

            return _centerImageView;
        }
        private set => _centerImageView = value;
    }

    UIColor _backgroundColor;

    /// <summary>
    /// Background Color of the FAB
    /// </summary>
    /// <value>The color of the background.</value>
    public new UIColor BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            _backgroundColor = value;

            UpdateBackground();
        }
    }

    UIColor _shadowColor;

    public UIColor ShadowColor
    {
        get => _shadowColor;
        set
        {
            _shadowColor = value;
            UpdateBackground();
        }
    }

    bool _hasShadow;

    public bool HasShadow
    {
        get => _hasShadow;
        set
        {
            _hasShadow = value;
            UpdateBackground();
        }
    }

    public nfloat ShadowOpacity { get; private set; }

    public nfloat ShadowRadius { get; private set; }

    public nfloat AnimationScale { get; private set; }

    public nfloat AnimationDuration { get; private set; }

    public bool IsAnimating { get; private set; }

    public UIView BackgroundCircle { get; private set; }

    public bool AnimateOnSelection { get; set; }

    public MnFloatingActionButton(bool animateOnSelection)
        : base()
    {
        _animationDuration = 0.05f;
        _animationScale = 0.85f;
        _shadowOpacity = 0.6f;
        _shadowRadius = 1.5f;
        AnimateOnSelection = animateOnSelection;

        CommonInit();
    }

    public MnFloatingActionButton(CGRect frame, bool animateOnSelection)
        : base(frame)
    {
        _animationDuration = 0.05f;
        _animationScale = 0.85f;
        _shadowOpacity = 0.6f;
        _shadowRadius = 1.5f;
        AnimateOnSelection = animateOnSelection;

        CommonInit();
    }

    public override void LayoutSubviews()
    {
        base.LayoutSubviews();

        CenterImageView.Center = BackgroundCircle.Center;
        if (!IsAnimating)
        {
            UpdateBackground();
        }
    }

    public override void TouchesBegan(NSSet touches, UIEvent evt)
    {
        base.TouchesBegan(touches, evt);

        AnimateToSelectedState();
        SendActionForControlEvents(UIControlEvent.TouchDown);
    }

    public override void TouchesEnded(NSSet touches, UIEvent evt)
    {
        AnimateToDeselectedState();
        SendActionForControlEvents(UIControlEvent.TouchUpInside);
    }

    public override void TouchesCancelled(NSSet touches, UIEvent evt)
    {
        AnimateToDeselectedState();
        SendActionForControlEvents(UIControlEvent.TouchCancel);
    }

    private void CommonInit()
    {
        BackgroundCircle = new UIView();

        BackgroundColor = UIColor.Red.ColorWithAlpha(0.4f);
        BackgroundColor = new UIColor(33.0f / 255.0f, 150.0f / 255.0f, 243.0f / 255.0f, 1.0f);
        BackgroundCircle.BackgroundColor = BackgroundColor;
        ShadowOpacity = _shadowOpacity;
        ShadowRadius = _shadowRadius;
        AnimationScale = _animationScale;
        AnimationDuration = _animationDuration;

        BackgroundCircle.AddSubview(CenterImageView);
        AddSubview(BackgroundCircle);
    }

    private void AnimateToSelectedState()
    {
        if (AnimateOnSelection)
        {
            IsAnimating = true;
            ToggleShadowAnimationToState(ShadowState.ShadowStateHidden);
            Animate(_animationDuration, () =>
            {
                BackgroundCircle.Transform = CGAffineTransform.MakeScale(AnimationScale, AnimationScale);
            }, () =>
            {
                IsAnimating = false;
            });
        }
    }

    private void AnimateToDeselectedState()
    {
        if (AnimateOnSelection)
        {
            IsAnimating = true;
            ToggleShadowAnimationToState(ShadowState.ShadowStateShown);
            Animate(_animationDuration, () =>
            {
                BackgroundCircle.Transform = CGAffineTransform.MakeScale(1.0f, 1.0f);
            }, () =>
            {
                IsAnimating = false;
            });
        }
    }

    private void ToggleShadowAnimationToState(ShadowState state)
    {
        nfloat endOpacity = 0.0f;
        if (state == ShadowState.ShadowStateShown)
        {
            endOpacity = ShadowOpacity;
        }

        CABasicAnimation animation = CABasicAnimation.FromKeyPath("shadowOpacity");
        animation.From = NSNumber.FromFloat((float)ShadowOpacity);
        animation.To = NSNumber.FromFloat((float)endOpacity);
        animation.Duration = _animationDuration;
        BackgroundCircle.Layer.AddAnimation(animation, "shadowOpacity");
        BackgroundCircle.Layer.ShadowOpacity = (float)endOpacity;
    }

    private void UpdateBackground()
    {
        BackgroundCircle.Frame = Bounds;
        BackgroundCircle.Layer.CornerRadius = Bounds.Size.Height / 2;
        BackgroundCircle.Layer.ShadowColor = ShadowColor != null ? ShadowColor.CGColor : BackgroundColor.CGColor;
        BackgroundCircle.Layer.ShadowOpacity = (float)ShadowOpacity;
        BackgroundCircle.Layer.ShadowRadius = ShadowRadius;
        BackgroundCircle.Layer.ShadowOffset = new CGSize(1.0, 1.0);
        BackgroundCircle.BackgroundColor = BackgroundColor;

        var xPos = (BackgroundCircle.Bounds.Width / 2) - 12;
        var yPos = (BackgroundCircle.Bounds.Height / 2) - 12;

        CenterImageView.Frame = new CGRect(xPos, yPos, 24, 24);
    }
}