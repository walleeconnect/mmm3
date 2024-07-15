using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModuleA2.Application;

namespace ModuleA2.API
{
    [Route("api/moduleA2")]
    public class ModuleA2Controller : ControllerBase
    {
        IMediator _mediator;

        public ModuleA2Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetModuleA2Query());
            return Ok(result);
        }
    }
}
