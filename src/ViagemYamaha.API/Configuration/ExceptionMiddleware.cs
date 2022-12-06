using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using ViagemYamaha.Core.Contracts.Api;

namespace ViagemYamaha.API.Configuration
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
            catch (ApplicationException ex)
            {
                await HandleExceptionAsync(httpContext, ex, (int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(new ErrorResult()
            {
                StatusCode = context.Response.StatusCode,
                Error = exception.Message
            }.ToString());
        }
    }
}
