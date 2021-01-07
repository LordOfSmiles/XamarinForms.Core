using System.Linq;
using Xamarin.Forms;

namespace XamarinForms.Core.Controls
{
	public sealed class CustomToolbarItem : ToolbarItem
	{
        public ContentPage ParentPage => Parent as ContentPage;
    
		#region IsVisible

		//NOTE: Default value is true, because toolbar items are by default visible when added to the Toolbar

		public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(CustomToolbarItem), true, BindingMode.TwoWay, null, OnIsVisibleChanged);

		public bool IsVisible
		{
			get => (bool)GetValue(IsVisibleProperty);
			set => SetValue(IsVisibleProperty, value);
		}

		private static void OnIsVisibleChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var ctrl = bindable as CustomToolbarItem;
			if (ctrl?.ParentPage == null)
				return;

			bool newVisible = (bool)newvalue;
			var items = ctrl.ParentPage.ToolbarItems;

			Device.BeginInvokeOnMainThread(() =>
			{
				if (newVisible && !items.Contains(ctrl))
				{
					if (items.Count == 0)
					{
						items.Add(ctrl);
					}
					else
					{
						if (ctrl.Priority == 0)
						{
							items.Insert(0, ctrl);
						}
						else
						{
							int maxPriority = items.Max(x => x.Priority);
							if (ctrl.Priority >= maxPriority)
							{
								items.Add(ctrl);
							}
							else
							{
								var prevPriority = items.LastOrDefault(x => x.Priority <= ctrl.Priority);
								if (prevPriority != null)
								{
									var index = items.IndexOf(prevPriority);
									if (index != -1)
									{
										items.Insert(index + 1, ctrl);
									}
									else
									{
										items.Add(ctrl);
									}
								}
								else
								{
									items.Add(ctrl);
								}
							}
						}
					}
				}
				else if (!newVisible && items.Contains(ctrl))
				{
					items.Remove(ctrl);
				}
			});
		}

		#endregion
	}
}
