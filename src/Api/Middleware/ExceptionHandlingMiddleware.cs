namespace Api.Middleware
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Process.Aspects.Validation;

    public class ExceptionHandlingMiddleware
    {
        readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    throw;
                }

                ErrorResponse response = ErrorResponse.InternalServerError;
                int code = (int)HttpStatusCode.InternalServerError;

                if (ex is PipelineValidationException pipelineException)
                {
                    code = (int)HttpStatusCode.BadRequest;

                    response = new ErrorResponse
                    {
                        Errors = pipelineException
                            .Errors
                            .Select(x => x.ErrorMessage)
                            .ToArray()
                    };
                }

                context.Response.Clear();
                context.Response.StatusCode = code;
                context.Response.ContentType = "application/json";

                string responseJson = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(responseJson);
            }
        }
    }

    public class ErrorResponse
    {
        public static readonly ErrorResponse InternalServerError = new ErrorResponse
        {
            Errors = new[] { "An internal server error occurred." }
        };

        public string[] Errors { get; set; }
    }
}
