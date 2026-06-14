namespace UniKuroKit.Events
{
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        int Priority { get; }
 
        void Handle(IEventContext<TEvent> context);
    }
}
