using Microsoft.Extensions.DependencyInjection;
using ViagemYamaha.Core.Contracts.Repositories;
using ViagemYamaha.Core.Contracts.Services;
using ViagemYamaha.Core.Data.Repositories;
using ViagemYamaha.Core.Services;

namespace ViagemYamaha.API.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependecyInjection(this IServiceCollection services)
        {
            services.AddScoped<IRotaRepository, RotaRepository>();
            services.AddScoped<IRotaService, RotaService>();

            return services;
        }
    }
}
