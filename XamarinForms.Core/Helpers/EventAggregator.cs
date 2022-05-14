using Xamarin.Forms;

namespace XamarinForms.Core.Helpers;

public sealed class EventAggregator
{
    #region Public Methods

    public void Subscribe(string message, Action callback) => MessagingCenter.Subscribe<EventAggregator>(this, message, service => callback.Invoke());
    public void Subscribe<T>(string message, Action<T> callback) => MessagingCenter.Subscribe<EventAggregator, T>(this, message, (service, parameter) => callback.Invoke(parameter));

    public void SendMessage(string message) => MessagingCenter.Send(this, message);
    public void SendMessage<T>(string message, T args) => MessagingCenter.Send(this, message, args);

    public void Unsubscribe(string message) => MessagingCenter.Unsubscribe<EventAggregator>(this, message);
    public void Unsubscribe<T>(string message) => MessagingCenter.Unsubscribe<EventAggregator, T>(this, message);

    #endregion

    #region Singleton
        
    public static EventAggregator Current => _instance ??= new EventAggregator();
    private static EventAggregator _instance;

    #endregion
}