using System;

namespace UniKuroKit.Events
{
    internal struct EventSubscription
    {
        public Type EventType;

        public Type ContextType;

        public Delegate Callback;

        public object Owner;

        public bool IsDisposed;
    }
}
