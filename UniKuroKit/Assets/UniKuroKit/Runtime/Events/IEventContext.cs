namespace UniKuroKit.Events
{
    public interface IEventContext<TEvent> where TEvent : IEvent
    {
        TEvent Event { get; }
    }
}
