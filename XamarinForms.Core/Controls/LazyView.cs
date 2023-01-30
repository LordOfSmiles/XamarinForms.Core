using XamarinForms.Core.Helpers.TaskMonitor;

namespace XamarinForms.Core.Controls;

public interface ILazyView
{
    View Content { get; set; }

    Color AccentColor { get; }

    bool IsLoaded { get; }

    void LoadView();
}

public abstract class ALazyView : ContentView, ILazyView, IDisposable, IAnimatableReveal
{
    public static readonly BindableProperty AccentColorProperty = BindableProperty.Create(nameof(AccentColor),
                                                                                          typeof(Color),
                                                                                          typeof(ILazyView),
                                                                                          Color.Accent,
                                                                                          propertyChanged: AccentColorChanged);

    public static readonly BindableProperty UseActivityIndicatorProperty = BindableProperty.Create(nameof(UseActivityIndicator),
                                                                                                   typeof(bool),
                                                                                                   typeof(ILazyView),
                                                                                                   false,
                                                                                                   propertyChanged: UseActivityIndicatorChanged);

    public static readonly BindableProperty AnimateProperty = BindableProperty.Create(nameof(Animate),
                                                                                      typeof(bool),
                                                                                      typeof(ILazyView),
                                                                                      false);

    public Color AccentColor
    {
        get => (Color)GetValue(AccentColorProperty);
        set => SetValue(AccentColorProperty, value);
    }

    public bool UseActivityIndicator
    {
        get => (bool)GetValue(UseActivityIndicatorProperty);
        set => SetValue(UseActivityIndicatorProperty, value);
    }

    public bool Animate
    {
        get => (bool)GetValue(AnimateProperty);
        set => SetValue(AnimateProperty, value);
    }

    public bool IsLoaded { get; protected set; }

    public abstract void LoadView();

    public void Dispose()
    {
        if (Content is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    protected override void OnBindingContextChanged()
    {
        if (Content != null
            && !(Content is ActivityIndicator))
        {
            Content.BindingContext = BindingContext;
        }
    }

    private static void AccentColorChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        var lazyView = (ILazyView)bindable;
        if (lazyView.Content is ActivityIndicator activityIndicator)
        {
            activityIndicator.Color = (Color)newvalue;
        }
    }

    private static void UseActivityIndicatorChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        var lazyView = (ILazyView)bindable;
        bool useActivityIndicator = (bool)newvalue;

        if (useActivityIndicator)
        {
            lazyView.Content = new ActivityIndicator
            {
                Color = lazyView.AccentColor,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                IsRunning = true,
            };
        }
    }
}

public class LazyView<TView> : ALazyView
    where TView : View, new()
{
    public override void LoadView()
    {
        IsLoaded = true;

        View view = new TView { BindingContext = BindingContext };

        Content = view;
    }
}

public class DelayedView : ALazyView
{
    public static readonly BindableProperty ViewProperty = BindableProperty.Create(nameof(View),
                                                                                   typeof(View),
                                                                                   typeof(DelayedView),
                                                                                   default(View));

    public View View
    {
        get => (View)GetValue(ViewProperty);
        set => SetValue(ViewProperty, value);
    }

    public int DelayInMilliseconds { get; set; } = 200;

    public override void LoadView()
    {
        if (IsLoaded)
        {
            return;
        }

        TaskMonitor.Create(async () =>
        {
            await Task.Delay(DelayInMilliseconds);

            if (IsLoaded)
            {
                return;
            }

            IsLoaded = true;
            Content = View;
        });
    }
}

public class DelayedView<TView> : LazyView<TView>
    where TView : View, new()
{
    public int DelayInMilliseconds { get; set; } = 200;

    public override void LoadView()
    {
        TaskMonitor.Create(async () =>
        {
            View? view = null;
            if (Device.RuntimePlatform == Device.Android)
            {
                await Task.Run(() =>
                {
                    view = new TView
                    {
                        BindingContext = BindingContext,
                    };
                });
            }
            else
            {
                view = new TView
                {
                    BindingContext = BindingContext,
                };
            }

            await Task.Delay(DelayInMilliseconds);

            IsLoaded = true;
            Content = view;
        });
    }
}

public interface IAnimatableReveal
{
    bool Animate { get; }
}