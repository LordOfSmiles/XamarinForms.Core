using System;

namespace XamarinForms.Core.Services.MessagingService
{
    public interface IMessagingService
    {
        void Subscribe(string message, Action<IMessagingService> callback);
        void Subscribe(string message, Action callback);
        
        void Subscribe<T>(string message, Action<IMessagingService, T> callback);
        void Subscribe<T>(string message, Action<T> callback);

        void SendMessage(string message);
        void SendMessage<T>(string message, T args);

        void Unsubscribe(string message);
        void Unsubscribe<T>(string message);
    }
}

