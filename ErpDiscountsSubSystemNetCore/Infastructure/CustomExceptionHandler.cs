using Application.Shared.Exceptions;
using System.Net;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace ErpDiscountsSubSystemNetCore.Infastructure
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        private readonly Dictionary<Type, Func<ExceptionHandlerContext, Exception, Task>> _exceptionHandlers;

        public CustomExceptionHandler()
        {
            _exceptionHandlers = new()
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
                { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
            };
        }

        public async Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            await TryHandleAsync(context, cancellationToken);
        }

        public async ValueTask<bool> TryHandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            var exceptionType = context.Exception.GetType();

            if (_exceptionHandlers.ContainsKey(exceptionType))
            {
                await _exceptionHandlers[exceptionType].Invoke(context, context.Exception);
                return true;
            }

            return false;
        }

        private async Task HandleValidationException(ExceptionHandlerContext httpContext, Exception ex)
        {
            var exception = (ValidationException)ex;

            httpContext.ExceptionContext.Response.StatusCode = HttpStatusCode.BadRequest;

            httpContext.Result = await Task.FromResult(new TextPlainErrorResult
            {
                Request = httpContext.ExceptionContext.Request,
                Content = exception.Message
            });
        }


        private async Task HandleUnauthorizedAccessException(ExceptionHandlerContext httpContext, Exception ex)
        {

            httpContext.ExceptionContext.Response.StatusCode = HttpStatusCode.Unauthorized;
            httpContext.Result = await Task.FromResult(new TextPlainErrorResult
            {
                Request = httpContext.ExceptionContext.Request,
                Content = ex.Message
            });
        }

        private async Task HandleForbiddenAccessException(ExceptionHandlerContext httpContext, Exception ex)
        {
            httpContext.ExceptionContext.Response.StatusCode = HttpStatusCode.Forbidden;
            httpContext.Result = await Task.FromResult(new TextPlainErrorResult
            {
                Request = httpContext.ExceptionContext.Request,
                Content = ex.Message
            });
        }

        private class TextPlainErrorResult : IHttpActionResult
        {
            public HttpRequestMessage Request { get; set; }

            public string Content { get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(Content);
                response.RequestMessage = Request;
                return Task.FromResult(response);
            }
        }
    }
}
