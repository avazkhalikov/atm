using System;
using System.Runtime.Serialization;

namespace NBUbankATMSystem
{
    [Serializable]
    internal class InvalidAccountNumberException : Exception
    {
        public InvalidAccountNumberException()
        {
        }

        public InvalidAccountNumberException(string message) : base(message)
        {
        }

        public InvalidAccountNumberException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidAccountNumberException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}