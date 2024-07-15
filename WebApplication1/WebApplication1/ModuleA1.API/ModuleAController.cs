using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModuleA1.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ModuleA1.API
{
    [Route("api/moduleA")]
    [Authorize]
    public class ModuleAController : ControllerBase
    {
        IMediator _mediator;

        public ModuleAController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetModuleAQuery());
            return Ok(result);
        }
    }
}
