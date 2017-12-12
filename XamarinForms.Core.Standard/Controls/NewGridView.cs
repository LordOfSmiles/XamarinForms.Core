﻿using System;
using System.Collections;
using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Controls
{
    /// <summary>
    /// Class GridView.
    /// </summary>
    public class NewGridView : View
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewGridView"/> class.
        /// </summary>
        public NewGridView()
        {
            SelectionEnabled = true;
        }

        //
        // Static Fields
        //
        /// <summary>
        /// The items source property
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(NewGridView), null, BindingMode.OneWay, null, null, null, null);

        /// <summary>
        /// The item template property
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(NewGridView), null, BindingMode.OneWay, null, null, null, null);

        /// <summary>
        /// The row spacing property
        /// </summary>
        public static readonly BindableProperty RowSpacingProperty = BindableProperty.Create("RowSpacing", typeof(double), typeof(NewGridView), 0.0, BindingMode.OneWay, null, null, null, null);

        /// <summary>
        /// The column spacing property
        /// </summary>
        public static readonly BindableProperty ColumnSpacingProperty = BindableProperty.Create("ColumnSpacing", typeof(double), typeof(NewGridView), 0.0, BindingMode.OneWay, null, null, null, null);

        /// <summary>
        /// The item width property
        /// </summary>
        public static readonly BindableProperty ItemWidthProperty = BindableProperty.Create("ItemWidth", typeof(double), typeof(NewGridView), 100.0, BindingMode.OneWay, null, null, null, null);

        /// <summary>
        /// The item height property
        /// </summary>
        public static readonly BindableProperty ItemHeightProperty = BindableProperty.Create("ItemHeight", typeof(double), typeof(NewGridView), 100.0, BindingMode.OneWay, null, null, null, null);

        /// <summary>
        /// The padding property
        /// </summary>
        public static readonly BindableProperty PaddingProperty = BindableProperty.Create<NewGridView, Thickness>(view => view.Padding, new Thickness(0), BindingMode.OneWay);

        //
        // Properties
        //
        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>The items source.</value>
        public IEnumerable ItemsSource
        {
            get => (IEnumerable)base.GetValue(NewGridView.ItemsSourceProperty);
            set => base.SetValue(NewGridView.ItemsSourceProperty, value);
        }

        /// <summary>
        /// Gets or sets the item template.
        /// </summary>
        /// <value>The item template.</value>
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)base.GetValue(NewGridView.ItemTemplateProperty);
            set => base.SetValue(NewGridView.ItemTemplateProperty, value);
        }

        /// <summary>
        /// Gets or sets the row spacing.
        /// </summary>
        /// <value>The row spacing.</value>
        public double RowSpacing
        {
            get => (double)base.GetValue(NewGridView.RowSpacingProperty);
            set => base.SetValue(NewGridView.RowSpacingProperty, value);
        }

        /// <summary>
        /// Gets or sets the column spacing.
        /// </summary>
        /// <value>The column spacing.</value>
        public double ColumnSpacing
        {
            get => (double)base.GetValue(NewGridView.ColumnSpacingProperty);
            set => base.SetValue(NewGridView.ColumnSpacingProperty, value);
        }

        /// <summary>
        /// Gets or sets the width of the item.
        /// </summary>
        /// <value>The width of the item.</value>
        public double ItemWidth
        {
            get => (double)base.GetValue(NewGridView.ItemWidthProperty);
            set => base.SetValue(NewGridView.ItemWidthProperty, value);
        }

        /// <summary>
        /// Gets or sets the height of the item.
        /// </summary>
        /// <value>The height of the item.</value>
        public double ItemHeight
        {
            get => (double)base.GetValue(NewGridView.ItemHeightProperty);
            set => base.SetValue(NewGridView.ItemHeightProperty, value);
        }

        /// <summary>
        /// Gets or sets the padding.
        /// </summary>
        /// <value>The padding.</value>
        public Thickness Padding
        {
            get => (Thickness)base.GetValue(PaddingProperty);
            set => base.SetValue(PaddingProperty, value);
        }

        /// <summary>
        /// Occurs when item is selected.
        /// </summary>
        public event EventHandler<object> ItemSelected;

        /// <summary>
        /// Invokes the item selected event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="item">Item.</param>
        public void InvokeItemSelectedEvent(object sender, object item)
        {
            ItemSelected?.Invoke(sender, item);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [selection enabled].
        /// </summary>
        /// <value><c>true</c> if [selection enabled]; otherwise, <c>false</c>.</value>
        public bool SelectionEnabled
        {
            get;
            set;
        }
    }
}
