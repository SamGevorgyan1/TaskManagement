using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskManagement.Application.Exceptions;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, context.Exception.Message);

        var statusCode = 500;
        var message = "An unexpected error occurred";

        switch (context.Exception)
        {
            case AppException appException:
                statusCode = (int)appException.StatusCode;
                message = appException.Message;
                break;
            case ValidationException validationException:
                statusCode = (int)HttpStatusCode.BadRequest;
                message = validationException.Message;
                break;
        }

        context.Result = new ObjectResult(new { error = message })
        {
            StatusCode = statusCode
        };

        context.ExceptionHandled = true;
    }
}