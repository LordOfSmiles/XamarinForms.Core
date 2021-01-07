using System;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinForms.Core.Behaviors
{
    public sealed class EventToCommandBehavior : BehaviorBase<View>
    {
        #region BehaviorBase

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);

            RegisterEvent(EventName);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            base.OnDetachingFrom(bindable);

            DeregisterEvent(EventName);
        }

        #endregion

        #region Fields

        private Delegate _eventHandler;

        #endregion

        #region Bindable Proeprties

        #region EventName

        public static readonly BindableProperty EventNameProperty = BindableProperty.Create(nameof(EventName), typeof(string), typeof(EventToCommandBehavior), null, propertyChanged: OnEventNameChanged);

        public string EventName
        {
            get => (string) GetValue(EventNameProperty);
            set => SetValue(EventNameProperty, value);
        }

        private static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as EventToCommandBehavior;
            if (ctrl == null)
                return;

            if (ctrl.AssociatedObject == null)
                return;

            var oldEventName = (string) oldValue;
            var newEventName = (string) newValue;

            ctrl.DeregisterEvent(oldEventName);
            ctrl.RegisterEvent(newEventName);
        }

        #endregion

        #region Command

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EventToCommandBehavior));

        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        #endregion

        #region CommandParameter

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(EventToCommandBehavior));

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        #endregion

        #region InputConverter

        public static readonly BindableProperty InputConverterProperty = BindableProperty.Create(nameof(Converter), typeof(IValueConverter), typeof(EventToCommandBehavior));

        public IValueConverter Converter
        {
            get => (IValueConverter) GetValue(InputConverterProperty);
            set => SetValue(InputConverterProperty, value);
        }

        #endregion

        #endregion

        #region Handlers

        private void OnEvent(object sender, object eventArgs)
        {
            if (Command == null)
                return;

            object resolvedParameter;

            if (CommandParameter != null)
            {
                resolvedParameter = CommandParameter;
            }
            else if (Converter != null)
            {
                resolvedParameter = Converter.Convert(eventArgs, typeof(object), null, null);
            }
            else
            {
                resolvedParameter = eventArgs;
            }

            if (Command.CanExecute(resolvedParameter))
                Command.Execute(resolvedParameter);
        }


        #endregion

        #region Private Methods

        private void RegisterEvent(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            var eventInfo = AssociatedObject.GetType().GetRuntimeEvent(name);
            if (eventInfo == null)
                throw new ArgumentException($"EventToCommandBehavior: Can't register the '{EventName}' event.");

            var methodInfo = typeof(EventToCommandBehavior).GetTypeInfo().GetDeclaredMethod("OnEvent");

            _eventHandler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);

            eventInfo.AddEventHandler(AssociatedObject, _eventHandler);
        }

        private void DeregisterEvent(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            if (_eventHandler == null)
                return;

            var eventInfo = AssociatedObject.GetType().GetRuntimeEvent(name);

            if (eventInfo == null)
                throw new ArgumentException($"EventToCommandBehavior: Can't de-register the '{EventName}' event.");

            eventInfo.RemoveEventHandler(AssociatedObject, _eventHandler);

            _eventHandler = null;
        }

        #endregion
    }
}
