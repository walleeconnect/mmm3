using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModuleA1.Application;

namespace ModuleA1.API
{
    [Route("api/moduleA11")]
    public class ModuleA11Controller : ControllerBase
    {
        IMediator _mediator;

        public ModuleA11Controller(IMediator mediator)
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
