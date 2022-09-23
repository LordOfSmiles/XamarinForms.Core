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

        datePicker.On<iOS>().SetUpdateMode(UpdateMode.WhenFinished);
    }

    #region Commands

    protected override void OnOpenPicker()
    {
        DateTime initialDate;

        if (!SelectedDate.HasValue && DefaultDate.HasValue)
        {
            initialDate = DefaultDate.Value;
        }
        else if (SelectedDate.HasValue)
        {
            initialDate = SelectedDate.Value;
        }
        else
        {
            initialDate = DateTime.Today;
        }

        //костыль
        if (Device.RuntimePlatform == Device.iOS && datePicker.Date == initialDate)
        {
            datePicker.Date = GetFakeDate(initialDate);
        }

        datePicker.Date = initialDate;

        datePicker.DoneEvent += OnDone;

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

    #endregion

    #region Handlers

    private void OnDateSelected(object sender, DateChangedEventArgs e)
    {
        SelectedDate = e.NewDate;

        if (SelectedDate == e.NewDate)
        {
            AcceptCommand?.Execute(null);
        }
    }
    
    private void OnDone(object sender, EventArgs e)
    {
        if (SelectedDate != datePicker.Date)
        {
            SelectedDate = datePicker.Date;
            AcceptCommand?.Execute(null);
        }

        datePicker.DoneEvent -= OnDone;
    }


    #endregion

    #region Private Methods

    private static DateTime GetFakeDate(DateTime date)
    {
        if (date > DateTime.MinValue)
        {
            return date.AddDays(-1);
        }
        else if (date < DateTime.MaxValue)
        {
            return date.AddDays(+1);
        }
        else
        {
            return date;
        }
    }

    #endregion
}