using System;

namespace RaiNa.Exceptions
{
    /// <summary>
    /// Exception thrown when singleton initialization fails.
    /// </summary>
    public class SingletonInitializeException : Exception
    {
        public SingletonInitializeException() { }
        public SingletonInitializeException(string message) : base(message) { }
        public SingletonInitializeException(string message, Exception inner) : base(message, inner) { }
    }
}
