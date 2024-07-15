using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ModuleB1.API
{
    public static class ModuleB1ServiceCollectionExtensions
    {
        public static IServiceCollection AddModuleB1(this IServiceCollection services)
        {
            
            services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(typeof(ModuleB1.Application.DIForMediatr).Assembly));
            // Register other ModuleA services
            return services;
        }
    }
}
