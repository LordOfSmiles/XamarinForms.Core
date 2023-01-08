namespace XamarinForms.Core.Controls;

public sealed class WrapLayout : Layout<View>
{
    // /// <summary>
    // /// Backing Storage for the Orientation property
    // /// </summary>
    // public static readonly BindableProperty OrientationProperty = BindableProperty.Create(
    //         nameof(Orientation),
    //         typeof(StackOrientation),
    //         typeof(WrapLayout),
    //         StackOrientation.Vertical,
    //         BindingMode.TwoWay,
    //         null,
    //         (bindable, oldvalue, newvalue) => ((WrapLayout) bindable).OnSizeChanged());
    //
    // /// <summary>
    // /// Orientation (Horizontal or Vertical)
    // /// </summary>
    // public StackOrientation Orientation
    // {
    //     get => (StackOrientation) GetValue(OrientationProperty);
    //     set => SetValue(OrientationProperty, value);
    // }

    /// <summary>
    /// Backing Storage for the Spacing property
    /// </summary>
    public static readonly BindableProperty SpacingProperty = BindableProperty.Create(
        nameof(Spacing),
        typeof(double),
        typeof(WrapLayout),
        (double) 6,
        propertyChanged: (bindable, oldvalue, newvalue) => ((WrapLayout) bindable).InvalidateMeasure());

    /// <summary>
    /// Spacing added between elements (both directions)
    /// </summary>
    /// <value>The spacing.</value>
    public double Spacing
    {
        get => (double) GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }

    /// <summary>
    /// This is called when the spacing or orientation properties are changed - it forces
    /// the control to go back through a layout pass.
    /// </summary>
    // private void OnSizeChanged()
    // {
    //     ForceLayout();
    // }

    /// <summary>
    /// This method is called during the measure pass of a layout cycle to get the desired size of an element.
    /// </summary>
    /// <param name="widthConstraint">The available width for the element to use.</param>
    /// <param name="heightConstraint">The available height for the element to use.</param>
    protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
    {
        if (WidthRequest > 0)
            widthConstraint = Math.Min(widthConstraint, WidthRequest);

        if (HeightRequest > 0)
            heightConstraint = Math.Min(heightConstraint, HeightRequest);

        double internalWidth = double.IsPositiveInfinity(widthConstraint) ? double.PositiveInfinity : Math.Max(0, widthConstraint);
        double internalHeight = double.IsPositiveInfinity(heightConstraint) ? double.PositiveInfinity : Math.Max(0, heightConstraint);

        if (double.IsPositiveInfinity(widthConstraint) && double.IsPositiveInfinity(heightConstraint))
        {
            return new SizeRequest(Size.Zero, Size.Zero);
        }

        return VariableMeasureAndLayout(internalWidth, internalHeight);

        // if (WidthRequest > 0)
        //     widthConstraint = Math.Min(widthConstraint, WidthRequest);
        // if (HeightRequest > 0)
        //     heightConstraint = Math.Min(heightConstraint, HeightRequest);
        //
        // double internalWidth = double.IsPositiveInfinity(widthConstraint) ? double.PositiveInfinity : Math.Max(0, widthConstraint);
        // double internalHeight = double.IsPositiveInfinity(heightConstraint) ? double.PositiveInfinity : Math.Max(0, heightConstraint);
        //
        //
        // return Orientation == StackOrientation.Vertical
        //     ? DoVerticalMeasure(internalWidth, internalHeight)
        //     : DoHorizontalMeasure(internalWidth, internalHeight);
    }

    /// <summary>
    /// Does the vertical measure.
    /// </summary>
    /// <returns>The vertical measure.</returns>
    /// <param name="widthConstraint">Width constraint.</param>
    /// <param name="heightConstraint">Height constraint.</param>
    private SizeRequest DoVerticalMeasure(double widthConstraint, double heightConstraint)
    {
        int columnCount = 1;

        double width = 0;
        double height = 0;
        double minWidth = 0;
        double minHeight = 0;
        double heightUsed = 0;

        foreach (var item in Children)
        {
            var size = item.Measure(widthConstraint, heightConstraint);
            width = Math.Max(width, size.Request.Width);

            var newHeight = height + size.Request.Height + Spacing;

            if (newHeight > heightConstraint)
            {
                columnCount++;
                heightUsed = Math.Max(height, heightUsed);
                height = size.Request.Height;
            }
            else
                height = newHeight;

            minHeight = Math.Max(minHeight, size.Minimum.Height);
            minWidth = Math.Max(minWidth, size.Minimum.Width);
        }

        if (columnCount > 1)
        {
            height = Math.Max(height, heightUsed);
            width *= columnCount; // take max width
        }

        return new SizeRequest(new Size(width, height), new Size(minWidth, minHeight));
    }

