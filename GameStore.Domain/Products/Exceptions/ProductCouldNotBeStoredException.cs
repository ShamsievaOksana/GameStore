using System;
using System.Runtime.Serialization;

namespace GameStore.Domain.Products.Exceptions
{
    public class ProductCouldNotBeStoredException
        : Exception
    {
        public ProductCouldNotBeStoredException()
        {
        }

        protected ProductCouldNotBeStoredException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ProductCouldNotBeStoredException(string? message)
            : base(message)
        {
        }

        public ProductCouldNotBeStoredException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}