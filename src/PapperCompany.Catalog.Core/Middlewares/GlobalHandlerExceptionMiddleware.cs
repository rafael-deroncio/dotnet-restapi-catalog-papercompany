using System.Net;
using System.Text.Json;
using PapperCompany.Catalog.Core.Exceptions;
using PapperCompany.Catalog.Core.Extensions;
using PapperCompany.Catalog.Core.Responses;

namespace PapperCompany.Catalog.Core.Middlewares;

public class GlobalHandlerExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalHandlerExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BaseException ex)
        {
            ExceptionResponse response = new()
            {
                Title = ex.Title,
                Type = ex.Type.GetDescription(),
                Messages = [ex.Message]
            };

            string json = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(json);
        }
        catch (Exception)
        {
            ExceptionResponse response = new()
            {
                Title = "Internal Error",
                Type = ResponseType.Fatal.GetDescription(),
                Messages = ["An error occurred while processing the request."]
            };

            string json = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(json);
        }
    }
}
