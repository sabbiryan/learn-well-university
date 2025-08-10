using LearnWellUniversity.Application.Common;
using Serilog;
using System.Net;
using System.Text.Json;

namespace LearnWellUniversity.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Serilog.ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
            _logger = Log.ForContext<ExceptionHandlingMiddleware>();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Unhandled exception occurred");

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";


                var response = new ApiResponse<string>("Internal Server Error. Please try again later.",StatusCodes.Status500InternalServerError);

                var json = JsonSerializer.Serialize(response);

                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}
