using System;
using System.Runtime.Serialization;

namespace GameStore.Domain.Products.Exceptions
{
    public class ProductMismatchException
        : Exception
    {
        public ProductMismatchException(int expectedProductId, int actualProductId)
            : this($"Expected Product ID to be {expectedProductId}, but found {actualProductId}")
        {
        }

        protected ProductMismatchException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ProductMismatchException(string? message)
            : base(message)
        {
        }

        public ProductMismatchException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}