using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA1.Application
{
    public class GetModuleAHandler : IRequestHandler<GetModuleAQuery, string>
    {
        public GetModuleAHandler()
        {
            
        }
        public Task<string> Handle(GetModuleAQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult("ModuleA");
        }
    }
}
