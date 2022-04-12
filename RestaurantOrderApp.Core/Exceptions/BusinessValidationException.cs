using System;
using System.Runtime.Serialization;

namespace RestaurantOrderApp.Core.Exceptions
{
    [Serializable]
    public class BusinessValidationException : Exception
    {
        public BusinessValidationException()
        {
        }

        public BusinessValidationException(string message) : base(message)
        {
        }

        public BusinessValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusinessValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