    /// <summary>
    /// Does the horizontal measure.
    /// </summary>
    /// <returns>The horizontal measure.</returns>
    /// <param name="widthConstraint">Width constraint.</param>
    /// <param name="heightConstraint">Height constraint.</param>
    private SizeRequest DoHorizontalMeasure(double widthConstraint, double heightConstraint)
    {
        int rowCount = 1;

        double width = 0;
        double height = 0;
        double minWidth = 0;
        double minHeight = 0;
        double widthUsed = 0;

        foreach (var item in Children)
        {
            var size = item.Measure(widthConstraint, heightConstraint);
            height = Math.Max(height, size.Request.Height);

            var newWidth = width + size.Request.Width + Spacing;

            if (newWidth > widthConstraint)
            {
                rowCount++;
                widthUsed = Math.Max(width, widthUsed);
                width = size.Request.Width;
            }
            else
                width = newWidth;

            minHeight = Math.Max(minHeight, size.Minimum.Height);
            minWidth = Math.Max(minWidth, size.Minimum.Width);
        }

        if (rowCount > 1)
        {
            width = Math.Max(width, widthUsed);
            height *= rowCount; // take max height
        }

        return new SizeRequest(new Size(width, height), new Size(minWidth, minHeight));
    }

    /// <summary>
    /// Positions and sizes the children of a Layout.
    /// </summary>
    /// <param name="x">A value representing the x coordinate of the child region bounding box.</param>
    /// <param name="y">A value representing the y coordinate of the child region bounding box.</param>
    /// <param name="width">A value representing the width of the child region bounding box.</param>
    /// <param name="height">A value representing the height of the child region bounding box.</param>
    protected override void LayoutChildren(double x, double y, double width, double height)
    {
        VariableMeasureAndLayout(width, height, true, x, y);

        // if (Orientation == StackOrientation.Vertical)
        // {
        //     double colWidth = 0;
        //     double yPos = y, xPos = x;
        //
        //     foreach (var child in Children.Where(c => c.IsVisible))
        //     {
        //         var request = child.Measure(width, height);
        //
        //         double childWidth = request.Request.Width;
        //         double childHeight = request.Request.Height;
        //         colWidth = Math.Max(colWidth, childWidth);
        //
        //         if (yPos + childHeight > height)
        //         {
        //             yPos = y;
        //             xPos += colWidth + Spacing;
        //             colWidth = 0;
        //         }
        //
        //         var region = new Rectangle(xPos, yPos, childWidth, childHeight);
        //         LayoutChildIntoBoundingRegion(child, region);
        //         yPos += region.Height + Spacing;
        //     }
        // }
        // else
        // {
        //     double rowHeight = 0;
        //     double yPos = y, xPos = x;
        //
        //     foreach (var child in Children.Where(c => c.IsVisible))
        //     {
        //         var request = child.Measure(width, height);
        //
        //         double childWidth = request.Request.Width;
        //         double childHeight = request.Request.Height;
        //         rowHeight = Math.Max(rowHeight, childHeight);
        //
        //         if (xPos + childWidth > width)
        //         {
        //             xPos = x;
        //             yPos += rowHeight + Spacing;
        //             rowHeight = 0;
        //         }
        //
        //         var region = new Rectangle(xPos, yPos, childWidth, childHeight);
        //         LayoutChildIntoBoundingRegion(child, region);
        //         xPos += region.Width + Spacing;
        //     }
        // }
    }

