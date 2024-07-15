using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleB1.Application
{
    public class GetModuleB1Handler : IRequestHandler<GetModuleB1Query, string>
    {
        public Task<string> Handle(GetModuleB1Query request, CancellationToken cancellationToken)
        {
            return Task.FromResult("ModuleB1");
        }
    }
}
