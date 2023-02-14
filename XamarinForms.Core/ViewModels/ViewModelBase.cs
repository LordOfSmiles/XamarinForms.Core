using Xamarin.Core.Models;
using Xamarin.Essentials;
using XamarinForms.Core.Helpers;

namespace XamarinForms.Core.ViewModels;

public abstract class ViewModelBase : NotifyObject
{
    public static string NeedRefreshDataKey => nameof(NeedRefreshDataKey);
    protected static string GoBackKey => nameof(GoBackKey);

    #region Navigation

    public virtual Task OnAppearingAsync(IDictionary<string, object> navigationParameters)
    {
        return Task.CompletedTask;
    }

    public virtual void OnDisappearing()
    { }

    public virtual void Cleanup()
    { }

    #endregion

    #region Protected Methods

    protected void ShowAnimation()
    {
        IsAnimationVisible = true;
        InputTransparent = true;
    }

    protected void HideAnimation()
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
        OnPropertyChanged(nameof(BottomButtonIndents));
        OnPropertyChanged(nameof(LeftRightPagePadding));
    }

    #endregion

    #region Fields

    protected bool IsInitCompleted;

    #endregion

    #region Constructor

    protected ViewModelBase()
    {
        if (!DeviceHelper.IsPhone)
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

    public bool IsPhone => DeviceHelper.IsPhone;
    public bool IsTablet => DeviceHelper.IsTablet;

    public DisplayOrientation Orientation => DeviceDisplay.MainDisplayInfo.Orientation;

    public virtual Thickness LeftRightPagePadding
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

    public virtual Thickness StartEndHorizontalPadding
    {
        get
        {
            int side;

            if (DeviceHelper.IsPhone)
            {
                side = 16;
            }
            else
            {
                side = DeviceHelper.IsPortrait
                           ? 32
                           : 48;
            }

            return new Thickness(side, 0);
        }
    }

    public virtual Thickness ToolbarIndents
    {
        get
        {
            var result = LeftRightPagePadding;
            result.Top = 8;
            result.Bottom = 8;

            return result;
        }
    }

    public virtual Thickness BottomButtonIndents
    {
        get
        {
            var result = LeftRightPagePadding;
            result.Bottom = 16;

            return result;
        }
    }

    #endregion
}