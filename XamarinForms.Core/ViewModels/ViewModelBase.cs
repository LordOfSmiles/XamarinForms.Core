using Xamarin.Core.Models;
using Xamarin.Essentials;
using XamarinForms.Core.Helpers;
using XamarinForms.Core.PlatformServices;

namespace XamarinForms.Core.ViewModels;

public abstract class ViewModelBase : NotifyObject
{
    public static string NeedRefreshDataKey => nameof(NeedRefreshDataKey);
    protected static string GoBackKey => nameof(GoBackKey);

    #region Navigation

    public virtual Task OnAppearingAsync(IDictionary<string, object> navParameters)
    {
        return Task.CompletedTask;
    }

    public virtual void OnDisappearing()
    { }

    public virtual void OnCleanup()
    {
        IsInitCompleted = false;
    }

    #endregion

    #region Protected Methods

    public void ShowAnimation()
    {
        IsAnimationVisible = true;
        InputTransparent = true;
    }

    public void HideAnimation()
    {
        IsAnimationVisible = false;
        InputTransparent = false;
    }

    protected virtual async void OnClose()
    {
        if (Shell.Current?.Navigation != null)
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }

    protected static T ParseNavParameters<T>(IDictionary<string, object> navParams, string key, T initialValue = default)
    {
        return navParams.TryGetValue(key, out var value)
                   ? (T)value
                   : initialValue;
    }

    protected virtual void OnOrientationChanged()
    {
        OnPropertyChanged(nameof(Orientation));
        OnPropertyChanged(nameof(ToolbarIndents));
        OnPropertyChanged(nameof(LeftRightPadding));
    }

    #endregion

    #region Fields

    protected bool IsInitCompleted;

    #endregion
    
    #region Constructor

    protected ViewModelBase()
    {
        if (DeviceHelper.IsTablet)
        {
            DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
        }
    }

    private void OnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
    {
        OnOrientationChanged();
    }

    #endregion

    #region Commands

    public ICommand CloseCommand => CommandHelper.Create(OnClose);

    #endregion

    #region Properties

    public bool InputTransparent
    {
        get => _inputTransparent;
        set => SetProperty(ref _inputTransparent, value);
    }
    private bool _inputTransparent;

    public bool IsAnimationVisible
    {
        get => _isAnimationVisible;
        protected set => SetProperty(ref _isAnimationVisible, value);
    }
    private bool _isAnimationVisible;

    public DisplayOrientation Orientation => DeviceDisplay.MainDisplayInfo.Orientation;

    public virtual Thickness LeftRightPadding
    {
        get
        {
            Thickness result;

            if (DeviceHelper.IsPhone)
            {
                result = new Thickness(16, 0);
            }
            else
            {
                var side = DeviceHelper.IsPortrait
                               ? 32
                               : 48;

                result = new Thickness(side, 0);
            }

            return result;
        }
    }

    public Thickness TopBottomPadding
    {
        get
        {
            double bottom = 72;

            if (DeviceHelper.IsIos
                && DependencyService.Get<IIosDependencyService>().IsDeviceWithSafeArea)
            {
                bottom = 96;
            }

            return new Thickness(0, 16, 0, bottom);
        }
    }

    public virtual Thickness ToolbarIndents
    {
        get
        {
            var side = DeviceHelper.OnIdiom(8, LeftRightPadding.Left);

            return new Thickness(side, 4);
        }
    }

    #endregion
}