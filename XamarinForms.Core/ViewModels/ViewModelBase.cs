using Xamarin.Core.Infrastructure.Container;
using Xamarin.Core.Interfaces;
using Xamarin.Core.Models;
using Xamarin.Essentials;
using XamarinForms.Core.Helpers;

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
    { }

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

    #region Dependencies

    protected readonly IAnalyticsService AnalyticsService;
    protected readonly ICrashlyticsService CrashlyticsService;

    #endregion

    #region Constructor

    protected ViewModelBase()
    {
        AnalyticsService = FastContainer.TryResolve<IAnalyticsService>();
        CrashlyticsService = FastContainer.TryResolve<ICrashlyticsService>();

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

    public ICommand CloseCommand => new Command(OnClose);

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

    public virtual Thickness ToolbarIndents
    {
        get
        {
            var topBottom = 4;
            var side = DeviceHelper.OnIdiom(8, LeftRightPadding.Left);

            return new Thickness(side, topBottom);
        }
    }

    #endregion
}