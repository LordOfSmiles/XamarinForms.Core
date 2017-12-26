using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace XamarinForms.Core.iOS.Renderers
{
    public sealed class LabeledTextField : UITextField
    {
        private UIView _lineView;
        private UILabel _titleLabel;
        private string _errorMessage;
        private nfloat _lineHeight = 0.5f;
        private nfloat _selectedLineHeight = 1.0f;

        private UIColor _titleColor = UIColor.Gray;
        private UIColor _lineColor = UIColor.LightGray;
        private UIColor _errorColor = UIColor.Red;
        private UIColor _selectedTitleColor = UIColor.Blue;
        private UIColor _selectedLineColor = UIColor.Black;

        /// <summary>
        /// Should this field be read only.
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// The color of the <see cref="P:UIKit.UITextField.Text"/>.
        /// </summary>        
        public override UIColor TextColor
        {
            get { return base.TextColor; }
            set
            {
                base.TextColor = value;
                UpdateControl();
            }
        }

        /// <summary>
        /// A UIColor value that determines the text color of the title label when in the normal state
        /// </summary>
        public UIColor TitleColor
        {
            get { return _titleColor; }
            set
            {
                _titleColor = value;
                UpdateTitleColor();
            }
        }

        /// <summary>
        /// A UIColor value that determines the color of the bottom line when in the normal state
        /// </summary>
        public UIColor LineColor
        {
            get { return _lineColor; }
            set
            {
                _lineColor = value;
                UpdateLineView();
            }
        }

        /// <summary>
        /// A UIColor value that determines the color used for the title label and the line when 
        /// the error message is not null
        /// </summary>
        public UIColor ErrorColor
        {
            get { return _errorColor; }
            set
            {
                _errorColor = value;
                UpdateColors();
            }
        }

        /// <summary>
        /// A UIColor value that determines the text color of the title label when editing
        /// </summary>
        public UIColor SelectedTitleColor
        {
            get { return _selectedTitleColor; }
            set
            {
                _selectedTitleColor = value;
                UpdateTitleColor();
            }
        }

        /// <summary>
        /// A UIColor value that determines the color of the line in a selected state
        /// </summary>
        public UIColor SelectedLineColor
        {
            get { return _selectedLineColor; }
            set
            {
                _selectedLineColor = value;
                UpdateLineView();
            }
        }

        /// <summary>
        /// A CGFloat value that determines the height for the bottom line when the control is 
        /// in the normal state
        /// </summary>
        public nfloat LineHeight
        {
            get { return _lineHeight; }
            set
            {
                _lineHeight = value;
                UpdateLineView();
                SetNeedsDisplay();
            }
        }

        /// <summary>
        /// A CGFloat value that determines the height for the bottom line when the control is 
        /// in a selected state
        /// </summary>
        public nfloat SelectedLineHeight
        {
            get { return _selectedLineHeight; }
            set
            {
                _selectedLineHeight = value;
                UpdateLineView();
                SetNeedsDisplay();
            }
        }

        /// <summary>
        /// Whether this UIControl is highlighted.
        /// </summary>
        public override bool Highlighted
        {
            get { return base.Highlighted; }
            set
            {
                base.Highlighted = value;
                UpdateTitleColor();
                UpdateLineView();
            }
        }

        /// <summary>
        /// The text content of the textfield
        /// </summary>   
        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                UpdateControl();
            }
        }

        /// <summary>
        /// Sets the contents of the placeholder as an attributed string.
        /// </summary>  
        public override NSAttributedString AttributedPlaceholder
        {
            get { return base.AttributedPlaceholder; }
            set
            {
                base.AttributedPlaceholder = value;
                UpdateTitleLabel(true);
            }
        }

        /// <summary>
        /// A Boolean value that determines whether the receiver has an error message.
        /// </summary>
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        /// <summary>
        /// A String value for the error message to display.
        /// </summary>
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                UpdateControl();
            }
        }

        /// <param name="frame">Frame used by the view, expressed in iOS points.</param>
        /// <summary>
        /// Initializes the UITextField with the specified frame.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This constructor is used to programmatically create a new instance of UITextField with
        ///  the specified dimension in the frame.   The object will only be displayed once it has 
        /// been added to a view hierarchy by calling AddSubview in a containing view.
        /// </para>
        /// <para>
        /// This constructor is not invoked when deserializing objects from storyboards or XIB 
        /// filesinstead the constructor that takes an NSCoder parameter is invoked.
        /// </para>
        /// </remarks>
        public LabeledTextField(CGRect frame) : base(frame)
        {
            CreateTitleLabel();
            CreateLineView();

            EditingChanged += OnEditingChanged;
            ShouldBeginEditing += f =>
            {
                return !ReadOnly;
            };

            UpdateLineColor();
        }

        private void OnEditingChanged(object sender, EventArgs e)
        {
            UpdateControl(true);
            UpdateTitleLabel(true);
        }

        private void CreateTitleLabel()
        {
            _titleLabel = new UILabel();
            _titleLabel.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
            _titleLabel.Font = UIFont.SystemFontOfSize(13);
            AddSubview(_titleLabel);
        }

        private void CreateLineView()
        {
            _lineView = new UIView();
            _lineView.UserInteractionEnabled = false;
            _lineView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
            ConfigureDefaultLineHeight();
            AddSubview(_lineView);
        }

        private void ConfigureDefaultLineHeight()
        {
            var onePixel = 1.0f / UIScreen.MainScreen.Scale;
            _lineHeight = 2.0f * onePixel;
            _selectedLineHeight = 2.0f * _lineHeight;
        }

        public override CGSize IntrinsicContentSize =>
            new CGSize(Bounds.Size.Width, GetTitleHeight() + GetTextHeight());


        public override bool BecomeFirstResponder()
        {
            var result = base.BecomeFirstResponder();
            UpdateControl();
            return result;
        }

        public override bool ResignFirstResponder()
        {
            var result = base.ResignFirstResponder();
            UpdateControl();
            return result;
        }

        private void UpdateControl(bool animated = false)
        {
            UpdateColors();
            UpdateLineView();
            UpdateTitleLabel(animated);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            _titleLabel.Frame = GetTitleLabelRectForBounds(Bounds);
            _lineView.Frame = GetLineViewRectForBounds(Bounds, IsEditing);
        }

        private CGRect GetTitleLabelRectForBounds(CGRect bounds)
        {
            var titleHeight = GetTitleHeight();
            return new CGRect(0, 0, bounds.Size.Width, titleHeight);
        }

        public CGRect GetLineViewRectForBounds(CGRect bounds, bool editing)
        {
            var height = editing ? SelectedLineHeight : LineHeight;
            return new CGRect(0, bounds.Size.Height - height, bounds.Size.Width, height);
        }

        public nfloat GetTitleHeight()
        {
            if (_titleLabel != null)
            {
                return _titleLabel.Font.LineHeight + 7.0f;
            }

            return 15.0f;
        }

        public nfloat GetTextHeight()
        {
            return Font.LineHeight + 7.0f;
        }

        public override CGRect TextRect(CGRect bounds)
        {
            base.TextRect(bounds);

            var titleHeight = GetTitleHeight();
            var height = SelectedLineHeight;
            return new CGRect(0, titleHeight, bounds.Size.Width, bounds.Size.Height - titleHeight - height);
        }

        public override CGRect EditingRect(CGRect bounds)
        {
            var titleHeight = GetTitleHeight();
            var height = SelectedLineHeight;
            return new CGRect(0, titleHeight, bounds.Size.Width, bounds.Size.Height - titleHeight - height);
        }

        public override CGRect PlaceholderRect(CGRect bounds)
        {
            return CGRect.Empty;
        }

        private void UpdateLineView()
        {
            if (_lineView != null)
                _lineView.Frame = GetLineViewRectForBounds(Bounds, IsEditing);

            UpdateLineColor();
        }

        private void UpdateColors()
        {
            UpdateLineColor();
            UpdateTitleColor();
        }

        private void UpdateLineColor()
        {
            if (HasErrorMessage)
            {
                _lineView.BackgroundColor = ErrorColor;
            }
            else
            {
                _lineView.BackgroundColor = IsEditing ? SelectedLineColor : LineColor;
            }
        }

        private void UpdateTitleColor()
        {
            if (HasErrorMessage)
            {
                _titleLabel.TextColor = ErrorColor;
            }
            else
            {
                if (IsEditing || Highlighted)
                {
                    _titleLabel.TextColor = SelectedTitleColor;
                }
                else
                {
                    _titleLabel.TextColor = TitleColor;
                }
            }
        }

        private void UpdateTitleLabel(bool animate)
        {
            if (HasErrorMessage)
            {
                _titleLabel.Text = ErrorMessage;
            }
            else
            {
                _titleLabel.Text = Placeholder;
            }
        }
    }
}