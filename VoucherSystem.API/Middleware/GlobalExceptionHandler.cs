using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using VoucherSystem.Domain.Exceptions;

namespace VoucherSystem.API.Middleware;

public class GlobalExceptionHandler
{
    public GlobalExceptionHandler() { }

    public Task HandleAsync(HttpContext context)
    {
        var feature = context.Features.Get<IExceptionHandlerFeature>();
        var exception = feature?.Error;

        var code = 500;
        var type = "https://httpstatuses.io/500";
        var errors = new List<string>();

        if (exception == null)
        {
            errors.Add("An unexpected error occurred.");
            var emptyResult = JsonSerializer.Serialize(
                new
                {
                    type,
                    message = "An unexpected error occurred.",
                    errors,
                }
            );
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;
            return context.Response.WriteAsync(emptyResult);
        }

        switch (exception)
        {
            case VoucherExpiredException ve:
            case MaxUsesExceededException me:
            case CampaignInactiveException ce:
            case InvalidRedemptionException ire:
                code = 422;
                type = "https://httpstatuses.io/422";
                errors.Add(exception.Message);
                break;
            case ValidationException vf:
                code = 400;
                type = "https://httpstatuses.io/400";
                errors.AddRange(vf.Errors.Select(e => e.ErrorMessage));
                break;
            case KeyNotFoundException knf:
                code = 404;
                type = "https://httpstatuses.io/404";
                errors.Add(knf.Message);
                break;
            default:
                errors.Add("An unexpected error occurred.");
                break;
        }

        var result = JsonSerializer.Serialize(
            new
            {
                type,
                message = exception.Message,
                errors,
            }
        );
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;
        return context.Response.WriteAsync(result);
    }
}
