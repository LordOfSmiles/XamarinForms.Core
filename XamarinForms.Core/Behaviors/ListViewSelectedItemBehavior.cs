using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinForms.Core.Behaviors
{
    public sealed class ListViewSelectedItemBehavior : BehaviorBase<ListView>
    {
        #region Command

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ListViewSelectedItemBehavior), null);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        #endregion

        #region Converter

        public static readonly BindableProperty InputConverterProperty = BindableProperty.Create(nameof(Converter), typeof(IValueConverter), typeof(ListViewSelectedItemBehavior), null);

        public IValueConverter Converter
        {
            get => (IValueConverter)GetValue(InputConverterProperty);
            set => SetValue(InputConverterProperty, value);
        }

        #endregion

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.ItemSelected += OnListViewItemSelected;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.ItemSelected -= OnListViewItemSelected;
        }

        private void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            if (Command != null)
            {
                object parameter = null;

                if (Converter != null)
                {
                    parameter = Converter.Convert(e, typeof(object), null, null);
                }
                else
                {
                    parameter = e.SelectedItem;
                }

                if (Command.CanExecute(parameter))
                    Command.Execute(parameter);
            }

            var lst = sender as ListView;
            if (lst != null)
                lst.SelectedItem = null;
        }
    }
}
