using Xamarin.Forms;

namespace BabyDream.Infrastructure.Controls
{
    public partial class TopGradientView 
    {
        public TopGradientView()
        {
            InitializeComponent();
        }
        
        #region Bindable Proeprties
        
        #region StartColor

        public static readonly BindableProperty StartColorProperty = BindableProperty.Create(nameof(StartColor),
            typeof(Color),
            typeof(TopGradientView),
            Color.Default);

        public Color StartColor
        {
            get => (Color) GetValue(StartColorProperty);
            set => SetValue(StartColorProperty, value);
        }

        #endregion
        
        #region EndColor

        public static readonly BindableProperty EndColorProperty = BindableProperty.Create(nameof(EndColor),
            typeof(Color),
            typeof(TopGradientView),
            Color.Default);

        public Color EndColor
        {
            get => (Color) GetValue(EndColorProperty);
            set => SetValue(EndColorProperty, value);
        }

        #endregion
        
        #endregion
    }
}