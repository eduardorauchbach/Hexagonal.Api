using Microsoft.Extensions.DependencyInjection;
using Hexagonal.Session.Implementation;

namespace Hexagonal.Session.Builder
{
    public static class SessionModule
    {
        public static IServiceCollection RegisterSession(this IServiceCollection services)
        {
            services.AddScoped<IDbSession, DbSession>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
