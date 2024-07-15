using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ModuleA1.API
{
    public static class ModuleA1ServiceCollectionExtensions
    {
        public static IServiceCollection AddModuleA1(this IServiceCollection services)
        {
            
            services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(typeof(ModuleA1.Application.DIForMediatr).Assembly));
            // Register other ModuleA services
            return services;
        }
    }
}
