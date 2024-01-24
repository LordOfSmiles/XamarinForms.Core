namespace XamarinForms.Core.Behaviors;

public sealed class FocusOnTapBehavior:Behavior<View>
{
    #region FocusedControl

    public static readonly BindableProperty FocusedViewProperty = BindableProperty.Create(nameof(FocusedView),
                                                                                          typeof(View),
                                                                                          typeof(FocusOnTapBehavior));

    public View FocusedView
    {
        get => (View)GetValue(FocusedViewProperty);
        set => SetValue(FocusedViewProperty, value);
    }
    
    #endregion

    protected override void OnAttachedTo(View bindable)
    {
        base.OnAttachedTo(bindable);

        var gesture = new TapGestureRecognizer();
        gesture.Tapped += OnTapped;
        bindable.GestureRecognizers.Add(gesture);
    }

    protected override void OnDetachingFrom(View bindable)
    {
        var gesture = bindable.GestureRecognizers[0];
        if (gesture is TapGestureRecognizer tap)
        {
            tap.Tapped -= OnTapped;
        }
        bindable.GestureRecognizers.Clear();
        
        base.OnDetachingFrom(bindable);
    }
    
    private void OnTapped(object sender, EventArgs e)
    {
        FocusedView?.Focus();
    }
}