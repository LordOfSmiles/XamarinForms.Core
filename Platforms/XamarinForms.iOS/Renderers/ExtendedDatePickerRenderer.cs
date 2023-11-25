using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms.Platform.iOS;
using XamarinForms.Core.Controls;
using XamarinForms.Core.Controls.Renderers;
using XamarinForms.Core.Helpers;


namespace XamarinForms.iOS.Renderers;

public sealed class ExtendedDatePickerRenderer : DatePickerRenderer
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
        base.OnElementChanged(e);

        if (e.NewElement is ExtendedDatePicker datePickerElement)
        {
            _datePickerElement = datePickerElement;

            if (Control != null
                && _datePickerElement != null)
            {
                AddClearButton();
            }
        }
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

    private void AddClearButton()
    {
        if (!_datePickerElement.WithClear)
            return;

        var originalToolbar = this.Control.InputAccessoryView as UIToolbar;
        if (originalToolbar is { Items.Length: <= 2 })
        {
            var buttons = originalToolbar.Items.ToList();

            var clearButton = new UIBarButtonItem(UIBarButtonSystemItem.Trash,
                                                  (_, _) =>
                                                  {
                                                      var baseDatePicker = this.Element as ExtendedDatePicker;
                                                      baseDatePicker?.RaiseClearEvent();
                                                      this.Element.Date = DateTime.Now;
                                                  });

            buttons.Insert(0, clearButton);

            originalToolbar.Items = buttons.ToArray();
            originalToolbar.SetNeedsLayout();
        }
    }
}