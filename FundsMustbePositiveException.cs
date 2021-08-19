using System;
using System.Runtime.Serialization;

namespace NBUbankATMSystem
{
    [Serializable]
    internal class FundsNotEnoughException : Exception
    {
        public FundsNotEnoughException()
        {
        }

        public FundsNotEnoughException(string message) : base(message)
        {
        }

        public FundsNotEnoughException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FundsNotEnoughException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}