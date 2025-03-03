using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace afi_demo.Classes;

public class GlobalExceptionHandler(IHostEnvironment env, ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private const string UnhandledExceptionMsg = "An unhandled exception has occurred while executing the request.";

    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, exception.Message);

        var problemDetails = CreateProblemDetails(context, exception);
        var json = ToJson(problemDetails);

        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsync(json, cancellationToken);

        return true;
    }

    private ProblemDetails CreateProblemDetails(HttpContext context, Exception exception)
    {
        var statusCode = 500;
        var data = exception.Data;
        var message = exception.Message;

        if (exception.GetType() == typeof(CustomException))
        {
            var e = (CustomException)exception;

            message = e.Message;

            if (e.statusCode != 0)
            {
                statusCode = e.statusCode;
                context.Response.StatusCode = e.statusCode;
            }

            if (!string.IsNullOrEmpty(e.detail))
                data.Add("customDetail", e.detail);
        }
        else
            statusCode = context.Response.StatusCode;

        var reasonPhrase = ReasonPhrases.GetReasonPhrase(statusCode);
        if (string.IsNullOrEmpty(reasonPhrase))
        {
            reasonPhrase = UnhandledExceptionMsg;
        }
        data.Add("ReasonPhrase", reasonPhrase);

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = message,
            Extensions =
            {
                [nameof(data)] = data
            }
        };

        if (!env.IsDevelopment())
        {
            return problemDetails;
        }

        problemDetails.Detail = exception.ToString();
        problemDetails.Extensions["traceId"] = context.TraceIdentifier;
        problemDetails.Extensions["data"] = data;

        return problemDetails;
    }

    private string ToJson(in ProblemDetails problemDetails)
    {
        try
        {
            return JsonSerializer.Serialize(problemDetails, SerializerOptions);
        }
        catch (Exception ex)
        {
            const string msg = "An exception has occurred while serializing error to JSON";
            logger.LogError(ex, msg);
        }

        return string.Empty;
    }
}