    private SizeRequest VariableMeasureAndLayout(double widthConstraint,
                                                 double heightConstraint,
                                                 bool doLayout = false,
                                                 double x = 0,
                                                 double y = 0)
    {
        double totalWidth = 0;
        double totalHeight = 0;
        double rowHeight = 0;
        double rowWidth = 0;
        double minWidth = 0;
        double minHeight = 0;
        double xPos = x;
        double yPos = y;

        var visibleChildren = Children.Where(c => c.IsVisible).Select(c => new
        {
            child = c,
            size = c.Measure(widthConstraint, heightConstraint)
        }).ToArray();

        var nextChildren = visibleChildren.Skip(1).ToList();
        nextChildren.Add(null); //make element count same

        var zipChildren = visibleChildren.Zip(nextChildren, (c, n) => new {current = c, next = n});

        foreach (var childBlock in zipChildren)
        {
            var child = childBlock.current.child;
            var size = childBlock.current.size;
            var itemWidth = size.Request.Width;
            var itemHeight = size.Request.Height;

            rowHeight = Math.Max(rowHeight, itemHeight + Spacing);
            rowWidth += itemWidth + Spacing;

            minHeight = Math.Max(minHeight, itemHeight);
            minWidth = Math.Max(minWidth, itemWidth);

            if (doLayout)
            {
                var region = new Rectangle(xPos, yPos, itemWidth, itemHeight);
                LayoutChildIntoBoundingRegion(child, region);
            }

            if (childBlock.next == null)
            {
                totalHeight += rowHeight;
                totalWidth = Math.Max(totalWidth, rowWidth);
                break;
            }

            xPos += itemWidth + Spacing;
            var nextWitdh = childBlock.next.size.Request.Width;

            if (xPos + nextWitdh - x > widthConstraint)
            {
                xPos = x;
                yPos += rowHeight;
                totalHeight += rowHeight;
                totalWidth = Math.Max(totalWidth, rowWidth);
                rowHeight = 0;
                rowWidth = 0;
            }
        }

        totalWidth = Math.Max(totalWidth - Spacing, 0);
        totalHeight = Math.Max(totalHeight - Spacing, 0);

        return new SizeRequest(new Size(totalWidth, totalHeight), new Size(minWidth, minHeight));
    }
}

// public sealed class WrapLayout : Layout<View>
//    {
//        #region Overrides
//
//        protected override void OnChildMeasureInvalidated()
//        {
//            _layoutCache.Clear();
//
//            base.OnChildMeasureInvalidated();
//        }
//
//        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
//        {
//         var layout = NaiveLayout(widthConstraint, heightConstraint, out var lastX, out var lastY);
//
//            return new SizeRequest(new Size(lastX, lastY));
//        }
//
//        protected override void LayoutChildren(double x, double y, double width, double height)
//        {
//         var layout = NaiveLayout(width, height, out var lastX, out var lastY);
//
//            foreach (var t in layout)
//            {
//                var offset = (int)((width - t.Last().Item2.Right) / 2);
//
//                foreach (var dingus in t)
//                {
//                    var location = new Rectangle(dingus.Item2.X + x + offset, dingus.Item2.Y + y, dingus.Item2.Width, dingus.Item2.Height);
//
//                    LayoutChildIntoBoundingRegion(dingus.Item1, location);
//                }
//            }
//        }
//
//        #endregion
//
//        #region Fields
//
//        readonly Dictionary<View, SizeRequest> _layoutCache = new Dictionary<View, SizeRequest>();
//
//        #endregion
//
//        #region Concstructor
//
//        public WrapLayout()
//        {
//            //VerticalOptions = HorizontalOptions = LayoutOptions.FillAndExpand;
//        }
//
//        #endregion
//
//        #region Bindable Properties
//
//        #region Spacing
//
//        public static readonly BindableProperty SpacingProperty = BindableProperty.Create(nameof(Spacing), typeof(double), typeof(WrapLayout), 5.0, propertyChanged: OnSpacingChanged);
//
//        public double Spacing
//        {
//            get { return (double)GetValue(SpacingProperty); }
//            set { SetValue(SpacingProperty, value); }
//        }
//
//        private static void OnSpacingChanged(BindableObject bo, object oldValue, object newValue)
//        {
//            var ctrl = bo as WrapLayout;
//            if (ctrl == null)
//                return;
//
//            ctrl._layoutCache.Clear();
//        }
//
//        #endregion
//
//        #endregion
//
//        #region Private Methods
//
//        private IEnumerable<List<Tuple<View, Rectangle>>> NaiveLayout(double width, double height, out double lastX, out double lastY)
//        {
//            double startX = 0;
//            double startY = 0;
//            var right = width;
//            double nextY = 0;
//
//            lastX = 0;
//            lastY = 0;
//
//            var result = new List<List<Tuple<View, Rectangle>>>();
//
//            var currentList = new List<Tuple<View, Rectangle>>();
//
//            foreach (var child in Children)
//            {
//             if (!_layoutCache.TryGetValue(child, out var sizeRequest))
//                {
//                    _layoutCache[child] = sizeRequest = child.Measure(double.PositiveInfinity, double.PositiveInfinity);
//                }
//
//                var paddedWidth = sizeRequest.Request.Width + Spacing;
//                var paddedHeight = sizeRequest.Request.Height + Spacing;
//
//                if (startX + paddedWidth > right)
//                {
//                    startX = 0;
//                    startY += nextY;
//
//                    if (currentList.Count > 0)
//                    {
//
//                        result.Add(currentList);
//
//                        currentList = new List<Tuple<View, Rectangle>>();
//
//                    }
//                }
//
//                currentList.Add(new Tuple<View, Rectangle>(child, new Rectangle(startX, startY, sizeRequest.Request.Width, sizeRequest.Request.Height)));
//
//                lastX = Math.Max(lastX, startX + paddedWidth);
//                lastY = Math.Max(lastY, startY + paddedHeight);
//
//                nextY = Math.Max(nextY, paddedHeight);
//
//                startX += paddedWidth;
//            }
//
//            result.Add(currentList);
//
//            return result;
//        }
//
//
//        #endregion
//    }

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
// 		set => SetValue(ColumnSpacingProperty, value);
// 		get => (double)GetValue(ColumnSpacingProperty);
// 	}
//
// 	public double RowSpacing
// 	{
// 		set => SetValue(RowSpacingProperty, value);
// 		get => (double)GetValue(RowSpacingProperty);
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
// 		Size totalSize = new Size(layoutData.CellSize.Width * layoutData.Columns + ColumnSpacing * (layoutData.Columns - 1),
// 								  layoutData.CellSize.Height * layoutData.Rows + RowSpacing * (layoutData.Rows - 1));
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
// 	LayoutData GetLayoutData(double width, double height)
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

