using Xamarin.Core.Interfaces;
using Xamarin.Core.Models;
using XamarinForms.Core.Helpers;
using Thickness = Xamarin.Forms.Thickness;

namespace XamarinForms.Core.ViewModels.Items.Base;

public abstract class CellItemBase : NotifyObject, ISupportOrientation, IUiListItem
{
    public Thickness LeftRightPadding => new(LeftRight, Top, LeftRight, Bottom);

    protected virtual int LeftRight
    {
        get
        {
            int result;

            if (DeviceHelper.IsPhone)
            {
                result = 8;
            }
            else
            {
                result = DeviceHelper.IsPortrait
                             ? 32
                             : 48;
            }

            return result;
        }
    }
    protected virtual int Top
    {
        get
        {
            int result;

            if (DeviceHelper.IsPhone)
            {
                result = IsFirst
                             ? 12
                             : 4;
            }
            else
            {
                result = IsFirst
                             ? 14
                             : 6;
            }

            return result;
        }
    }
    protected virtual int Bottom
    {
        get
        {
            int result;

            if (DeviceHelper.IsPhone)
            {
                result = IsLast
                             ? 68
                             : 4;
            }
            else
            {
                result = IsLast
                             ? 68
                             : 6;
            }

            return result;
        }
    }

    #region ISupportOrientation

    public void RaiseOrientationChange()
    {
        OnPropertyChanged(nameof(LeftRightPadding));
    }

    #endregion

    #region IUiListItem

    public int Index { get; set; }
    public bool IsFirst
    {
        get => _isFirst;
        set
        {
            SetProperty(ref _isFirst, value);
            OnPropertyChanged(nameof(LeftRightPadding));
        }
    }
    private bool _isFirst;

    public bool IsLast
    {
        get => _isLast;
        set
        {
            SetProperty(ref _isLast, value);
            OnPropertyChanged(nameof(LeftRightPadding));
        }
    }
    private bool _isLast;

    public bool IsSingle
    {
        get => _isSingle;
        set => SetProperty(ref _isSingle, value);
    }
    private bool _isSingle;

    #endregion

    public ICommand TapCommand { get; set; }
}