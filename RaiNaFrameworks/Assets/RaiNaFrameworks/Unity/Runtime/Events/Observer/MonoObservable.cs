using UnityEngine;
using RaiNa.Events.Observer;

namespace RaiNa.Unity.Events.Observer
{
    public abstract class MonoObservable<T> : MonoBehaviour
    {
        protected readonly Observable<T> _observable = new();
        public IReadOnlyObservable<T> Observable => _observable;
    }
}
