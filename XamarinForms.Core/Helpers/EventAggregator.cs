
namespace XamarinForms.Core.Helpers;

// public sealed class EventAggregator2
// {
//     public void Register<T>(ValueChangedMessage<T> message, Action<T> callback)
//     {
//         WeakReferenceMessenger.Default.Register<ValueChangedMessage<T>>(this,
//                                                                         (_, input) =>
//                                                                         {
//                                                                             callback.Invoke(input.Value);
//                                                                         });
//     }
//
//     public void Unregister<T>(ValueChangedMessage<T> message)
//     {
//         WeakReferenceMessenger.Default.Unregister<ValueChangedMessage<T>>(this);
//     }
//
//     public void Send<T>(ValueChangedMessage<T> message)
//     {
//         WeakReferenceMessenger.Default.Send(message);
//     }
// }

public sealed class EventAggregator
{
    #region Public Methods

    public void Subscribe(string message, Action callback) => MessagingCenter.Subscribe<EventAggregator>(this, message, service => callback.Invoke());
    
    public void Subscribe<T>(string message, Action<T> callback) => MessagingCenter.Subscribe<EventAggregator, T>(this, message, (service, parameter) => callback.Invoke(parameter));
    
    public void SendMessage(string message) => MessagingCenter.Send(this, message);

    //public void Send(string message) => WeakReferenceMessenger.Default.Send(new ValueChangedMessage<string>(message));

    public void SendMessage<T>(string message, T args) => MessagingCenter.Send(this, message, args);

    //public void Send<T>(T args) => WeakReferenceMessenger.Default.Send(new ValueChangedMessage<T>(args));

    public void Unsubscribe(string message) => MessagingCenter.Unsubscribe<EventAggregator>(this, message);

    public void Unsubscribe<T>(string message) => MessagingCenter.Unsubscribe<EventAggregator, T>(this, message);

    #endregion

    #region Singleton

    public static EventAggregator Current => _instance ??= new EventAggregator();
    private static EventAggregator _instance;

    #endregion
}