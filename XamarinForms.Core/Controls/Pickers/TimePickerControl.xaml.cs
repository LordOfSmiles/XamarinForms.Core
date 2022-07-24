using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using TimePicker = Xamarin.Forms.TimePicker;

namespace XamarinForms.Core.Controls.Pickers;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class TimePickerControl
{
    #region Fields

    private bool _isTimeChanged;
    private bool _isOpened;

    #endregion

    public TimePickerControl()
    {
        InitializeComponent();

        timePicker.On<iOS>().SetUpdateMode(UpdateMode.Immediately);
    }

    #region Events

    public event EventHandler<TimeSpan?> TimeChanged;

    #endregion

    #region Command

    protected override void OnOpenPicker()
    {
        if (!IsEnabled)
            return;

        _isTimeChanged = false;

        TimeSpan initialTime;

        if (!SelectedTime.HasValue && DefaultTime.HasValue)
        {
            initialTime = DefaultTime.Value;
        }
        else if (SelectedTime.HasValue)
        {
            initialTime = SelectedTime.Value;
        }
        else
        {
            initialTime = DateTime.Now.TimeOfDay;
        }

        timePicker.Time = initialTime;

        if (Device.RuntimePlatform == Device.iOS)
        {
            timePicker.DoneEvent += OnDone;
        }
        else
        {
            timePicker.Focused += OnPickerFocused;
            timePicker.PropertyChanged += OnPickerPropertyChanged;
            timePicker.Unfocused += OnPickerUnfocused;
        }

        timePicker.Focus();
    }

    #endregion

    #region Bindable Properties

    #region SelectedTime

    public static readonly BindableProperty SelectedTimeProperty = BindableProperty.Create(nameof(SelectedTime),
        typeof(TimeSpan?),
        typeof(TimePickerControl),
        null,
        BindingMode.TwoWay,
        coerceValue: OnSelectedTimeCoerced,
        propertyChanged: OnSelectedTimeChanged);

    public TimeSpan? SelectedTime
    {
        get => (TimeSpan?)GetValue(SelectedTimeProperty);
        set => SetValue(SelectedTimeProperty, value);
    }

    private static object OnSelectedTimeCoerced(BindableObject bindable, object value)
    {
        var ctrl = (TimePickerControl)bindable;
        var time = (TimeSpan?)value;

        var timeToSet = time ?? TimeSpan.Zero;

        if (ctrl.MinimumTime <= timeToSet && timeToSet <= ctrl.MaximumTime)
        {
            return time;
        }
        else
        {
            return ctrl.SelectedTime;
        }
    }

    private static void OnSelectedTimeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = (TimePickerControl)bindable;

        var time = (TimeSpan?)newValue;

        var timeToSet = time ?? TimeSpan.Zero;
        ctrl.timePicker.Time = timeToSet;
    }

    #endregion

    #region DefaultTime

    public static readonly BindableProperty DefaultTimeProperty = BindableProperty.Create(nameof(DefaultTime),
        typeof(TimeSpan?),
        typeof(TimePickerControl),
        DateTime.Now.TimeOfDay);

    public TimeSpan? DefaultTime
    {
        get => (TimeSpan?)GetValue(DefaultTimeProperty);
        set => SetValue(DefaultTimeProperty, value);
    }

    #endregion

    #region MinimumTime

    public static readonly BindableProperty MinimumTimeProperty = BindableProperty.Create(nameof(MinimumTime),
        typeof(TimeSpan),
        typeof(TimePickerControl),
        TimeSpan.Zero);

    public TimeSpan MinimumTime
    {
        get => (TimeSpan)GetValue(MinimumTimeProperty);
        set => SetValue(MinimumTimeProperty, value);
    }

    #endregion

    #region MaximumTime

    public static readonly BindableProperty MaximumTimeProperty = BindableProperty.Create(nameof(MaximumTime),
        typeof(TimeSpan),
        typeof(TimePickerControl),
        new TimeSpan(23, 59, 59));

    public TimeSpan MaximumTime
    {
        get => (TimeSpan)GetValue(MaximumTimeProperty);
        set => SetValue(MaximumTimeProperty, value);
    }

    #endregion

    #endregion

    #region Handlers

    private void OnPickerFocused(object sender, FocusEventArgs e)
    {
        _isOpened = true;
    }

    private void OnPickerPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == TimePicker.TimeProperty.PropertyName)
        {
            _isTimeChanged = _isOpened && timePicker.Time != SelectedTime;
        }
    }

    private void OnPickerUnfocused(object sender, FocusEventArgs e)
    {
        _isOpened = false;

        if (_isTimeChanged && SelectedTime != timePicker.Time)
        {
            SelectedTime = timePicker.Time;
            TimeChanged?.Invoke(this, timePicker.Time);
            AcceptCommand?.Execute(null);
        }

        timePicker.Focused -= OnPickerFocused;
        timePicker.PropertyChanged -= OnPickerPropertyChanged;
        timePicker.Unfocused -= OnPickerUnfocused;
    }

    private void OnDone(object sender, EventArgs e)
    {
        if (SelectedTime != timePicker.Time)
        {
            SelectedTime = timePicker.Time;

            TimeChanged?.Invoke(this, timePicker.Time);
            AcceptCommand?.Execute(timePicker.Time);
        }

        timePicker.DoneEvent -= OnDone;
    }

    #endregion
}