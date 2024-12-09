using System.Net;
using Microsoft.Extensions.Localization;
using TBC.PersonReference.Application.Constants;
using TBC.PersonReference.Core.Exceptions;

namespace TBC.PersonReference.API.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IStringLocalizer<GlobalExceptionMiddleware> _localizer;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger,
            IStringLocalizer<GlobalExceptionMiddleware> localizer)
        {
            _next = next;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.StackTrace);

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exception is NotFoundException notFoundException)
            {
                await HandleNotFoundExceptionAsync(context, notFoundException);
            }
            else if (exception is ValidationException validationException)
            {
                await HandleValidationExceptionAsync(context, validationException);
            }
            else
            {
                await HandleUnexpectedExceptionAsync(context, exception);
            }
        }

        private async Task HandleNotFoundExceptionAsync(HttpContext context, NotFoundException exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;

            await context.Response.WriteAsJsonAsync(new
            {
                Error = _localizer[ResourceFileConstants.ResourceNotFound].Value,
                Details = exception.Message
            });
        }

        private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            _logger.LogWarning(exception, ResourceFileConstants.ValidationError);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            await context.Response.WriteAsJsonAsync(new
            {
                Error = _localizer[ResourceFileConstants.ValidationError],
                Details = exception.Message
            });
        }

        private async Task HandleUnexpectedExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, ResourceFileConstants.UnexpectedError);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsJsonAsync(new
            {
                Error = _localizer[ResourceFileConstants.UnexpectedError],
                Details = _localizer[ResourceFileConstants.TryAgainLater]
            });
        }
    }
}
