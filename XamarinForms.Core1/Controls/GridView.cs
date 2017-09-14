using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinForms.Core.Controls
{
    public class GridView : Grid
    {
        public GridView()
        {
            RowSpacing = 2;
            ColumnSpacing = 2;

            for (var i = 0; i < MaxColumns; i++)
            {
                ColumnDefinitions.Add(new ColumnDefinition());
            }
        }


        #region Bindable Properties

        #region ItemsSource Property

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(GridView), null, BindingMode.TwoWay, null, OnItemsSourceChanged);

        public IList ItemsSource
        {
            get => (IList)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        private static async void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as GridView;
            if (ctrl == null)
                return;

            await ctrl.BuildTilesAsync();
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

        public DataTemplate ItemTemplate { get; set; }

        public int MaxColumns { get; set; } = 2;

        public float TileHeight { get; set; } = 100;


        #region Prviate Methods

        private async Task BuildTilesAsync()
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
            if (RowDefinitions.Any())
                RowDefinitions.Clear();

            if (ItemsSource == null)
                return;

            var numberOfRows = Math.Ceiling(ItemsSource.Count / (double)MaxColumns);

            for (var i = 0; i < numberOfRows; i++)
            {
                RowDefinitions.Add(new RowDefinition { Height = TileHeight });
            }

            var tempItems = new List<View>();


            for (var i = 0; i < ItemsSource.Count; i++)
            {
                var index = i;
                var column = i % MaxColumns;
                var row = (int)Math.Floor(i / (double)MaxColumns);

                var tile = await BuildTileAsync(ItemsSource[index]);
                tempItems.Add(tile);

                Children.Add(tile, column, row);
            }

            //for (var i = 0; i < ItemsSource.Count; i++)
            //{
            //    var column = i % MaxColumns;
            //    var row = (int)Math.Floor(i / (double)MaxColumns);

            //    Children.Add(tempItems[i], column, row);
            //}
        }

        private Task<View> BuildTileAsync(object bindingContext)
        {
            return Task.Run(() =>
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
            });
        }

        #endregion
    }


    public class FastGrid : Grid
    {
        protected override bool ShouldInvalidateOnChildAdded(View child)
        {
            return false;
        }

        protected override bool ShouldInvalidateOnChildRemoved(View child)
        {
            return false;
        }

        protected override void OnChildMeasureInvalidated()
        {
        }
    }
}


