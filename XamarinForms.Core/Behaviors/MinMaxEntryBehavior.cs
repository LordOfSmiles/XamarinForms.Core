using Xamarin.Forms;

namespace XamarinForms.Core.Behaviors;

public sealed class MinMaxEntryBehavior:BehaviorBase<Entry>
{
    protected override void OnAttachedTo(Entry bindable)
    {
        base.OnAttachedTo(bindable);

        bindable.TextChanged += OnEntryTextChanged;
    }

    protected override void OnDetachingFrom(Entry bindable)
    {
        base.OnDetachingFrom(bindable);

        bindable.TextChanged -= OnEntryTextChanged;
    }
        
    #region Bindable Properties
        
    #region Min

    public static readonly BindableProperty MinProperty = BindableProperty.Create(nameof(Min),
        typeof(double?),
        typeof(MinMaxEntryBehavior));

    public double? Min
    {
        get => (double?) GetValue(MinProperty);
        set => SetValue(MinProperty, value);
    }
        
    #endregion

    #region Max

    public static readonly BindableProperty MaxProperty = BindableProperty.Create(nameof(Max),
        typeof(double?),
        typeof(MinMaxEntryBehavior));

    public double? Max
    {
        get => (double?) GetValue(MaxProperty);
        set => SetValue(MaxProperty, value);
    }

    #endregion
        
    #endregion
        
    private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = sender as Entry;
        if (entry == null)
            return;

        entry.TextChanged -= OnEntryTextChanged;
            
        if (!string.IsNullOrWhiteSpace(e.NewTextValue))
        {
            if (double.TryParse(e.NewTextValue, out var currentValue))
            {
                var isValid = true;
                    
                if (Min.HasValue && Max.HasValue)
                {
                    isValid = Min.Value <= currentValue && currentValue <= Max.Value;
                }
                else if (Min.HasValue)
                {
                    isValid = Min.Value <= currentValue;
                }
                else if (Max.HasValue)
                {
                    isValid = currentValue <= Max.Value;
                }

                entry.Text = isValid
                    ? e.NewTextValue
                    : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
        }
            
        entry.TextChanged += OnEntryTextChanged;
    }
}