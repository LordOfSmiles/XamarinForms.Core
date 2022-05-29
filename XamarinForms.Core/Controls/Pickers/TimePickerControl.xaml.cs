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

    #endregion

    public TimePickerControl()
    {
        InitializeComponent();

        timePicker.On<iOS>().SetUpdateMode(UpdateMode.Immediately);
    }

    #region Command

    protected override void OnOpenPicker()
    {
        if (!SelectedTime.HasValue && DefaultTime.HasValue)
        {
            timePicker.Time = DefaultTime.Value;
        }
        else if (SelectedTime.HasValue)
        {
            timePicker.Time = SelectedTime.Value;
        }

        timePicker.PropertyChanged += OnPickerPropertyChanged;
        timePicker.Unfocused += OnPickerUnfocused;
        timePicker.Focus();
    }

    #endregion

    #region Bindable Properties

    #region Time

    public static readonly BindableProperty SelectedTimeProperty = BindableProperty.Create(nameof(SelectedTime),
        typeof(TimeSpan?),
        typeof(TimePickerControl),
        null,
        BindingMode.TwoWay,
        propertyChanged: OnSelectedTimeChanged);

    public TimeSpan? SelectedTime
    {
        get => (TimeSpan?)GetValue(SelectedTimeProperty);
        set => SetValue(SelectedTimeProperty, value);
    }

    private static void OnSelectedTimeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = (TimePickerControl)bindable;

        var time = (TimeSpan?)newValue;

        var timeToSet = time ?? TimeSpan.Zero;

        if (ctrl.MinimumTime <= timeToSet && timeToSet <= ctrl.MaximumTime)
        {
            ctrl.timePicker.Time = time ?? TimeSpan.Zero;
        }
        else
        {
            var oldTime = (TimeSpan?)oldValue;
            ctrl.timePicker.Time = oldTime ?? DateTime.Now.TimeOfDay;
        }
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
        TimeSpan.MaxValue);

    public TimeSpan MaximumTime
    {
        get => (TimeSpan)GetValue(MaximumTimeProperty);
        set => SetValue(MaximumTimeProperty, value);
    }

    #endregion

    #region WithDone

    public static readonly BindableProperty WithFinishedUpdateModeProperty = BindableProperty.Create(nameof(WithFinishedUpdateMode),
        typeof(bool),
        typeof(TimePickerControl),
        false,
        propertyChanged: OnWithDoneChanged);

    public bool WithFinishedUpdateMode
    {
        get => (bool)GetValue(WithFinishedUpdateModeProperty);
        set => SetValue(WithFinishedUpdateModeProperty, value);
    }

    private static void OnWithDoneChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = (TimePickerControl)bindable;

        var withDone = (bool)newValue;
        if (withDone)
        {
            ctrl.timePicker.On<iOS>().SetUpdateMode(UpdateMode.WhenFinished);
        }
        else
        {
            ctrl.timePicker.On<iOS>().SetUpdateMode(UpdateMode.Immediately);
        }
    }

    #endregion

    #endregion

    #region Handlers

    private void OnPickerPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == TimePicker.TimeProperty.PropertyName)
        {
            SelectedTime = timePicker.Time;
            AcceptCommand?.Execute(null);
        }
    }

    private void OnPickerUnfocused(object sender, FocusEventArgs e)
    {
        timePicker.PropertyChanged -= OnPickerPropertyChanged;
        timePicker.Unfocused -= OnPickerUnfocused;
    }

    #endregion
}