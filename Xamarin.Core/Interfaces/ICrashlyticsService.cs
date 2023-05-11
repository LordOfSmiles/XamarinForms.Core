namespace Xamarin.Core.Interfaces;

public interface ICrashlyticsService
{
    void Error(Exception ex);

    void Error(Exception ex, IDictionary<string, string> info);
}