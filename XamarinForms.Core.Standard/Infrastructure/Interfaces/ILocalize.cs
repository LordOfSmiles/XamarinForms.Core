using System;
using System.Globalization;

namespace XamarinForms.Core.Standard.Infrastructure.Interfaces
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
        void SetLocale(CultureInfo ci);
    }
}
