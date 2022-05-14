using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Forms;

namespace XamarinForms.Core.Controls.Tabs;

public sealed class TabControl : Grid
{
    public TabControl()
    {
        ColumnSpacing = 0;
    }
        
    #region Bindable Properties
        
    #region ItemsSource

    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource),
        typeof(ObservableCollection<TabOption>),
        typeof(TabControl),
        new ObservableCollection<TabOption>(),
        propertyChanged: OnItemsSourceChanged);

    public ObservableCollection<TabOption> ItemsSource
    {
        get => (ObservableCollection<TabOption>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }
        
    private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = bindable as TabControl;
        if (ctrl == null)
            return;

        var oldItems = (ObservableCollection<TabOption>) oldValue;
        oldItems.CollectionChanged -= ctrl.NewValueINotifyCollectionChangedCollectionChanged;

        var newItems = (ObservableCollection<TabOption>) newValue;
            
        newItems.CollectionChanged -= ctrl.NewValueINotifyCollectionChangedCollectionChanged;
        newItems.CollectionChanged += ctrl.NewValueINotifyCollectionChangedCollectionChanged;
            
        ctrl.FillControl(newItems);
    }
        
    #endregion
        
    #region ItemTemplate
        
    public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate),
        typeof(DataTemplate), 
        typeof(TabControl));

    public DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }
        
    #endregion
        
    #region SelectedIndex

    public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex),
        typeof(int),
        typeof(TabControl),
        0,
        BindingMode.TwoWay,
        propertyChanged: OnSelectedIndexChanged);

    public int SelectedIndex
    {
        get => (int) GetValue(SelectedIndexProperty);
        set => SetValue(SelectedIndexProperty, value);
    }

    private static void OnSelectedIndexChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = bindable as TabControl;
        if (ctrl == null)
            return;

        var index = (int) newValue;

        if (ctrl.ItemsSource != null && ctrl.ItemsSource.Count > index)
        {
            foreach (var option in ctrl.ItemsSource.Where((x, i) => i != index))
            {
                option.IsSelected = false;
            }

            ctrl.ItemsSource[index].IsSelected = true;
        }
    }
        
    #endregion
        
    #endregion
        
    #region Handlers
        
    private void OnTabOptionTap(object sender, EventArgs e)
    {
        var view = sender as View;
        if (view == null)
            return;

        var tabOption = view.BindingContext as TabOption;
        if (tabOption == null)
            return;

        foreach (var option in ItemsSource)
        {
            option.IsSelected = false;
        }
        tabOption.IsSelected = true;
            
        var index = ItemsSource.IndexOf(tabOption);
        SelectedIndex = index;
    }

    private void NewValueINotifyCollectionChangedCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Reset)
        {
            FillControl(new TabOption[] { });
            return;
        }

        if (e.OldItems != null)
        {
            foreach (var item in e.OldItems)
            {
                var indexToRemove = Children.IndexOf(item as View);
                if (indexToRemove > -1)
                {
                    ColumnDefinitions.RemoveAt(indexToRemove);
                    Children.RemoveAt(indexToRemove);
                }

            }
        }

        if (e.NewItems != null)
        {
            foreach (var item in e.NewItems)
            {
                if (Children.All(prop => prop.BindingContext != item))
                {
                    var element = CreateNewItem((TabOption) item);
                    ColumnDefinitions.Add(new ColumnDefinition());
                    SetColumn(element, ColumnDefinitions.Count - 1);
                    Children.Add(element);
                }
            }
        }
    }

    #endregion
        
    #region Private Methods

    private void FillControl(IList<TabOption> items)
    {
        ColumnDefinitions.Clear();
        Children.Clear();

        foreach (var tabOption in items)
        {
            ColumnDefinitions.Add(new ColumnDefinition());
        }

        for (var i = 0; i < items.Count; i++)
        {
            var newItem = CreateNewItem(items[i]);

            if (newItem != null)
            {
                SetColumn(newItem, i);
                Children.Add(newItem);
            }
        }

        var itemForSelection = ItemsSource.ElementAtOrDefault(SelectedIndex);
        if (itemForSelection != null)
        {
            itemForSelection.IsSelected = true;
        }

        SelectedIndex = 0;
    }
        
    private View CreateNewItem(TabOption item)
    {
        View view = null;

        if (ItemTemplate != null)
        {
            var content = ItemTemplate.CreateContent();
                
            view = content is View
                ? content as View
                : ((ViewCell) content).View;

            view.BindingContext = item;
                
            var gesture = new TapGestureRecognizer();
            gesture.Tapped += OnTabOptionTap;
            view.GestureRecognizers.Add(gesture);
        }

        return view;
    }

    #endregion
}