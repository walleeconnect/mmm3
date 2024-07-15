using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA1.API
{

    public class CustomAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
        }

        public override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith("Permission:", StringComparison.OrdinalIgnoreCase))
            {
                var parts = policyName.Split(':');
                if (parts.Length == 2)
                {
                    var permissions = parts[1].Split(',')
                        .Select(p => Enum.TryParse(p, out Permissions perm) ? perm : Permissions.None)
                        .Where(p => p != Permissions.None)
                        .ToArray();

                    var policy = new AuthorizationPolicyBuilder()
                        .AddRequirements(new PermissionsRequirement(permissions))
                        .Build();

                    return Task.FromResult(policy);
                }
            }

            return base.GetPolicyAsync(policyName);
        }
    }

    //public class CustomAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    //{
    //    public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    //    {
    //    }

    //    public override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    //    {
    //        if (policyName.StartsWith("Permission:", StringComparison.OrdinalIgnoreCase))
    //        {
    //            var parts = policyName.Split(':');
    //            if (parts.Length == 2 && Enum.TryParse(parts[1], out Permissions permission))
    //            {
    //                var policy = new AuthorizationPolicyBuilder()
    //                    .AddRequirements(new PermissionsRequirement(permission))
    //                    .Build();

    //                return Task.FromResult(policy);
    //            }
    //        }

    //        return base.GetPolicyAsync(policyName);
    //    }
    //}
}
