using System;
using System.Runtime.Serialization;

namespace NBUbankATMSystem
{
    [Serializable]
    internal class AccountNameMissmatchException : Exception
    {
        public AccountNameMissmatchException()
        {
        }

        public AccountNameMissmatchException(string message) : base(message)
        {
        }

        public AccountNameMissmatchException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountNameMissmatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}