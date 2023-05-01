using Common.Models.DataTransferObjects;
using Common.Models.Exceptions.Generic;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace API.Middleware
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseExceptionHandler(app =>
            {
                app.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        // Exception has been thrown in the application
                        var error = contextFeature.Error;

                        var statusCode = GetStatusCodeFromException(error);
                        var message = GetMessageFromException(error);

                        var errorDto = new ErrorDto(statusCode, message);

                        context.Response.StatusCode = statusCode;

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorDto, Formatting.Indented));
                    }
                });
            });
        }

        private static int GetStatusCodeFromException(Exception error)
        {
            return error switch
            {
                BadRequestException => BadRequestException.STATUS_CODE,
                _ => StatusCodes.Status500InternalServerError
            };
        }

        // currently just returns the exception message but can be expanded based on exception type
        private static string GetMessageFromException(Exception error)
        {
            return error.Message;
        }
    }
}