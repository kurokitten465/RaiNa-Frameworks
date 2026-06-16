namespace RaiNa.Events.Observer
{
    internal sealed class Subscription<TObserver>
    {
        public int Id;

        public IObserver<TObserver> Observer;
    }
}
