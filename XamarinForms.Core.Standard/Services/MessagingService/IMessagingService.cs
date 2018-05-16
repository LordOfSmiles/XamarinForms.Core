﻿using System;

namespace XamarinForms.Core.Standard.Services.MessagingService
{
    /// <summary>
    /// Interface to simplify messaging center
    /// Never have to worry about the sender or subscriber, everything is sent/received for you
    /// </summary>
    public interface IMessagingService
    {
        /// <summary>
        /// Subscribe the specified message and callback.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="callback">Callback.</param>
        void Subscribe(string message, Action<IMessagingService> callback);

        /// <summary>
        /// Subscribe the specified message and callback.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="callback">Callback.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        void Subscribe<TArgs>(string message, Action<IMessagingService, TArgs> callback);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">Message.</param>
        void SendMessage(string message);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        void SendMessage<TArgs>(string message, TArgs args);

        /// <summary>
        /// Unsubscribe the specified message.
        /// </summary>
        /// <param name="message">Message.</param>
        void Unsubscribe(string message);

        /// <summary>
        /// Unsubscribe the specified message and args.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        void Unsubscribe<TArgs>(string message);
    }
}

