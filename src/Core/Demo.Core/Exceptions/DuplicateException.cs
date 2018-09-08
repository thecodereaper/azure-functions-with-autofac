using System;

namespace Demo.Core.Exceptions
{
    public sealed class DuplicateException : Exception
    {
        public DuplicateException() { }

        public DuplicateException(string message)
            : base(message) { }

        public DuplicateException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}