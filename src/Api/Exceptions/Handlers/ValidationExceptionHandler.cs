using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Api.Exceptions.Handlers
{
    public class ValidationExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken)
        {
            if(exception is not ValidationException validationException)
            {
                return false;
            }

            httpContext.Response.StatusCode = validationException.Errors.Any(x => x.ErrorCode == HttpStatusCode.NotFound.ToString()) ? 
                StatusCodes.Status404NotFound 
                : StatusCodes.Status400BadRequest;

            var errors = validationException.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray()
            );

            var problemDetails = new HttpValidationProblemDetails(errors)
            {
                Status = httpContext.Response.StatusCode,
                Title = "Validation failed",
                Detail = "One or more validation errors occurred."
            };

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
