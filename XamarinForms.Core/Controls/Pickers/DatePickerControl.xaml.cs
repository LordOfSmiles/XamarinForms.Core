using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace XamarinForms.Core.Controls.Pickers;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DatePickerControl
{
    public DatePickerControl()
    {
        InitializeComponent();
        
        datePicker.On<iOS>().SetUpdateMode(UpdateMode.Immediately);
    }

    #region Commands

    protected override void OnOpenPicker()
    {
        if (!SelectedDate.HasValue && DefaultDate.HasValue)
        {
            datePicker.Date = DefaultDate.Value;
        }
        else if (SelectedDate.HasValue)
        {
            datePicker.Date = SelectedDate.Value;
        }

        datePicker.DateSelected += OnDateSelected;
        datePicker.Unfocused += OnDatePickerUnfocused;
        datePicker.Focus();
    }

    #endregion

    #region Bindable Properties

    #region SelectedDate

    public static readonly BindableProperty SelectedDateProperty = BindableProperty.Create(nameof(SelectedDate),
        typeof(DateTime?),
        typeof(DatePickerControl),
        null,
        BindingMode.TwoWay,
        propertyChanged: OnSelectedDateChanged);
    
    public DateTime? SelectedDate
    {
        get => (DateTime?)GetValue(SelectedDateProperty);
        set => SetValue(SelectedDateProperty, value);
    }
    
    // private static object OnSelectedDateCoerced(BindableObject bindable, object value)
    // {
    //     var ctrl = (DatePickerControl)bindable;
    //
    //     var date = (DateTime?)value;
    //     
    //     var minDate = ctrl.MinimumDate ?? DateTime.MinValue;
    //     var maxDate = ctrl.MaximumDate ?? DateTime.MaxValue;
    //     
    // }
    
    private static void OnSelectedDateChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = (DatePickerControl)bindable;

        var date = (DateTime?)newValue;
        ctrl.datePicker.Date = date ?? DateTime.Today;
    }

    #endregion

    #region MinimumDate

    public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(nameof(MinimumDate),
        typeof(DateTime?),
        typeof(DatePickerControl),
        null,
        propertyChanged: OnMinimumDateChanged);

    public DateTime? MinimumDate
    {
        get => (DateTime?)GetValue(MinimumDateProperty);
        set => SetValue(MinimumDateProperty, value);
    }

    private static void OnMinimumDateChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = (DatePickerControl)bindable;

        var minDate = (DateTime?)newValue;
        ctrl.datePicker.MinimumDate = minDate ?? DateTime.MinValue;
    }

    #endregion

    #region MaximumDate

    public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(nameof(MaximumDate),
        typeof(DateTime?),
        typeof(DatePickerControl),
        null,
        propertyChanged: OnMaximumDateChanged);

    public DateTime? MaximumDate
    {
        get => (DateTime?)GetValue(MaximumDateProperty);
        set => SetValue(MaximumDateProperty, value);
    }

    private static void OnMaximumDateChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = (DatePickerControl)bindable;

        var maxDate = (DateTime?)newValue;
        ctrl.datePicker.MaximumDate = maxDate ?? DateTime.MaxValue;
    }

    #endregion

    #region DefaultDate

    public static readonly BindableProperty DefaultDateProperty = BindableProperty.Create(nameof(DefaultDate),
        typeof(DateTime?),
        typeof(DatePickerControl),
        DateTime.Today);

    public DateTime? DefaultDate
    {
        get => (DateTime?)GetValue(DefaultDateProperty);
        set => SetValue(DefaultDateProperty, value);
    }

    #endregion
    
    #region WithDone

    public static readonly BindableProperty WithFinishedUpdateModeProperty = BindableProperty.Create(nameof(WithFinishedUpdateMode),
        typeof(bool),
        typeof(DatePickerControl),
        false,
        propertyChanged: OnWithDoneChanged);

    public bool WithFinishedUpdateMode
    {
        get => (bool)GetValue(WithFinishedUpdateModeProperty);
        set => SetValue(WithFinishedUpdateModeProperty, value);
    }

    private static void OnWithDoneChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = (DatePickerControl)bindable;

        var withFinished = (bool)newValue;
        if (withFinished)
        {
            ctrl.datePicker.On<iOS>().SetUpdateMode(UpdateMode.WhenFinished);
        }
        else
        {
            ctrl.datePicker.On<iOS>().SetUpdateMode(UpdateMode.Immediately);
        }
    }

    #endregion

    #endregion

    #region Handlers

    private void OnDatePickerUnfocused(object sender, FocusEventArgs e)
    {
        datePicker.DateSelected -= OnDateSelected;
        datePicker.Unfocused -= OnDatePickerUnfocused;
    }

    private void OnDateSelected(object sender, DateChangedEventArgs e)
    {
        SelectedDate = e.NewDate;
        if (SelectedDate == e.NewDate)
        {
            AcceptCommand?.Execute(null);
        }
    }

    #endregion
}