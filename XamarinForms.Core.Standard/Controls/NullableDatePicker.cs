using System;
using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Controls
{
    public sealed class NullableDatePicker : DatePicker
    {
        public NullableDatePicker()
        {
            Format = "d";
        }
        public string _originalFormat = null;

        public static readonly BindableProperty PlaceHolderProperty =
            BindableProperty.Create(nameof(PlaceHolder), typeof(string), typeof(NullableDatePicker), "                  ");

        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set
            {
                SetValue(PlaceHolderProperty, value);
            }
        }


        public static readonly BindableProperty NullableDateProperty =
        BindableProperty.Create(nameof(NullableDate), typeof(DateTime?), typeof(NullableDatePicker), null, defaultBindingMode: BindingMode.TwoWay);

        public DateTime? NullableDate
        {
            get { return (DateTime?)GetValue(NullableDateProperty); }
            set { SetValue(NullableDateProperty, value); UpdateDate(); }
        }

        private void UpdateDate()
        {
            if (NullableDate != null)
            {
                if (_originalFormat != null)
                {
                    Format = _originalFormat;
                }
            }
            else
            {
                Format = PlaceHolder;
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                _originalFormat = Format;
                UpdateDate();
            }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == DateProperty.PropertyName || (propertyName == IsFocusedProperty.PropertyName && !IsFocused && (Date.ToString("d") == DateTime.Now.ToString("d"))))
            {
                AssignValue();
            }

            if (propertyName == NullableDateProperty.PropertyName && NullableDate.HasValue)
            {
                Date = NullableDate.Value;

                if (Date.ToString(_originalFormat) == DateTime.Now.ToString(_originalFormat))
                {
                    //this code was done because when date selected is the actual date the"DateProperty" does not raise  
                    UpdateDate();
                }
            }
        }

        public void CleanDate()
        {
            NullableDate = null;
            UpdateDate();
        }
        public void AssignValue()
        {
            NullableDate = Date;
            UpdateDate();

        }

    }

    public sealed class NullableTimePicker : TimePicker
    {
        public NullableTimePicker()
        {
            Format = "t";
        }

        public string _originalFormat = null;

        public static readonly BindableProperty PlaceHolderProperty = BindableProperty.Create(nameof(PlaceHolder), typeof(string), typeof(NullableDatePicker), "                  ");

        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set
            {
                SetValue(PlaceHolderProperty, value);
            }
        }


        public static readonly BindableProperty NullableTimeProperty =
            BindableProperty.Create(nameof(NullableTime), typeof(TimeSpan?), typeof(NullableTimePicker), null, defaultBindingMode: BindingMode.TwoWay);

        public TimeSpan? NullableTime
        {
            get { return (TimeSpan?)GetValue(NullableTimeProperty); }
            set { SetValue(NullableTimeProperty, value); UpdateDate(); }
        }

        private void UpdateDate()
        {
            if (NullableTime != null)
            {
                if (_originalFormat != null)
                {
                    Format = _originalFormat;
                }
            }
            else
            {
                Format = PlaceHolder;
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                _originalFormat = Format;
                UpdateDate();
            }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TimeProperty.PropertyName || (propertyName == IsFocusedProperty.PropertyName && !IsFocused && (Time.ToString("t") == DateTime.Now.TimeOfDay.ToString("t"))))
            {
                AssignValue();
            }

            if (propertyName == NullableTimeProperty.PropertyName && NullableTime.HasValue)
            {
                Time = NullableTime.Value;

                if (Time.ToString(_originalFormat) == DateTime.Now.TimeOfDay.ToString(_originalFormat))
                {
                    //this code was done because when date selected is the actual date the"DateProperty" does not raise  
                    UpdateDate();
                }
            }
        }

        public void CleanDate()
        {
            NullableTime = null;
            UpdateDate();
        }
        public void AssignValue()
        {
            NullableTime = Time;
            UpdateDate();

        }
    }
}
