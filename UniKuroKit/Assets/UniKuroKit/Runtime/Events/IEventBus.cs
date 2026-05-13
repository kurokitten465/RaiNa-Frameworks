using System;

namespace UniKuroKit.Events
{
    public interface IEventBus
    {
        EventToken Subscribe<TEvent, TContext>(Action<TEvent> callback)
            where TEvent : IEvent
            where TContext : IEventContext;

        EventToken Subscribe<TEvent, TContext>(IEventListener<TEvent> listener)
            where TEvent : IEvent
            where TContext : IEventContext;

        void Unsubscribe(EventToken token);

        void Publish<TEvent>(TEvent eventData, IEventContext context)
            where TEvent : IEvent;
    }
}
