using System.Linq;
using Xamarin.Forms;

namespace XamarinForms.Core.Behaviors;

public sealed class OnlyNumbersBehavior : BehaviorBase<Entry>
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

    private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
    {
        var entry = sender as Entry;
        if (entry == null)
            return;

        entry.TextChanged -= OnEntryTextChanged;

        if (!string.IsNullOrWhiteSpace(args.NewTextValue))
        {
            var isValid = args.NewTextValue.ToCharArray().All(char.IsDigit);

            entry.Text = isValid
                ? args.NewTextValue
                : args.NewTextValue.Remove(args.NewTextValue.Length - 1);
        }
            
        entry.TextChanged += OnEntryTextChanged;
    }
}