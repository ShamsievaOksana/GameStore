using System;
using System.Runtime.Serialization;

namespace GameStore.Domain.Products.Exceptions
{
    public class ProductNotFoundException
        : Exception
    {
        public ProductNotFoundException(int productId)
            : this($"Product {productId} does not exist.")
        {
        }

        protected ProductNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ProductNotFoundException(string? message)
            : base(message)
        {
        }

        public ProductNotFoundException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}