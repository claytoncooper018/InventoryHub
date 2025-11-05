using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace InventoryHubAPI.Middleware
{
    public class SimpleAuthMiddleware
    {
        private readonly RequestDelegate _next;
        public SimpleAuthMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Get)
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("X-API-KEY", out var key) || key != "inventory-secret")
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { success = false, message = "Missing or invalid API key. Use header: X-API-KEY: inventory-secret" });
                return;
            }

            await _next(context);
        }
    }
}
