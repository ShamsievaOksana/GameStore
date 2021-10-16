using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GameStore.Foundation.ExceptionHandling
{
    public class ExceptionHandlingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly IExceptionStatusCodeMapping _exceptionStatusCodeMapping;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILoggerFactory loggerFactory,
            IExceptionStatusCodeMapping exceptionStatusCodeMapping)
        {
            _logger = loggerFactory.CreateLogger("General");
            _next = next;
            _exceptionStatusCodeMapping = exceptionStatusCodeMapping;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next?.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var exceptionType = exception.GetType();
            var statusCode = _exceptionStatusCodeMapping.Map(exceptionType);

            var errorDetails = new ErrorDetailsModel()
            {
                ErrorMessage = exception.Message
            };

            await WriteErrorResult(context, errorDetails, statusCode);
        }

        private static async Task WriteErrorResult(HttpContext httpContext, ErrorDetailsModel errorDetails, int statusCode)
        {
            await httpContext.WriteObjectResult(
                new ObjectResult(errorDetails)
                {
                    StatusCode = statusCode
                });
        }
    }
}