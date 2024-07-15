namespace Monolith
{
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //if (context.User.Identity?.IsAuthenticated == true)
            //{
            //    var permissionsClaim = context.User.Claims.FirstOrDefault(c => c.Type == "Permissions");

            //    if (permissionsClaim == null)
            //    {
            //        context.Response.StatusCode = StatusCodes.Status403Forbidden;
            //        await context.Response.WriteAsync("Permission claim not found.");
            //        return;
            //    }

            //    // Add your logic to validate permissions here.
            //    // For example, check if the user has a specific permission:
            //    var requiredPermission = context.Request.Headers["Required-Permission"].ToString();
            //    if (!string.IsNullOrEmpty(requiredPermission) && !permissionsClaim.Value.Contains(requiredPermission))
            //    {
            //        context.Response.StatusCode = StatusCodes.Status403Forbidden;
            //        await context.Response.WriteAsync("Permission denied.");
            //        return;
            //    }
            //}

            await _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class PermissionMiddlewareExtensions
    {
        public static IApplicationBuilder UsePermissionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PermissionMiddleware>();
        }
    }
}
