using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace XamarinForms.Core.Controls
{
	public sealed class WrapLayout : Layout<View>
    {
        #region Overrides

        protected override void OnChildMeasureInvalidated()
        {
            _layoutCache.Clear();

            base.OnChildMeasureInvalidated();
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
	        var layout = NaiveLayout(widthConstraint, heightConstraint, out var lastX, out var lastY);

            return new SizeRequest(new Size(lastX, lastY));
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
	        var layout = NaiveLayout(width, height, out var lastX, out var lastY);

            foreach (var t in layout)
            {
                var offset = (int)((width - t.Last().Item2.Right) / 2);

                foreach (var dingus in t)
                {
                    var location = new Rectangle(dingus.Item2.X + x + offset, dingus.Item2.Y + y, dingus.Item2.Width, dingus.Item2.Height);

                    LayoutChildIntoBoundingRegion(dingus.Item1, location);
                }
            }
        }

        #endregion

        #region Fields

        readonly Dictionary<View, SizeRequest> _layoutCache = new Dictionary<View, SizeRequest>();

        #endregion

        #region Concstructor

        public WrapLayout()
        {
            //VerticalOptions = HorizontalOptions = LayoutOptions.FillAndExpand;
        }

        #endregion

        #region Bindable Properties

        #region Spacing

        public static readonly BindableProperty SpacingProperty = BindableProperty.Create(nameof(Spacing), typeof(double), typeof(WrapLayout), 5.0, propertyChanged: OnSpacingChanged);

        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        private static void OnSpacingChanged(BindableObject bo, object oldValue, object newValue)
        {
            var ctrl = bo as WrapLayout;
            if (ctrl == null)
                return;

            ctrl._layoutCache.Clear();
        }

        #endregion

        #endregion

        #region Private Methods

        private IEnumerable<List<Tuple<View, Rectangle>>> NaiveLayout(double width, double height, out double lastX, out double lastY)
        {
            double startX = 0;
            double startY = 0;
            var right = width;
            double nextY = 0;

            lastX = 0;
            lastY = 0;

            var result = new List<List<Tuple<View, Rectangle>>>();

            var currentList = new List<Tuple<View, Rectangle>>();

            foreach (var child in Children)
            {
	            if (!_layoutCache.TryGetValue(child, out var sizeRequest))
                {
                    _layoutCache[child] = sizeRequest = child.Measure(double.PositiveInfinity, double.PositiveInfinity);
                }

                var paddedWidth = sizeRequest.Request.Width + Spacing;
                var paddedHeight = sizeRequest.Request.Height + Spacing;

                if (startX + paddedWidth > right)
                {
                    startX = 0;
                    startY += nextY;

                    if (currentList.Count > 0)
                    {

                        result.Add(currentList);

                        currentList = new List<Tuple<View, Rectangle>>();

                    }
                }

                currentList.Add(new Tuple<View, Rectangle>(child, new Rectangle(startX, startY, sizeRequest.Request.Width, sizeRequest.Request.Height)));

                lastX = Math.Max(lastX, startX + paddedWidth);
                lastY = Math.Max(lastY, startY + paddedHeight);

                nextY = Math.Max(nextY, paddedHeight);

                startX += paddedWidth;
            }

            result.Add(currentList);

            return result;
        }


        #endregion
    }
	
 //    public sealed class WrapLayout : Layout<View>
	// {
	// 	private readonly Dictionary<Size, LayoutData> _layoutDataCache = new Dictionary<Size, LayoutData>();
 //
	// 	public static readonly BindableProperty ColumnSpacingProperty = BindableProperty.Create(
	// 		"ColumnSpacing",
	// 		typeof(double),
	// 		typeof(WrapLayout),
	// 		5.0,
	// 		propertyChanged: (bindable, oldvalue, newvalue) =>
	// 		{
	// 			((WrapLayout)bindable).InvalidateLayout();
	// 		});
 //
	// 	public static readonly BindableProperty RowSpacingProperty = BindableProperty.Create(
	// 		"RowSpacing",
	// 		typeof(double),
	// 		typeof(WrapLayout),
	// 		5.0,
	// 		propertyChanged: (bindable, oldvalue, newvalue) =>
	// 		{
	// 			((WrapLayout)bindable).InvalidateLayout();
	// 		});
 //
	// 	public double ColumnSpacing
	// 	{
	// 		set { SetValue(ColumnSpacingProperty, value); }
	// 		get { return (double)GetValue(ColumnSpacingProperty); }
	// 	}
 //
	// 	public double RowSpacing
	// 	{
	// 		set { SetValue(RowSpacingProperty, value); }
	// 		get { return (double)GetValue(RowSpacingProperty); }
	// 	}
 //
	// 	protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
	// 	{
	// 		LayoutData layoutData = GetLayoutData(widthConstraint, heightConstraint);
	// 		if (layoutData.VisibleChildCount == 0)
	// 		{
	// 			return new SizeRequest();
	// 		}
 //
	// 		var width = layoutData.CellSize.Width * layoutData.Columns + ColumnSpacing * (layoutData.Columns - 1);
	// 		var height = layoutData.CellSize.Height * layoutData.Rows + RowSpacing * (layoutData.Rows - 1);
 //
	// 		Size totalSize = new Size(width,height);
	// 		
	// 		return new SizeRequest(totalSize);
	// 	}
 //
	// 	protected override void LayoutChildren(double x, double y, double width, double height)
	// 	{
	// 		LayoutData layoutData = GetLayoutData(width, height);
 //
	// 		if (layoutData.VisibleChildCount == 0)
	// 		{
	// 			return;
	// 		}
 //
	// 		double xChild = x;
	// 		double yChild = y;
	// 		int row = 0;
	// 		int column = 0;
 //
	// 		foreach (View child in Children)
	// 		{
	// 			if (!child.IsVisible)
	// 			{
	// 				continue;
	// 			}
 //
	// 			LayoutChildIntoBoundingRegion(child, new Rectangle(new Point(xChild, yChild), layoutData.CellSize));
 //
	// 			if (++column == layoutData.Columns)
	// 			{
	// 				column = 0;
	// 				row++;
	// 				xChild = x;
	// 				yChild += RowSpacing + layoutData.CellSize.Height;
	// 			}
	// 			else
	// 			{
	// 				xChild += ColumnSpacing + layoutData.CellSize.Width;
	// 			}
	// 		}
	// 	}
 //
	// 	private LayoutData GetLayoutData(double width, double height)
	// 	{
	// 		Size size = new Size(width, height);
 //
	// 		// Check if cached information is available.
	// 		if (_layoutDataCache.ContainsKey(size))
	// 		{
	// 			return _layoutDataCache[size];
	// 		}
 //
	// 		int visibleChildCount = 0;
	// 		Size maxChildSize = new Size();
	// 		int rows = 0;
	// 		int columns = 0;
	// 		LayoutData layoutData = new LayoutData();
 //
	// 		// Enumerate through all the children.
	// 		foreach (View child in Children)
	// 		{
	// 			// Skip invisible children.
	// 			if (!child.IsVisible)
	// 				continue;
 //
	// 			// Count the visible children.
	// 			visibleChildCount++;
 //
	// 			// Get the child's requested size.
	// 			SizeRequest childSizeRequest = child.Measure(Double.PositiveInfinity, Double.PositiveInfinity);
 //
	// 			// Accumulate the maximum child size.
	// 			maxChildSize.Width = Math.Max(maxChildSize.Width, childSizeRequest.Request.Width);
	// 			maxChildSize.Height = Math.Max(maxChildSize.Height, childSizeRequest.Request.Height);
	// 		}
 //
	// 		if (visibleChildCount != 0)
	// 		{
	// 			// Calculate the number of rows and columns.
	// 			if (Double.IsPositiveInfinity(width))
	// 			{
	// 				columns = visibleChildCount;
	// 				rows = 1;
	// 			}
	// 			else
	// 			{
	// 				columns = (int)((width + ColumnSpacing) / (maxChildSize.Width + ColumnSpacing));
	// 				columns = Math.Max(1, columns);
	// 				rows = (visibleChildCount + columns - 1) / columns;
	// 			}
 //
	// 			// Now maximize the cell size based on the layout size.
	// 			Size cellSize = new Size();
 //
	// 			if (Double.IsPositiveInfinity(width))
	// 			{
	// 				cellSize.Width = maxChildSize.Width;
	// 			}
	// 			else
	// 			{
	// 				cellSize.Width = (width - ColumnSpacing * (columns - 1)) / columns;
	// 			}
 //
	// 			if (Double.IsPositiveInfinity(height))
	// 			{
	// 				cellSize.Height = maxChildSize.Height;
	// 			}
	// 			else
	// 			{
	// 				cellSize.Height = (height - RowSpacing * (rows - 1)) / rows;
	// 			}
 //
	// 			layoutData = new LayoutData(visibleChildCount, cellSize, rows, columns);
	// 		}
 //
	// 		_layoutDataCache.Add(size, layoutData);
	// 		return layoutData;
	// 	}
 //
	// 	protected override void InvalidateLayout()
	// 	{
	// 		base.InvalidateLayout();
 //
	// 		// Discard all layout information for children added or removed.
	// 		_layoutDataCache.Clear();
	// 	}
 //
	// 	protected override void OnChildMeasureInvalidated()
	// 	{
	// 		base.OnChildMeasureInvalidated();
 //
	// 		// Discard all layout information for child size changed.
	// 		_layoutDataCache.Clear();
	// 	}
	// }
 //
 //    internal struct LayoutData
 //    {
	//     public int VisibleChildCount { get; private set; }
 //
	//     public Size CellSize { get; private set; }
 //
	//     public int Rows { get; private set; }
 //
	//     public int Columns { get; private set; }
 //
	//     public LayoutData(int visibleChildCount, Size cellSize, int rows, int columns)
	//     {
	// 	    VisibleChildCount = visibleChildCount;
	// 	    CellSize = cellSize;
	// 	    Rows = rows;
	// 	    Columns = columns;
	//     }
 //    }
}