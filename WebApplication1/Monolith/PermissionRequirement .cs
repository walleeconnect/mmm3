namespace Monolith
{
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    //[Flags]
    //public enum Permissions
    //{
    //    None = 0,
    //    ManageDirectTax = 1 << 0,
    //    ManageInDirectTax = 1 << 1,
    //    ManageCompliance = 1 << 2,
    //    DirectTaxReadOnly = 1 << 3,
    //    DirectTaxAddOnly = 1 << 4,
    //    DirectTaxModifyOnly = 1 << 5,
    //    DirectTaxUploadOnly = 1 << 6,
    //    DirectTaxDeleteOnly = 1 << 7,
    //    InDirectTaxReadOnly = 1 << 8,
    //    InDirectTaxAddOnly = 1 << 9,
    //    InDirectTaxModifyOnly = 1 << 10,
    //    InDirectTaxUploadOnly = 1 << 11,
    //    InDirectTaxDeleteOnly = 1 << 12,
    //    ComplianceTaxReadOnly = 1 << 13,
    //    ComplianceTaxAddOnly = 1 << 14,
    //    ComplianceTaxModifyOnly = 1 << 15,
    //    ComplianceTaxUploadOnly = 1 << 16,
    //    ComplianceDeleteOnly = 1 << 17
    //}

    //public class PermissionRequirement : IAuthorizationRequirement
    //{
    //    public string Permission { get; }

    //    public PermissionRequirement(string permission)
    //    {
    //        Permission = permission;
    //    }
    //}

    //public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    //{
    //    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    //    {
    //        if (context.User.HasClaim(c => c.Type == "Permission" && c.Value == requirement.Permission))
    //        {
    //            context.Succeed(requirement);
    //        }

    //        return Task.CompletedTask;
    //    }
    //}

}
