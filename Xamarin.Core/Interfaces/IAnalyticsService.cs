namespace Xamarin.Core.Interfaces;

public interface IAnalyticsService
{
    void OnNavigation(string from, string to);

    void Info(string text, string prefix);

    void Info(string text, IDictionary<string, string> info, string prefix = "INFO");
}