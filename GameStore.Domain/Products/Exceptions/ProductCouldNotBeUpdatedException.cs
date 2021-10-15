using System;
using System.Runtime.Serialization;

namespace GameStore.Domain.Products.Exceptions
{
    public class ProductCouldNotBeUpdatedException
        : Exception
    {
        public ProductCouldNotBeUpdatedException()
        {
        }

        protected ProductCouldNotBeUpdatedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ProductCouldNotBeUpdatedException(string? message)
            : base(message)
        {
        }

        public ProductCouldNotBeUpdatedException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}