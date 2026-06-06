using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Src.Shared;

namespace Api.Src.Api.Middlewares;

public class AppExceptionHandlerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BusinessException ex)
        {
            await WriteResponseAsync(
                context,
                StatusCodes.Status422UnprocessableEntity,
                ex.Message
            );
        }
        catch (ConflictException ex)
        {
            await WriteResponseAsync(
                context,
                StatusCodes.Status409Conflict,
                ex.Message
            );
        }
        catch (ForbidException ex)
        {
            await WriteResponseAsync(
                context,
                StatusCodes.Status403Forbidden,
                ex.Message
            );
        }
        catch (NotFoundException ex)
        {
            await WriteResponseAsync(
                context,
                StatusCodes.Status404NotFound, 
                ex.Message
            );
        }
        catch (UnauthorizedException ex)
        {
            await WriteResponseAsync(
                context,
                StatusCodes.Status401Unauthorized, 
                ex.Message
            );
        }
        catch (ValidationException ex)
        {
            await WriteResponseAsync(
                context,
                StatusCodes.Status400BadRequest, 
                ex.Message
            );
        }
        catch (Exception ex)
        {
            await WriteResponseAsync(
                context,
                StatusCodes.Status500InternalServerError, 
                ex.Message
            );
        }
    }

    private static async Task WriteResponseAsync(HttpContext context, int statusCode, string message)
    {
        Console.WriteLine($"[{DateTime.Now}] {context.Request.Method} {context.Request.Path}: {message}");

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(new
        {
            StatusCode = statusCode,
            Detail = message
        });
    }
}