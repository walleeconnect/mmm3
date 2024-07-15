namespace UserManagement.API
{
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserPermissionService permissionService)
        {
            // Extract required details from context, e.g., user ID, group ID, entity ID, module ID, submodule ID, permission name
            // Example: string userId = context.User.Identity.Name;

            // Perform permission check using permissionService.UserHasPermissionAsync(...)
            // If the user does not have the required permissions, set the response status code to 403 Forbidden
            // context.Response.StatusCode = StatusCodes.Status403Forbidden;

            await _next(context);
        }
    }
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }

    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "Permission" && c.Value == requirement.Permission))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

}
