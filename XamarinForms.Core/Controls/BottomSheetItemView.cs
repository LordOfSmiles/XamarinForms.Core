using Xamarin.Core.Models;
using Xamarin.Forms;
using XamarinForms.Core.Extensions;
using XamarinForms.Core.Standard.Converters;
using XamarinForms.Core.Standard.Infrastructure;
using XamarinForms.Core.Standard.Services;

namespace XamarinForms.Core.Controls
{
    public sealed class BottomSheetItemView:StackLayout
    {
        public BottomSheetItemView()
        {
            Orientation = StackOrientation.Horizontal;
            Spacing = 16;
            Margin = new Thickness(8);

            var img = new Image()
                .CenterV()
                .Width(DeviceHelper.OnPlatform(25, 24))
                .Height(DeviceHelper.OnPlatform(25, 24));
            img.SetBinding(IsVisibleProperty, new Binding(nameof(BottomSheetItem.Icon), BindingMode.OneTime, ToVisibilityConverter.Current));
            img.SetBinding(Image.SourceProperty, new Binding(nameof(BottomSheetItem.Icon), BindingMode.OneTime));
            Children.Add(img);

            var lbl = new Label()
                .FillExpandH()
                .CenterV()
                .FontSize(18);
            lbl.TextColor = Color.Black;
            lbl.SetBinding(Label.TextProperty, new Binding(nameof(BottomSheetItem.Title), BindingMode.OneTime));
            Children.Add(lbl);

            this.BindTapGesture(nameof(BottomSheetItem.Command));
        }
    }
}