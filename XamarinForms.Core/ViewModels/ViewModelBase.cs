using Xamarin.Core.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinForms.Core.Helpers;

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

    protected async Task DisplayAlert(string title, string message, string cancel)
    {
        if (Application.Current?.MainPage != null)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }

    protected async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
    {
        var result = false;

        if (Application.Current?.MainPage != null)
        {
            result = await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        return result;
    }

    protected async Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
    {
        var result = string.Empty;

        if (Application.Current?.MainPage != null)
        {
            result = await Application.Current.MainPage.DisplayActionSheet(title, cancel, destruction, buttons);
        }

        return result;
    }

    protected virtual async void OnClose()
    {
        if (Shell.Current?.Navigation != null)
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }

    #endregion

    #region Fields

    protected bool IsInitCompleted;

    #endregion

    #region Constructor

    protected ViewModelBase()
    {
        DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
    }

    private void OnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
    {
        OnPropertyChanged(nameof(Orientation));
        OnPropertyChanged(nameof(ContentIndents));
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
                result = new Thickness(16, 0);
            }
            else
            {
                var side = Orientation == DisplayOrientation.Portrait
                    ? DeviceHelper.OnPlatform(128, 64)
                    : 128;

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
                result = new Thickness(16, 8);
            }
            else
            {
                var side = Orientation == DisplayOrientation.Portrait
                    ? DeviceHelper.OnPlatform(128, 64)
                    : 128;

                result = new Thickness(side, 8);
            }

            return result;
        }
    }

    #endregion
}