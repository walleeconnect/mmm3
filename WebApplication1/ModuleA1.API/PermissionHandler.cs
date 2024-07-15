using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA1.API
{
    public class PermissionsHandler : AuthorizationHandler<PermissionsRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionsRequirement requirement)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            var permissionsClaim = context.User.FindFirst("Permissions");
            if (permissionsClaim == null)
            {
                return Task.CompletedTask;
            }

            if (int.TryParse(permissionsClaim.Value, out int userPermissionsInt))
            {
                var userPermissions = (Permissions)userPermissionsInt;

                foreach (var requiredPermission in requirement.Permissions)
                {
                    if ((userPermissions & requiredPermission) == requiredPermission)
                    {
                        context.Succeed(requirement);
                        return Task.CompletedTask;
                    }
                }
            }

            return Task.CompletedTask;
        }
    }

    public class PermissionsRequirement : IAuthorizationRequirement
    {
        public PermissionsRequirement(params Permissions[] permissions)
        {
            Permissions = permissions;
        }

        public Permissions[] Permissions { get; }
    }
    //public class PermissionsHandler : AuthorizationHandler<PermissionsRequirement>
    //{
    //    private readonly IHttpContextAccessor _httpContextAccessor;

    //    public PermissionsHandler(IHttpContextAccessor httpContextAccessor)
    //    {
    //        _httpContextAccessor = httpContextAccessor;
    //    }

    //    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionsRequirement requirement)
    //    {
    //        if (context.User == null)
    //        {
    //            return Task.CompletedTask;
    //        }

    //        var permissionsClaim = context.User.FindFirst("Permissions");
    //        if (permissionsClaim == null)
    //        {
    //            return Task.CompletedTask;
    //        }

    //        if (int.TryParse(permissionsClaim.Value, out int userPermissionsInt))
    //        {
    //            var userPermissions = (Permissions)userPermissionsInt;

    //            if ((userPermissions & requirement.Permission) == requirement.Permission)
    //            {
    //                context.Succeed(requirement);
    //            }
    //        }

    //        return Task.CompletedTask;
    //    }
    //}

    //public class PermissionsRequirement : IAuthorizationRequirement
    //{
    //    public PermissionsRequirement(Permissions permission)
    //    {
    //        Permission = permission;
    //    }

    //    public Permissions Permission { get; }
    //}
}
