using System.Collections;
using Xamarin.Forms.Xaml;

namespace XamarinForms.Core.Controls.Pickers;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ContentPickerControl
{
    public ContentPickerControl()
    {
        InitializeComponent();
    }

    private void OnPickerUnfocused(object sender, FocusEventArgs e)
    {
        picker.Unfocused -= OnPickerUnfocused;
        InvokeUnfocusedEvent();
    }

    private void OnPickerFocused(object sender, FocusEventArgs e)
    {
        picker.Unfocused += OnPickerUnfocused;
        picker.Focused -= OnPickerFocused;
        
        InvokeFocusedEvent();
    }

    #region Commands

    protected override void OnOpenPicker()
    {
        picker.Focused += OnPickerFocused;
        picker.Focus();
    }

    #endregion

    #region Bindable Properties

    #region SelectedIndex

    public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex),
        typeof(int),
        typeof(ContentPickerControl),
        -1);

    public int SelectedIndex
    {
        get => (int)GetValue(SelectedIndexProperty);
        set => SetValue(SelectedIndexProperty, value);
    }

    #endregion

    #region SelectedItem

    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem),
        typeof(object),
        typeof(ContentPickerControl));

    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    #endregion

    #region ItemsSource

    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource),
        typeof(IList),
        typeof(ContentPickerControl),
        propertyChanged: OnItemsSourceChanged);

    public IList ItemsSource
    {
        get => (IList)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = (ContentPickerControl)bindable;

        ctrl.picker.ItemDisplayBinding = null;
        ctrl.picker.ItemDisplayBinding = ctrl.ItemDisplayBinding;
    }

    #endregion

    #region ItemDisplayBinding

    public static readonly BindableProperty ItemDisplayBindingProperty = BindableProperty.Create(nameof(ItemDisplayBinding),
        typeof(BindingBase),
        typeof(ContentPickerControl),
        new Binding("Text"),
        propertyChanged: OnItemDisplayBindingChanged);

    public BindingBase ItemDisplayBinding
    {
        get => (BindingBase)GetValue(ItemDisplayBindingProperty);
        set => SetValue(ItemDisplayBindingProperty, value);
    }

    private static void OnItemDisplayBindingChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = (ContentPickerControl)bindable;

        ctrl.picker.ItemDisplayBinding = (BindingBase)newValue;
    }

    #endregion

    #endregion
}

public record ContentPickerItem(string Id, string Text)
{
    public string Id { get; set; } = Id;
    public string Text { get; set; } = Text;
}