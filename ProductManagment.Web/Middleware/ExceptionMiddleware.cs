using FluentValidation;
using ProductManagment.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace ProductManagment.Web.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var errorResponse = new { message = exception.Message };

            switch (exception)
            {
                case NotFoundException _:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case ValidationException _:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse = new { message = "An unexpected error occurred." };
                    break;
            }

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            return response.WriteAsync(jsonResponse);
        }
    }
}
