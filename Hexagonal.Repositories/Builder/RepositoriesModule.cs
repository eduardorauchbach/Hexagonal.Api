using Microsoft.Extensions.DependencyInjection;
using Hexagonal.Repositories.Implementation;

namespace Hexagonal.Repositories.Builder
{
    public static class RepositoriesModule
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVerificationRepository, VerificationRepository>();

            return services;
        }
    }
}
