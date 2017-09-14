using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinForms.Core.Controls
{
    public sealed class CustomToolbarItem : ToolbarItem
    {
        //NOTE: Default value is true, because toolbar items are by default visible when added to the Toolbar

        public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(CustomToolbarItem), true, BindingMode.TwoWay, null, OnIsVisibleChanged);



        public bool IsVisible

        {

            get => (bool)GetValue(IsVisibleProperty);

            set => SetValue(IsVisibleProperty, value);
        }



        public ContentPage ParentPage => Parent as ContentPage;


        private static void OnIsVisibleChanged(BindableObject bindable, object oldvalue, object newvalue)

        {

            var item = bindable as CustomToolbarItem;



            bool newVisible = (bool)newvalue;



            if (item.ParentPage == null)

                return;



            var items = item.ParentPage.ToolbarItems;



            Device.BeginInvokeOnMainThread(() =>

            {

                if (newVisible && !items.Contains(item))

                {

                    items.Add(item);

                }

                else if (!newVisible && items.Contains(item))

                {

                    items.Remove(item);

                }

            });

        }
    }
}
