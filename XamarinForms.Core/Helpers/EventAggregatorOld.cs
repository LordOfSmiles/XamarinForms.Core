using CommunityToolkit.Mvvm.Messaging;

namespace XamarinForms.Core.Helpers;

public static class EventAggregator
{
    public static void Subscribe<TMessage>(object recipient, Action<TMessage> callback)
        where TMessage : class
    {
        var isRegistered = WeakReferenceMessenger.Default.IsRegistered<TMessage>(recipient);
        if (!isRegistered)
        {
            WeakReferenceMessenger.Default.Register<TMessage>(recipient,
                                                              (_, input) =>
                                                              {
                                                                  callback.Invoke(input);
                                                              });
        }
    }

    public static void Subscribe<TMessage>(object recipient, Action callback)
        where TMessage : class
    {
        var isRegistered = WeakReferenceMessenger.Default.IsRegistered<TMessage>(recipient);
        if (!isRegistered)
        {
            WeakReferenceMessenger.Default.Register<TMessage>(recipient,
                                                              (_, _) =>
                                                              {
                                                                  callback.Invoke();
                                                              });
        }
    }

    public static void SendMessage<TMessage>(TMessage message)
        where TMessage : class
    {
        WeakReferenceMessenger.Default.Send(message);
    }

    public static void Unsubscribe<TMessage>(object recipient)
        where TMessage : class
    {
        var isRegistered = WeakReferenceMessenger.Default.IsRegistered<TMessage>(recipient);
        if (isRegistered)
        {
            WeakReferenceMessenger.Default.Unregister<TMessage>(recipient);
        }
    }
}