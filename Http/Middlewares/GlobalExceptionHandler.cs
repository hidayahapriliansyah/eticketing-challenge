using System.Net;
using eticketing.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace eticketing.Http.Middlewares;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        var problemDetails = new ProblemDetails { Instance = httpContext.Request.Path };
        if (exception is ApiCustomException e)
        {
            httpContext.Response.StatusCode = (int)e.StatusCode;
            problemDetails.Title = e.Message ?? exception.Message;
        }
        else
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            problemDetails.Title = exception.Message;
            logger.LogError("{ProblemDetailsTitle}", problemDetails.Title);
        }
        problemDetails.Status = httpContext.Response.StatusCode;
        await httpContext
            .Response.WriteAsJsonAsync(problemDetails, cancellationToken)
            .ConfigureAwait(false);
        return true;
    }
}
