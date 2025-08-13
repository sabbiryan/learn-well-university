using Microsoft.AspNetCore.Http;

namespace LearnWellUniversity.WebApi.Extensions
{
    public static class HttpContextIpExtensions
    {
        public static string GetClientIp(this HttpContext httpContext)
        {
            var forwardedFor = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(forwardedFor))
            {                
                var clientIp = forwardedFor.Split(',').First().Trim();
                return clientIp;
            }

            return httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        }
    }
}
