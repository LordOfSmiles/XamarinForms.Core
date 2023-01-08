using System.Collections.ObjectModel;

namespace XamarinForms.Core.Controls;

public sealed class StateContainer : Grid
{
    #region Propreties

    public ObservableCollection<StateCondition> Conditions => _conditions ??= new ObservableCollection<StateCondition>();
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
            var state = Conditions
                        .Where(x => x.State != null && x.Content != null)
                        .FirstOrDefault(stateCondition => string.Compare(stateCondition.State.ToString(), newState, StringComparison.OrdinalIgnoreCase) == 0);

            if (state != null)
            {
                var existState = Children.FirstOrDefault(x => x == state.Content);
                if (existState == null)
                {
                    Children.Add(state.Content);
                }

                foreach (var view in Children.Where(x=>x!=state.Content))
                {
                    view.IsVisible = false;
                }

                state.Content.IsVisible = true;
            }
        }
        catch
        {
            //
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