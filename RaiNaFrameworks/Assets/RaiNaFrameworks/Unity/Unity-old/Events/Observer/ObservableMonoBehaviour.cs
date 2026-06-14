using UnityEngine;

namespace UniKuroKit.Events.Observer
{
    public abstract class ObservableMonoBehaviour<T> : MonoBehaviour
    {
        protected readonly Observable<T> _observable = new();

        public IReadOnlyObservable<T> Observable => _observable;
    }
}
