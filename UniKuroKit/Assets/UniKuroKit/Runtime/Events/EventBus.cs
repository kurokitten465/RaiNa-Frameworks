using System;
using System.Collections.Generic;

namespace UniKuroKit.Events
{
    public sealed class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<EventSubscription>> _subscriptions = new();

        public EventToken Subscribe<TEvent, TContext>(Action<TEvent> callback)
            where TEvent : IEvent
            where TContext : IEventContext
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            EventSubscription subscription = new()
            {
                EventType = typeof(TEvent),
                ContextType = typeof(TContext),
                Callback = callback,
                Owner = callback.Target
            };

            AddSubscription(subscription);

            return new EventToken(() =>
            {
                RemoveSubscription(subscription);
            });
        }

        public EventToken Subscribe<TEvent, TContext>(IEventListener<TEvent> listener)
            where TEvent : IEvent
            where TContext : IEventContext
        {
            if (listener == null)
                throw new ArgumentNullException(nameof(listener));

            EventSubscription subscription = new()
            {
                EventType = typeof(TEvent),
                ContextType = typeof(TContext),
                Callback = new Action<TEvent>(listener.OnEvent),
                Owner = listener
            };

            AddSubscription(subscription);

            return new EventToken(() =>
            {
                RemoveSubscription(subscription);
            });
        }

        public void Unsubscribe(EventToken token)
        {
            token?.Dispose();
        }

        public void Publish<TEvent>(TEvent eventData, IEventContext context)
            where TEvent : IEvent
        {
            if (eventData == null)
                throw new ArgumentNullException(nameof(eventData));

            if (context == null)
                throw new ArgumentNullException(nameof(context));

            Type eventType = typeof(TEvent);

            if (!_subscriptions.TryGetValue(eventType, out List<EventSubscription> subscriptions))
                return;

            EventSubscription[] snapshot = subscriptions.ToArray();

            Type publishContextType = context.GetType();

            foreach (EventSubscription subscription in snapshot)
            {
                if (subscription.IsDisposed)
                    continue;

                if (!subscription.ContextType.IsAssignableFrom(publishContextType))
                    continue;

                ((Action<TEvent>)subscription.Callback).Invoke(eventData);
            }
        }

        private void AddSubscription(EventSubscription subscription)
        {
            if (!_subscriptions.TryGetValue(subscription.EventType, out List<EventSubscription> subscriptions))
            {
                subscriptions = new List<EventSubscription>();

                _subscriptions.Add(subscription.EventType, subscriptions);
            }

            subscriptions.Add(subscription);
        }

        private void RemoveSubscription(EventSubscription subscription)
        {
            if (subscription.IsDisposed)
                return;

            subscription.IsDisposed = true;

            if (!_subscriptions.TryGetValue(subscription.EventType, out List<EventSubscription> subscriptions))
                return;

            subscriptions.Remove(subscription);

            if (subscriptions.Count == 0)
                _subscriptions.Remove(subscription.EventType);
        }
    }
}
