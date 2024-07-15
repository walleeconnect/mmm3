using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModuleA1.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ModuleA1.API
{
    [Route("api/moduleA")]
    //[Authorize]
    public class ModuleAController : ControllerBase
    {
        IMediator _mediator;

        public ModuleAController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
       // [Authorize(Policy = "PermissionPolicy")]
        [Permissions(Permissions.ComplianceDeleteOnly )]
        public async Task<IActionResult> Get()
        {

            var result = await _mediator.Send(new GetModuleAQuery());
            return Ok(result);
        }

        [HttpGet]
        [Route("/somemore")]
        // [Authorize(Policy = "PermissionPolicy")]
        [Permissions(Permissions.ManageInDirectTax)]
        public async Task<IActionResult> GetSomeMore()
        {

            var result = await _mediator.Send(new GetModuleAQuery());
            return Ok(result);
        }
    }

    [Flags]
    public enum Permissions
    {
        None = 0,
        ManageDirectTax = 1 << 0,
        ManageInDirectTax = 1 << 1,
        ManageCompliance = 1 << 2,
        DirectTaxReadOnly = 1 << 3,
        DirectTaxAddOnly = 1 << 4,
        DirectTaxModifyOnly = 1 << 5,
        DirectTaxUploadOnly = 1 << 6,
        DirectTaxDeleteOnly = 1 << 7,
        InDirectTaxReadOnly = 1 << 8,
        InDirectTaxAddOnly = 1 << 9,
        InDirectTaxModifyOnly = 1 << 10,
        InDirectTaxUploadOnly = 1 << 11,
        InDirectTaxDeleteOnly = 1 << 12,
        ComplianceTaxReadOnly = 1 << 13,
        ComplianceTaxAddOnly = 1 << 14,
        ComplianceTaxModifyOnly = 1 << 15,
        ComplianceTaxUploadOnly = 1 << 16,
        ComplianceDeleteOnly = 1 << 17
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class PermissionsAttribute : AuthorizeAttribute
    {
        public PermissionsAttribute(params Permissions[] permissions)
        {
            Policy = $"Permission:{string.Join(",", permissions)}";
        }

        public Permissions Permission { get; }
    }

    //[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    //public class PermissionsAttribute : AuthorizeAttribute
    //{
    //    public PermissionsAttribute(Permissions permission)
    //    {
    //        Policy = $"Permission:{permission}";
    //    }

    //    public Permissions Permission { get; }
    //}
}
