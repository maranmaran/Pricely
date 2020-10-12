using Common.Exceptions;
using FluentValidation;
using IdentityService.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace IdentityService.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ILogger<GlobalExceptionMiddleware> logger)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(logger, httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(ILogger logger, HttpContext context, Exception exception)
        {
            logger.LogError(exception, $"{exception.Message} {exception.InnerException?.Message}");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            // generic internal server error
            var errorDetails = new ErrorDetails()
            {
                Status = HttpStatusCode.InternalServerError,
                Message = exception.Message,
            };

            if (exception is NotFoundException notFoundEx)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                errorDetails.Status = HttpStatusCode.NotFound;
                errorDetails.Message = notFoundEx.Message;
            }

            if (exception is ValidationException validationEx)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorDetails.Status = HttpStatusCode.BadRequest;
                errorDetails.Message = validationEx.Message;
                errorDetails.Errors = JsonConvert.SerializeObject(validationEx.Errors);
            }

            if (exception is UnauthorizedAccessException unauthorizedEx)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorDetails.Status = HttpStatusCode.Unauthorized;
                errorDetails.Message = unauthorizedEx.Message;
            }

            await context.Response.WriteAsync(errorDetails.ToString());
        }
    }
}
