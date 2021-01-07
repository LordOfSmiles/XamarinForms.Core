using System;
using Xamarin.Forms;

namespace XamarinForms.Core.Services.MessagingService
{
    public sealed class MessagingService : IMessagingService
    {
        #region IMessagingService

        public void Subscribe(string message, Action<IMessagingService> callback) => MessagingCenter.Subscribe<MessagingService>(this, message, callback);
        public void Subscribe(string message, Action callback) => MessagingCenter.Subscribe<MessagingService>(this, message, (service) => callback.Invoke());

        public void Subscribe<T>(string message, Action<IMessagingService, T> callback) => MessagingCenter.Subscribe<MessagingService, T>(this, message, callback);
        public void Subscribe<T>(string message, Action<T> callback) => MessagingCenter.Subscribe<MessagingService, T>(this, message, (service, parameter) => callback.Invoke(parameter));

        public void SendMessage(string message) => MessagingCenter.Send(this, message);
        public void SendMessage<T>(string message, T args) => MessagingCenter.Send(this, message, args);

        public void Unsubscribe(string message) => MessagingCenter.Unsubscribe<MessagingService>(this, message);
        public void Unsubscribe<T>(string message) => MessagingCenter.Unsubscribe<MessagingService, T>(this, message);

        #endregion

        #region Singleton


        public static MessagingService Current => _instance ?? (_instance = new MessagingService());
        private static MessagingService _instance = null;

        #endregion
    }


}

