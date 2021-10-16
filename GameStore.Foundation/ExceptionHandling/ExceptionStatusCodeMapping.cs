using System;
using System.Collections.Generic;
using System.Net;

namespace GameStore.Foundation.ExceptionHandling
{
    public class ExceptionStatusCodeMapping
        : IExceptionStatusCodeMapping
    {
        private readonly Dictionary<Type, HttpStatusCode> _exceptionMapping;

        public ExceptionStatusCodeMapping(Dictionary<Type, HttpStatusCode> exceptionMapping)
        {
            _exceptionMapping = exceptionMapping;
        }

        public int Map(Type exceptionType)
        {
            exceptionType.ShouldNotBeNull();

            var httpStatusCode = HttpStatusCode.InternalServerError;
            var isStatusCodeMapped = false;

            while (!isStatusCodeMapped)
            {
                if (exceptionType == typeof(Exception) || exceptionType == typeof(object))
                    break;

                isStatusCodeMapped = _exceptionMapping.TryGetValue(exceptionType, out var mappedStatusCode);
                if (isStatusCodeMapped)
                    httpStatusCode = mappedStatusCode;

                exceptionType = exceptionType.BaseType;
            }

            return (int)httpStatusCode;
        }
    }
}