using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using Xamarin.Core.Standard.Models;
using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Controls
{
    public sealed class ItemsControl : StackLayout
    {
        #region Fields

        private readonly ICommand _selectedCommand;

        private readonly Dictionary<object, View> _viewCache = new Dictionary<object, View>();

        #endregion

        public ItemsControl()
        {
            Spacing = 0;

            _selectedCommand = new Command<object>(item => SelectedItem = item);
        }

        #region Bindable Properties

        #region ItemsSource 

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(ItemsControl), propertyChanged: ItemsSourceChanged);

        public IList ItemsSource
        {
            get => (IList)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as ItemsControl;
            if (ctrl == null)
                return;

            var oldNotify = oldValue as INotifyCollectionChanged;
            if (oldNotify != null)
            {
                oldNotify.CollectionChanged -= ctrl.NotifyCollection_CollectionChanged;
            }

            var newCollection = newValue as IList;
            if (newCollection == null)
                return;

            var notifyCollection = newValue as INotifyCollectionChanged;
            if (notifyCollection != null)
            {
                notifyCollection.CollectionChanged += ctrl.NotifyCollection_CollectionChanged;
            }

            ctrl.SetItems();
        }



        #endregion

        #region SelectedItem

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create("SelectedItem", typeof(object), typeof(ItemsControl), null, BindingMode.TwoWay, propertyChanged: OnSelectedItemChanged);

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as ItemsControl;
            if (ctrl == null)
                return;

            if (newValue == oldValue)
                return;

            if (ctrl.ItemsSource != null)
            {
                var items = ctrl.ItemsSource.OfType<ISelectable>();

                foreach (var item in items)
                    item.IsSelected = item == newValue;

                var handler = ctrl.SelectedItemChanged;
                handler?.Invoke(ctrl, EventArgs.Empty);
            }
        }

        #endregion

        #region ItemTemplate

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(ItemsControl));

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        #endregion

        #region ItemsOrientation

        public static readonly BindableProperty ItemsOrientationProperty = BindableProperty.Create(nameof(ItemsOrientation), typeof(StackOrientation), typeof(ItemsControl), StackOrientation.Vertical, propertyChanged: OnItemsOrientationChanged);

        public StackOrientation ItemsOrientation
        {
            get => (StackOrientation)GetValue(ItemsOrientationProperty);
            set => SetValue(ItemsOrientationProperty, value);
        }

        private static void OnItemsOrientationChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as ItemsControl;
            if (ctrl == null)
                return;

            var orientation = (StackOrientation)newvalue;

            ctrl.Orientation = orientation;
        }

        #endregion

        #endregion

        #region Events

        public event EventHandler SelectedItemChanged;

        #endregion

        #region Prviate Methods

        private void SetItems()
        {
            Children.Clear();

            if (ItemsSource == null)
                return;

            foreach (var item in ItemsSource)
                Children.Add(GetItemView(item));


            SelectedItem = ItemsSource.OfType<object>().FirstOrDefault();
        }

        private View GetItemView(object item)
        {
            DataTemplate template;
            if (ItemTemplate is DataTemplateSelector)
            {
                var selector = (DataTemplateSelector)ItemTemplate;
                template = selector.SelectTemplate(item, this);
            }
            else
            {
                template = ItemTemplate;
            }

            var content = template.CreateContent();
            var view = content as View;

            if (view != null)
            {
                view.BindingContext = item;

                view.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = _selectedCommand,
                    CommandParameter = item
                });
            }

            return view;
        }

        #endregion

        #region Handlers

        private void NotifyCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                Children.Clear();
                _viewCache.Clear();

                var newItems = sender as IEnumerable<object>;
                if (newItems != null)
                {
                    foreach (var item in newItems)
                    {
                        var view = GetItemView(item);

                        Children.Add(view);

                        if (!_viewCache.ContainsKey(item))
                            _viewCache.Add(item, view);
                        else
                            _viewCache[item] = view;
                    }
                }
            }
            else
            {
                if (e.NewItems != null)
                {
                    foreach (var newItem in e.NewItems)
                    {
                        var view = GetItemView(newItem);

                        Children.Add(view);

                        if (!_viewCache.ContainsKey(newItem))
                            _viewCache.Add(newItem, view);
                        else
                            _viewCache[newItem] = view;
                    }
                }

                if (e.OldItems != null)
                {
                    foreach (var oldItem in e.OldItems)
                    {
                        if (_viewCache.ContainsKey(oldItem))
                        {
                            Children.Remove(_viewCache[oldItem]);
                            _viewCache.Remove(oldItem);
                        }
                    }
                }
            }
        }

        #endregion
    }
}

