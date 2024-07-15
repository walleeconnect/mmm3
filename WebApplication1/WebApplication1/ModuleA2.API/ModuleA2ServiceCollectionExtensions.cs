using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ModuleA2.API
{
    public static class ModuleA2ServiceCollectionExtensions
    {
        public static IServiceCollection AddModuleA2(this IServiceCollection services)
        {
            
            services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(typeof(ModuleA2.Application.DIForMediatr).Assembly));
            // Register other ModuleA services
            return services;
        }
    }
}
