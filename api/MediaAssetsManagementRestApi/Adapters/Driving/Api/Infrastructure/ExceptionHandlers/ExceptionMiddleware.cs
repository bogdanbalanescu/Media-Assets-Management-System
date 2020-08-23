using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Infrastructure.ExceptionHandlers
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorResponse = exception is ArgumentException ?
                new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = exception.Message
                } :
                new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Internal Server Error"
                };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(errorResponse.ToString());
        }
    }
}
