using System.Net;
using TestTask.BLL.Common.Exceptions;

namespace TestTask.Web.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = "500 INTERNAL SERVER";
            switch (exception)
            {
                case NotFoundException ex:
                    code = HttpStatusCode.NotFound;
                    result = $"{(int)code}. {ex.Message}";
                    break;
                case ContactEmailAlreadyExistException ex:
                    code = HttpStatusCode.Conflict;
                    result = $"{(int)code}. {ex.Message}";
                    break;
                case ContractorNameAlreadyExistException ex:
                    code = HttpStatusCode.Conflict;
                    result = $"{(int)code}. {ex.Message}";
                    break;
            }

            context.Response.StatusCode = (int)code;

            await context.Response.WriteAsync(result);
        }
    }
}
