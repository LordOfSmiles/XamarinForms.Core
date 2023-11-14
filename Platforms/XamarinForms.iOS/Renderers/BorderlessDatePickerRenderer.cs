using System;
using System.ComponentModel;
using Xamarin.Forms.Platform.iOS;
using XamarinForms.Core.Controls;
using XamarinForms.Core.Controls.Renderers;
using XamarinForms.Core.Helpers;


namespace XamarinForms.iOS.Renderers;

public sealed class BorderlessDatePickerRenderer : DatePickerRenderer
{
    protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.OnElementPropertyChanged(sender, e);

        if (Control != null)
        {
            Control.Layer.BorderWidth = (nfloat)0;
            Control.BorderStyle = UITextBorderStyle.None;

            var picker = (UIDatePicker)Control.InputView;
            if (VersionHelper.IsEqualOrGreater(13, 4)
                && picker != null)
            {
                picker.PreferredDatePickerStyle = PreferredStyle ?? UIDatePickerStyle.Wheels;
            }

            var toolbar = Control.InputAccessoryView as UIToolbar;
            if (toolbar?.Items != null)
            {
                foreach (var button in toolbar.Items)
                {
                    if (button.Style == UIBarButtonItemStyle.Done)
                    {
                        button.Clicked -= OnDoneButtonClicked;
                        button.Clicked += OnDoneButtonClicked;
                    }
                }
            }
        }
    }

    protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
    {
        if (e.NewElement is ExtendedDatePicker datePickerElement)
        {
            _datePickerElement = datePickerElement;
        }

        base.OnElementChanged(e);
    }

    #region Fields

    private ExtendedDatePicker _datePickerElement;

    #endregion

    #region Properties

    public static UIDatePickerStyle? PreferredStyle { get; set; }

    #endregion

    private void OnDoneButtonClicked(object sender, EventArgs e)
    {
        _datePickerElement?.RaiseDoneEvent();
    }
}