using System;
using System.Collections;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinForms.Core.Controls
{
    public sealed class GridView : Grid
    {
        #region Bindable Properties

        #region ItemsSource Property

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(GridView), null, BindingMode.TwoWay, null, OnItemsSourceChanged);

        public IList ItemsSource
        {
            get => (IList)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as GridView;
            if (ctrl == null)
                return;

            ctrl.BuildTilesAsync();
        }

        #endregion

        #region ItemCommand

        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(GridView), propertyChanged: OnItemCommandChanged);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        private static void OnItemCommandChanged(BindableObject bindable, object oldvalue, object newvalue)
        {

        }

        #endregion

        #region Command Parameter

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object), typeof(GridView));

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        #endregion

        #endregion

        public GridViewOrientation Orientation { get; set; }

        public GridViewSpacing ColumnMeasurment { get; set; }
        public GridViewSpacing RowMeasurement { get; set; }

        public int MaxColumnsOrRows { get; set; } = 2;

        public double TileHeight { get; set; } = 100;
        public double TileWidth { get; set; } = 200;

        public DataTemplate ItemTemplate { get; set; }

        #region Prviate Methods

        private void BuildTilesAsync()
        {
            if (Children.Any())
            {
                foreach (var child in Children)
                {
                    child.GestureRecognizers.Clear();
                }
                Children.Clear();
            }

            // Wipe out the previous row definitions if they're there.
            if (ColumnDefinitions.Any())
                ColumnDefinitions.Clear();
            if (RowDefinitions.Any())
                RowDefinitions.Clear();

            if (ItemsSource == null)
                return;

            var lastColumn = -1;
            var lastRow = -1;

            GridLength columnWidth;
            if (ColumnMeasurment == GridViewSpacing.Absolute)
            {
                columnWidth = new GridLength(TileWidth, GridUnitType.Absolute);
            }
            else
            {
                columnWidth = GridLength.Star;
            }

            GridLength rowHeight;
            if (RowMeasurement == GridViewSpacing.Absolute)
            {
                rowHeight = new GridLength(TileHeight, GridUnitType.Absolute);
            }
            else
            {
                rowHeight = GridLength.Star;
            }

            for (var i = 0; i < ItemsSource.Count; i++)
            {
                int column = 0;
                int row = 0;

                switch (Orientation)
                {
                    case GridViewOrientation.Horizontal:
                        column = i % MaxColumnsOrRows;
                        row = (int)Math.Floor(i / (double)MaxColumnsOrRows);

                        if (lastColumn < column && column < MaxColumnsOrRows)
                        {
                            ColumnDefinitions.Add(new ColumnDefinition() { Width = columnWidth });
                            lastColumn = column;
                        }

                        if (lastRow < row)
                        {
                            RowDefinitions.Add(new RowDefinition() { Height = rowHeight });
                            lastRow = row;
                        }

                        break;
                    case GridViewOrientation.Vertical:
                        row = i % MaxColumnsOrRows;
                        column = (int)Math.Floor(i / (double)MaxColumnsOrRows);

                        if (lastColumn < column)
                        {
                            ColumnDefinitions.Add(new ColumnDefinition() { Width = columnWidth });
                            lastColumn = column;
                        }

                        if (lastRow < row && row < MaxColumnsOrRows)
                        {
                            RowDefinitions.Add(new RowDefinition() { Height = rowHeight });
                            lastRow = row;
                        }

                        break;
                }

                var tile = BuildTile(ItemsSource[i]);

                SetRow(tile, row);
                SetColumn(tile, column);
                Children.Add(tile);
            }
        }

        private View BuildTile(object bindingContext)
        {
            var buildTile = ItemTemplate.CreateContent() as View;
            if (buildTile != null)
            {
                buildTile.BindingContext = bindingContext;

                if (Command != null)
                {
                    var tapGestureRecognizer = new TapGestureRecognizer
                    {
                        Command = Command,
                        CommandParameter = bindingContext
                    };

                    buildTile.GestureRecognizers.Add(tapGestureRecognizer);
                }
            }
            return buildTile;
        }

        #endregion

        #region Classes

        public enum GridViewOrientation
        {
            Vertical,
            Horizontal
        }

        public enum GridViewSpacing
        {
            Absolute,
            Star
        }

        #endregion
    }
}


