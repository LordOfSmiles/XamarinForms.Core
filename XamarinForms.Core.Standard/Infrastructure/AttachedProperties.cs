using System;
using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Infrastructure
{
    public static class AttachedProperties
    {
        public static readonly BindableProperty TagProperty = BindableProperty.CreateAttached("Tag", typeof(object), typeof(AttachedProperties), null);

        public static object GetTag(BindableObject bo)
        {
            return bo.GetValue(TagProperty);
        }

        public static void SetTag(BindableObject bo, object value)
        {
            bo.SetValue(TagProperty, value);
        }
    }
}
