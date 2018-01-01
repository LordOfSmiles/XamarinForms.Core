using System;
using System.Linq;
using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Behaviors
{
    public class NumericValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = sender as Entry;
            if (entry == null)
                return;

            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                bool isValid = args.NewTextValue.ToCharArray().All(char.IsDigit);

                entry.Text = isValid
                    ? args.NewTextValue
                    : args.NewTextValue.Remove(args.NewTextValue.Length - 1);
            }
        }
    }
}
