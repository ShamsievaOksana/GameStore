using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;

namespace GameStore.Foundation
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Writes an action result to HTTP response using <see cref="IActionResultExecutor{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult">The type of the action result.</typeparam>
        /// <param name="httpContext">Current HTTP context.</param>
        /// <param name="result">An action result.</param>
        /// <returns>Asynchronous task.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpContext"/> or <paramref name="result"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if there is no implementation of <see cref="IActionResultExecutor{TResult}"/></exception>
        public static async Task WriteActionResult<TResult>(this HttpContext httpContext, TResult result)
            where TResult : IActionResult
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            if (result == null)
                throw new ArgumentNullException(nameof(result));

            var executor = httpContext.GetActionResultExecutor<TResult>();

            if (executor == null)
                throw new InvalidOperationException($"There is no ActionResultExecutor for {typeof(TResult).Name}.");

            var routeData = httpContext.GetRouteData() ?? new RouteData();
            var actionContext = new ActionContext(httpContext, routeData, new ActionDescriptor());
            await executor.ExecuteAsync(actionContext, result);
        }

        /// <summary>
        /// Writes an <see cref="ObjectResult"/> to HTTP response using <see cref="IActionResultExecutor{ObjectResult}"/>.
        /// </summary>
        /// <param name="httpContext">Current HTTP context.</param>
        /// <param name="result">An <see cref="ObjectResult"/> instance.</param>
        /// <returns>Asynchronous task.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpContext"/> or <paramref name="result"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if there is no implementation of <see cref="IActionResultExecutor{ObjectResult}"/></exception>
        public static async Task WriteObjectResult(this HttpContext httpContext, ObjectResult result)
        {
            await httpContext.WriteActionResult(result);
        }

        /// <summary>
        /// Gets action result executor for <typeparamref name="TResult"/> action result.
        /// </summary>
        /// <typeparam name="TResult">The type of the action result.</typeparam>
        /// <param name="httpContext">Current HTTP context.</param>
        /// <returns>Asynchronous task.</returns>
        public static IActionResultExecutor<TResult> GetActionResultExecutor<TResult>(this HttpContext httpContext)
            where TResult : IActionResult
        {
            return httpContext.RequestServices.GetService(typeof(IActionResultExecutor<TResult>)) as
                IActionResultExecutor<TResult>;
        }
    }
}