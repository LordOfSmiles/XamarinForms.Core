using CommunityToolkit.Mvvm.Messaging;

namespace XamarinForms.Core.Helpers;

public static class EventAggregator
{
    public static void Subscribe<TMessage>(object recipient, Action<TMessage> callback)
        where TMessage : class
    {
        WeakReferenceMessenger.Default.Register<TMessage>(recipient,
                                                          (_, input) =>
                                                          {
                                                              callback.Invoke(input);
                                                          });
    }
    
    public static void Subscribe<TMessage>(object recipient, Action callback)
        where TMessage : class
    {
        WeakReferenceMessenger.Default.Register<TMessage>(recipient,
                                                          (_, _) =>
                                                          {
                                                              callback.Invoke();
                                                          });
    }

    public static void SendMessage<TMessage>(TMessage message)
        where TMessage : class
    {
        WeakReferenceMessenger.Default.Send(message);
    }

    public static void Unsubscribe<TMessage>(object recipient)
        where TMessage : class
    {
        WeakReferenceMessenger.Default.Unregister<TMessage>(recipient);
    }
}

//[Obsolete]
// public sealed class EventAggregatorOld
// {
//     #region Public Methods
//
//     public void Subscribe(string message, Action callback) => MessagingCenter.Subscribe<EventAggregatorOld>(this, message, service => callback.Invoke());
//     
//     public void SendMessage(string message) => MessagingCenter.Send(this, message);
//
//     public void SendMessage<T>(string message, T args) => MessagingCenter.Send(this, message, args);
//
//     public void Unsubscribe(string message) => MessagingCenter.Unsubscribe<EventAggregatorOld>(this, message);
//
//     public void Unsubscribe<T>(string message) => MessagingCenter.Unsubscribe<EventAggregatorOld, T>(this, message);
//
//     #endregion
//
//     #region Singleton
//
//     public static EventAggregatorOld Current => _instance ??= new EventAggregatorOld();
//     private static EventAggregatorOld _instance;
//
//     #endregion
// }