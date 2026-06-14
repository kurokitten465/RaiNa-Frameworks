using System;

namespace UniKuroKit.Events
{
    public sealed class DelegateEventHandler<TEvent> : IEventHandler<TEvent> where TEvent : IEvent
    {
        private readonly Action<IEventContext<TEvent>> _action;
 
        public int Priority { get; }
 
        public DelegateEventHandler(Action<IEventContext<TEvent>> action, int priority)
        {
            _action   = action;
            Priority  = priority;
        }
 
        public void Handle(IEventContext<TEvent> context) => _action(context);
    }
}