// public class WrapLayout : Layout<View>
//    {
//        /// <summary>
//        /// Backing Storage for the Orientation property
//        /// </summary>
//        public static readonly BindableProperty OrientationProperty = 
//            BindableProperty.Create(
//                nameof(Orientation), 
//                typeof(StackOrientation), 
//                typeof(WrapLayout), 
//                StackOrientation.Vertical, 
//                BindingMode.TwoWay, 
//                null,
//                propertyChanged: (bindable, oldvalue, newvalue) => ((WrapLayout)bindable).OnSizeChanged());
//
//        /// <summary>
//        /// Orientation (Horizontal or Vertical)
//        /// </summary>
//        public StackOrientation Orientation
//        {
//            get => (StackOrientation)GetValue(OrientationProperty);
//            set => SetValue(OrientationProperty, value);
//        }
//
//        /// <summary>
//        /// Backing Storage for the Spacing property
//        /// </summary>
//        public static readonly BindableProperty SpacingProperty =
//            BindableProperty.Create(
//                nameof(Spacing),
//                typeof(double),
//                typeof(WrapLayout),
//                (double)6,
//                propertyChanged: (bindable, oldvalue, newvalue) => ((WrapLayout)bindable).OnSizeChanged());
//
//        /// <summary>
//        /// Spacing added between elements (both directions)
//        /// </summary>
//        /// <value>The spacing.</value>
//        public double Spacing
//        {
//            get => (double)GetValue(SpacingProperty);
//            set => SetValue(SpacingProperty, value);
//        }
//
//        /// <summary>
//        /// This is called when the spacing or orientation properties are changed - it forces
//        /// the control to go back through a layout pass.
//        /// </summary>
//        private void OnSizeChanged()
//        {
//            ForceLayout();
//        }
//
//        /// <summary>
//        /// This method is called during the measure pass of a layout cycle to get the desired size of an element.
//        /// </summary>
//        /// <param name="widthConstraint">The available width for the element to use.</param>
//        /// <param name="heightConstraint">The available height for the element to use.</param>
//        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
//        {
//            if (WidthRequest > 0)
//                widthConstraint = Math.Min(widthConstraint, WidthRequest);
//            if (HeightRequest > 0)
//                heightConstraint = Math.Min(heightConstraint, HeightRequest);
//
//            double internalWidth = double.IsPositiveInfinity(widthConstraint) ? double.PositiveInfinity : Math.Max(0, widthConstraint);
//            double internalHeight = double.IsPositiveInfinity(heightConstraint) ? double.PositiveInfinity : Math.Max(0, heightConstraint);
//
//            return Orientation == StackOrientation.Vertical
//                ? DoVerticalMeasure(internalWidth, internalHeight)
//                    : DoHorizontalMeasure(internalWidth, internalHeight);
//        }
//
//        /// <summary>
//        /// Does the vertical measure.
//        /// </summary>
//        /// <returns>The vertical measure.</returns>
//        /// <param name="widthConstraint">Width constraint.</param>
//        /// <param name="heightConstraint">Height constraint.</param>
//        private SizeRequest DoVerticalMeasure(double widthConstraint, double heightConstraint)
//        {
//            int columnCount = 1;
//
//            double width = 0;
//            double height = 0;
//            double minWidth = 0;
//            double minHeight = 0;
//            double heightUsed = 0;
//
//            foreach (var item in Children)
//            {
//                var size = item.Measure(widthConstraint, heightConstraint);
//                width = Math.Max(width, size.Request.Width);
//
//                var newHeight = height + size.Request.Height + Spacing;
//                if (newHeight > heightConstraint)
//                {
//                    columnCount++;
//                    heightUsed = Math.Max(height, heightUsed);
//                    height = size.Request.Height;
//                }
//                else
//                    height = newHeight;
//
//                minHeight = Math.Max(minHeight, size.Minimum.Height);
//                minWidth = Math.Max(minWidth, size.Minimum.Width);
//            }
//
//            if (columnCount > 1)
//            {
//                height = Math.Max(height, heightUsed);
//                width *= columnCount;  // take max width
//            }
//
//            return new SizeRequest(new Size(width, height), new Size(minWidth, minHeight));
//        }
//
//        /// <summary>
//        /// Does the horizontal measure.
//        /// </summary>
//        /// <returns>The horizontal measure.</returns>
//        /// <param name="widthConstraint">Width constraint.</param>
//        /// <param name="heightConstraint">Height constraint.</param>
//        private SizeRequest DoHorizontalMeasure(double widthConstraint, double heightConstraint)
//        {
//            int rowCount = 1;
//
//            double width = 0;
//            double height = 0;
//            double minWidth = 0;
//            double minHeight = 0;
//            double widthUsed = 0;
//
//            foreach (var item in Children)
//            {
//                var size = item.Measure(widthConstraint, heightConstraint);
//                height = Math.Max(height, size.Request.Height);
//
//                var newWidth = width + size.Request.Width + Spacing;
//                if (newWidth > widthConstraint)
//                {
//                    rowCount++;
//                    widthUsed = Math.Max(width, widthUsed);
//                    width = size.Request.Width;
//                }
//                else
//                    width = newWidth;
//
//                minHeight = Math.Max(minHeight, size.Minimum.Height);
//                minWidth = Math.Max(minWidth, size.Minimum.Width);
//            }
//
//            if (rowCount > 1)
//            {
//                width = Math.Max(width, widthUsed);
//                height *= rowCount;  // take max height
//            }
//
//            return new SizeRequest(new Size(width, height), new Size(minWidth, minHeight));
//        }
//
//        /// <summary>
//        /// Positions and sizes the children of a Layout.
//        /// </summary>
//        /// <param name="x">A value representing the x coordinate of the child region bounding box.</param>
//        /// <param name="y">A value representing the y coordinate of the child region bounding box.</param>
//        /// <param name="width">A value representing the width of the child region bounding box.</param>
//        /// <param name="height">A value representing the height of the child region bounding box.</param>
//        protected override void LayoutChildren(double x, double y, double width, double height)
//        {
//            if (Orientation == StackOrientation.Vertical)
//            {
//                double colWidth = 0;
//                double yPos = y, xPos = x;
//
//                foreach (var child in Children.Where(c => c.IsVisible))
//                {
//                    var request = child.Measure(width, height);
//
//                    double childWidth = request.Request.Width;
//                    double childHeight = request.Request.Height;
//                    colWidth = Math.Max(colWidth, childWidth);
//
//                    if (yPos + childHeight > height)
//                    {
//                        yPos = y;
//                        xPos += colWidth + Spacing;
//                        colWidth = 0;
//                    }
//
//                    var region = new Rectangle(xPos, yPos, childWidth, childHeight);
//                    LayoutChildIntoBoundingRegion(child, region);
//                    yPos += region.Height + Spacing;
//                }
//            }
//            else
//            {
//                double rowHeight = 0;
//                double yPos = y, xPos = x;
//
//                foreach (var child in Children.Where(c => c.IsVisible))
//                {
//                    var request = child.Measure(width, height);
//
//                    double childWidth = request.Request.Width;
//                    double childHeight = request.Request.Height;
//                    rowHeight = Math.Max(rowHeight, childHeight);
//
//                    if (xPos + childWidth > width)
//                    {
//                        xPos = x;
//                        yPos += rowHeight + Spacing;
//                        rowHeight = 0;
//                    }
//
//                    var region = new Rectangle(xPos, yPos, childWidth, childHeight);
//                    LayoutChildIntoBoundingRegion(child, region);
//                    xPos += region.Width + Spacing;
//                }
//
//            }
//        }
//    }