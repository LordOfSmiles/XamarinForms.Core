using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace XamarinForms.Core.Models
{
	public class SmartObservableCollection<T> : ObservableCollection<T>
	{
		private bool _suppressOnCollectionChanged;

		public void AddRange (IEnumerable<T> items)
		{
			if (items == null) {
				throw new ArgumentNullException ("items");
			}

			if (items.Any ()) {
				try {
					this._suppressOnCollectionChanged = true;
					foreach (var item in items) {
						this.Add (item);
					}
				} finally {
					this._suppressOnCollectionChanged = false;
					this.OnCollectionChanged (new NotifyCollectionChangedEventArgs (NotifyCollectionChangedAction.Reset));
				}
			}
		}

		public void RemoveRange (IEnumerable<T> items)
		{
			if (items == null) {
				throw new ArgumentNullException ("items");
			}

			if (items.Any ()) {
				try {
					this._suppressOnCollectionChanged = true;
					foreach (var item in items) {
						this.Remove (item);
					}
				} finally {
					this._suppressOnCollectionChanged = false;
					this.OnCollectionChanged (new NotifyCollectionChangedEventArgs (NotifyCollectionChangedAction.Reset));
				}
			}
		}

		protected override void OnCollectionChanged (NotifyCollectionChangedEventArgs e)
		{
			if (!this._suppressOnCollectionChanged) {
				base.OnCollectionChanged (e);
			}
		}
	}
}

