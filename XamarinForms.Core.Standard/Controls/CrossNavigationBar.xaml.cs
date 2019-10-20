using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Controls
{
    [ContentProperty(nameof(Content))]
    public partial class CrossNavigationBar : StackLayout
    {
        public CrossNavigationBar()
        {
            InitializeComponent();
        }
        
        #region Bindable properties
        
        #region Title
        
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), 
            typeof(string), 
            typeof(CrossNavigationBar));
        
        public string Title
        {
            get => (string) GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        
        #endregion
        
        #region Content
        
        public static readonly BindableProperty ContentProperty = BindableProperty.Create(nameof(Content), typeof(View), typeof(CrossNavigationBar));
        
        public View Content
        {
            get => (View) GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }
        
        #endregion
        
        #region ExitCommand
        
        public static readonly BindableProperty ExitCommandProperty = BindableProperty.Create(nameof(ExitCommand), typeof(ICommand), typeof(CrossNavigationBar));
        
        public ICommand ExitCommand
        {
            get => (ICommand) GetValue(ExitCommandProperty);
            set => SetValue(ExitCommandProperty, value);
        }
        
        #endregion
        
        #region ExitIcon

        public static readonly BindableProperty ExitIconProperty = BindableProperty.Create(nameof(ExitIcon),
            typeof(ImageSource),
            typeof(CrossNavigationBar),
            null);

        public ImageSource ExitIcon
        {
            get => (ImageSource) GetValue(ExitIconProperty);
            set => SetValue(ExitIconProperty, value);
        }
        
        #endregion
        
        #region ExitIconAsCross
        
        public static readonly BindableProperty ExitIconAsCrossProperty = BindableProperty.Create(nameof(ExitIconAsCross), typeof(bool), typeof(CrossNavigationBar), false);
        
        public bool ExitIconAsCross
        {
            get => (bool) GetValue(ExitIconAsCrossProperty);
            set => SetValue(ExitIconAsCrossProperty, value);
        }
        
        #endregion
        
        #endregion

        
       
       
       
    }
}