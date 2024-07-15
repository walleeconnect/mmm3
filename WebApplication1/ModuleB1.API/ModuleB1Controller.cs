using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModuleB1.Application;

namespace ModuleB1.API
{
    [Route("api/moduleB1")]
    public class ModuleB1Controller : ControllerBase
    {
        IMediator _mediator;

        public ModuleB1Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetModuleB1Query());
            return Ok(result);
        }
    }
}
