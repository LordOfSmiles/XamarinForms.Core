namespace XamarinForms.Core.Controls
{
    //public class CustomBindablePicker : Picker
    //{

    //    public CustomBindablePicker()
    //    {
    //        this.SelectedIndexChanged += OnSelectedIndexChanged;
    //    }


    //    #region Bindable Porperties


    //    #region ItemsSource

    //    public static BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IList), typeof(IEnumerable), null, propertyChanged: OnItemsSourceChanged);


    //    public IList ItemsSource
    //    {
    //        get { return (IList)GetValue(ItemsSourceProperty); }
    //        set
    //        {
    //            SetValue(ItemsSourceProperty, value);
    //        }
    //    }

    //    static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
    //    {
    //        var ctrl = bindable as CustomBindablePicker;
    //        if (ctrl == null)
    //            return;

    //        ctrl.Items.Clear();

    //        var newCollection = newValue as IList;
    //        if (newCollection == null)
    //            return;

    //        var notifyCollection = newValue as INotifyCollectionChanged;

    //        if (notifyCollection != null)
    //        {
    //            notifyCollection.CollectionChanged += (sender, args) =>
    //            {
    //                if (args.Action == NotifyCollectionChangedAction.Reset)
    //                {
    //                    ctrl.Items.Clear();

    //                    var newItems = sender as IEnumerable<string>;
    //                    if (newItems != null)
    //                    {
    //                        foreach (var item in newItems)
    //                        {
    //                            ctrl.Items.Add(GetString(item));
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    if (args.NewItems != null)
    //                    {
    //                        foreach (var newItem in args.NewItems)
    //                        {
    //                            ctrl.Items.Add(GetString(newItem));
    //                        }
    //                    }

    //                    if (args.OldItems != null)
    //                    {
    //                        foreach (var oldItem in args.OldItems)
    //                        {
    //                            ctrl.Items.Remove(GetString(oldItem));
    //                        }
    //                    }
    //                }
    //            };
    //        }

    //        foreach (var item in newCollection)
    //            ctrl.Items.Add((item ?? "").ToString());
    //    }

    //    #endregion

    //    #region SelectedItem

    //    public static BindableProperty SelectedItemProperty =
    //        BindableProperty.Create("SelectedItem", typeof(object), typeof(CustomBindablePicker), null, BindingMode.TwoWay, propertyChanged: OnSelectedItemChanged);


    //    public object SelectedItem
    //    {
    //        get { return (object)GetValue(SelectedItemProperty); }
    //        set
    //        {
    //            SetValue(SelectedItemProperty, value);
    //        }
    //    }

    //    static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
    //    {
    //        var picker = bindable as CustomBindablePicker;
    //        if (picker == null)
    //            return;

    //        if (picker.ItemsSource != null)
    //            picker.SelectedIndex = picker.ItemsSource.IndexOf(picker.SelectedItem);
    //    }

    //    #endregion

    //    #endregion

    //    #region Handlers

    //    void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
    //    {
    //        if (SelectedIndex < 0 || SelectedIndex > Items.Count - 1)
    //        {
    //            SelectedItem = null;
    //        }
    //        else
    //        {
    //            SelectedItem = Items[SelectedIndex];
    //        }
    //    }

    //    #endregion

    //    #region Private Methods

    //    private static string GetString(object item)
    //    {
    //        var result = item != null
    //            ? item.ToString()
    //            : string.Empty;

    //        return result;
    //    }

    //    #endregion
    //}
}

