using BusinessLogicLayer.Exceptions.Abstract;
using InfrastructureLayer.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace InfrastructureLayer
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger<ErrorDetails> logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = ExceptionConfiguration.ResponseContentType;

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        if (contextFeature.Error is ServiceException serviceException)
                        {
                            SetResponseStatusCode(context, serviceException);
                            await GenerateResponseBodyAsync(context, serviceException.Message);
                        }
                        else
                        {
                            logger.LogCritical(String.Format(ExceptionConfiguration.LogMessage, contextFeature.Error));
                            await GenerateResponseBodyAsync(context, ExceptionConfiguration.UserMessage);
                        }
                        
                    }
                });
            });
        }

        private static void SetResponseStatusCode(HttpContext context, ServiceException exception)
        {
            var statusCode = exception switch
            {
                NotFoundException _ => HttpStatusCode.NotFound,
                IncorrectModelException _ => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError,
            };

            context.Response.StatusCode = (int)statusCode;
        }

        private static async Task GenerateResponseBodyAsync(HttpContext context, string message)
        {
            await context.Response.WriteAsync(new ErrorDetails(message).ToString());
        }
    }
}
