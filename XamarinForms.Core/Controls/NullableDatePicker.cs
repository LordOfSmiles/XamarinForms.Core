using System;
using Xamarin.Forms;

namespace XamarinForms.Core.Controls
{
    public sealed class NullableDatePicker : DatePicker
    {
        private string _format = null;
        public string PlaceHolder { get; set; }


        public static readonly BindableProperty NullableDateProperty = BindableProperty.Create(nameof(NullableDate),
            typeof(DateTime?),
            typeof(NullableDatePicker));

        public DateTime? NullableDate
        {
            get => (DateTime?)GetValue(NullableDateProperty);
            set
            {
                SetValue(NullableDateProperty, value);
                UpdateDate();
            }
        }

        private void UpdateDate()
        {
            if (NullableDate.HasValue)
            {
                if (null != _format)
                    Format = _format;

                Date = NullableDate.Value;
            }
            else
            {
                _format = Format;
                Format = PlaceHolder;
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateDate();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            
            if (propertyName == "Date") 
                NullableDate = Date;
        }
    }
}