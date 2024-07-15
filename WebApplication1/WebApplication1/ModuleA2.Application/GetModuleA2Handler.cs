using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA2.Application
{
    public class GetModuleA2Handler : IRequestHandler<GetModuleA2Query, string>
    {
        public GetModuleA2Handler()
        {
            
        }
        public Task<string> Handle(GetModuleA2Query request, CancellationToken cancellationToken)
        {
            return Task.FromResult("ModuleA2");
        }
    }
}
