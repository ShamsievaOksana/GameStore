using System;

namespace GameStore.Foundation.ExceptionHandling
{
    public interface IExceptionStatusCodeMapping
    {
        int Map(Type exceptionType);
    }
}