namespace UniKuroKit.Events.Observer
{
    public interface IReadOnlyObservable<TObserver>
    {
        ObserverToken Subscribe(IObserver<TObserver> observer);
        void Unsubscribe(ObserverToken token);
    }
}
