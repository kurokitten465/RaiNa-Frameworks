namespace UniKuroKit.Events
{
    public interface IEventListener<in TEvent> where TEvent : IEvent
    {
        void OnEvent(TEvent eventData);
    }
}
