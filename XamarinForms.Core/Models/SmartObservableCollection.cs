using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Xamarin.Core.Models
{
    public class SmartObservableCollection<T> : ObservableCollection<T>
    {
        private bool _suppressOnCollectionChanged;

        public void AddRange(IEnumerable<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            if (items.Any())
            {
                try
                {
                    _suppressOnCollectionChanged = true;

                    foreach (var item in items)
                    {
                        Add(item);
                    }
                }
                finally
                {
                    _suppressOnCollectionChanged = false;
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }
            }
        }

        public void RemoveRange(IEnumerable<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            if (items.Any())
            {
                try
                {
                    _suppressOnCollectionChanged = true;
                    foreach (var item in items)
                    {
                        Remove(item);
                    }
                }
                finally
                {
                    _suppressOnCollectionChanged = false;
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }
            }
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!_suppressOnCollectionChanged)
            {
                base.OnCollectionChanged(e);
            }
        }
    }
}

