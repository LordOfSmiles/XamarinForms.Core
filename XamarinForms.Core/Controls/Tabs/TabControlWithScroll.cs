using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Core.Models;

namespace XamarinForms.Core.Controls.Tabs;

public sealed class TabControlWithScroll : ScrollView
{
    #region Fields
        
    private readonly StackLayout _stkContent;
        
    #endregion
        
    public TabControlWithScroll()
    {
        Orientation = ScrollOrientation.Horizontal;
        HorizontalScrollBarVisibility = ScrollBarVisibility.Never;

        _stkContent = new StackLayout
        {
            Spacing = 0,
            Orientation = StackOrientation.Horizontal
        };
            
        Content = _stkContent;
    }
        
    #region Bindable Properties
        
    #region ItemsSource

    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource),
        typeof(ObservableCollection<TabOption>),
        typeof(TabControlWithScroll),
        new ObservableCollection<TabOption>(),
        propertyChanged: OnItemsSourceChanged);

    public ObservableCollection<TabOption> ItemsSource
    {
        get => (ObservableCollection<TabOption>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }
        
    private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = bindable as TabControlWithScroll;
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
        typeof(TabControlWithScroll));

    public DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }
        
    #endregion
        
    #region SelectedIndex

    public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex),
        typeof(int),
        typeof(TabControlWithScroll),
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
        var ctrl = bindable as TabControlWithScroll;
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
            FillControl(items: Enumerable.Empty<TabOption>());
            return;
        }
            
        if (e.OldItems != null)
        {
            foreach (var item in e.OldItems)
            {
                _stkContent.Children.Remove(item as View);
            }
        }

        if (e.NewItems != null)
        {
            foreach (var item in e.NewItems)
            {
                if (_stkContent.Children.All(prop => prop.BindingContext != item))
                {
                    var element = CreateNewItem((TabOption)item);
                    _stkContent.Children.Add(element);
                }
            }}
    }
        
    #endregion
        
    #region Private Methods

    private void FillControl(IEnumerable<TabOption> items)
    {
        _stkContent.Children.Clear();
            
        foreach (var item in items)
        {
            _stkContent.Children.Add(CreateNewItem(item));
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
    
public sealed class TabOption: NotifyObject
{
    public TabOption(string name)
    {
        Name = name;
    }
        
    public string Name { get; }
        
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            _isSelected = value;
            OnPropertyChanged();
        } 
    }
    private bool _isSelected;
}