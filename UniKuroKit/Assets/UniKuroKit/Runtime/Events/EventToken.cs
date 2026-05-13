using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniKuroKit.Events
{
    public class EventToken : IDisposable
    {
        private readonly Action _disposeAction;

        private bool _isDisposed;

        internal EventToken(Action disposeAction)
        {
            _disposeAction = disposeAction;
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            _disposeAction?.Invoke();
        }
    }
}
