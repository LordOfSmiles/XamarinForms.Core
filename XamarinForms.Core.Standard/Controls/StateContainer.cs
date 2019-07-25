using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Controls
{
    //[ContentProperty("Conditions")]
    public sealed class StateContainer : Grid
    {
        #region Constrcutor

        public StateContainer()
        {
            if (Conditions != null)
                Conditions.CollectionChanged += ConditionsOnCollectionChanged;
        }

        private void ConditionsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (Conditions != null)
            {
                Children.Clear();
                foreach (var stateCondition in Conditions)
                {
                    if (stateCondition.Content != null)
                        stateCondition.Content.IsVisible = false;

                    Children.Add(stateCondition.Content);
                }
            }
        }

        #endregion

        #region Propreties

        public ObservableCollection<StateCondition> Conditions => _conditions ?? (_conditions = new ObservableCollection<StateCondition>());
        private ObservableCollection<StateCondition> _conditions;

        #endregion

        #region Bindable Properties

        #region State

        public static readonly BindableProperty StateProperty = BindableProperty.Create(nameof(State), typeof(object), typeof(StateContainer), null, BindingMode.Default, null, StateChanged);

        private static void StateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as StateContainer;
            if (ctrl == null)
                return;

            if (newValue == null)
                return;

            var newState = newValue.ToString();

            ctrl.ChooseStateProperty(newState);
        }

        public object State
        {
            get => GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }

        #endregion

        #endregion

        #region Private Methods

        private void ChooseStateProperty(string newState)
        {
            if (Conditions == null || Conditions.Count == 0)
                return;

            try
            {
                foreach (var stateCondition in Conditions.Where(x => x.Content != null))
                {
                    stateCondition.Content.IsVisible = false;
                }

                var state = Conditions.Where(x => x.State != null && x.Content != null)
                                      .FirstOrDefault(stateCondition => string.Compare(stateCondition.State.ToString(), newState, StringComparison.OrdinalIgnoreCase) == 0);
                if (state != null)
                {
                    state.Content.IsVisible = true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"StateContainer ChooseStateProperty {newState} error: {e}");
            }
        }

        #endregion
    }

    [ContentProperty("Content")]
    public sealed class StateCondition : View
    {
        public object State { get; set; }
        public View Content { get; set; }
    }
}
