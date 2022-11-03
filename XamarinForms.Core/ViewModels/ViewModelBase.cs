using Xamarin.Core.Models;
using Xamarin.Essentials;

namespace XamarinForms.Core.ViewModels;

public abstract class ViewModelBase : NotifyObject
{
    public const string NeedRefreshDataKey = "NeedRefreshData";
    protected const string GoBackKey = "BackNavigation";

    #region Public Methods

    public virtual Task OnAppearingAsync(IDictionary<string, object> navigationParameters)
    {
        return Task.CompletedTask;
    }

    public virtual void OnDisappearing()
    {
    }

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
        OnPropertyChanged(nameof(ContentIndents));
    }

    #endregion

    #region Fields

    protected bool IsInitCompleted;

    #endregion

    #region Constructor

    protected ViewModelBase()
    {
        if (Device.Idiom != TargetIdiom.Phone)
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
        protected set => SetProperty(ref _inputTransparent, value);
    }
    private bool _inputTransparent;

    public bool IsAnimationVisible
    {
        get => _isAnimationVisible;
        protected set => SetProperty(ref _isAnimationVisible, value);
    }
    private bool _isAnimationVisible;

    public bool IsPhone => Device.Idiom == TargetIdiom.Phone;
    public bool IsTablet => Device.Idiom == TargetIdiom.Tablet;

    public DisplayOrientation Orientation => DeviceDisplay.MainDisplayInfo.Orientation;

    public virtual Thickness ContentIndents
    {
        get
        {
            Thickness result;

            if (IsPhone)
            {
                result = new Thickness(8, 0);
            }
            else
            {
                var side = Orientation == DisplayOrientation.Portrait
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
            Thickness result;

            if (IsPhone)
            {
                result = new Thickness(8, 8);
            }
            else
            {
                var side = Orientation == DisplayOrientation.Portrait
                    ? 32
                    : 48;

                result = new Thickness(side, 8);
            }

            return result;
        }
    }

    public virtual Thickness BottomButtonIndents
    {
        get
        {
            Thickness result;

            if (IsPhone)
            {
                result = new Thickness(8, 0, 8, 16);
            }
            else
            {
                result = Orientation == DisplayOrientation.Portrait
                    ? new Thickness(32, 0, 32, 16)
                    : new Thickness(48, 0, 48, 16);
            }

            return result;
        }
    }

    #endregion
}