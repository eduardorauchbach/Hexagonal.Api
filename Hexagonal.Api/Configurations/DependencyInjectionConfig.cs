using Hexagonal.Adapter.Blob.Builder;
using Hexagonal.Repositories.Builder;
using Hexagonal.Services.Builder;
using Hexagonal.Session.Builder;
using RauchTech.Logging.Api;
using RauchTech.Logging.Azure;
using RauchTech.Logging.Builder;

namespace Hexagonal.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        /// <summary>
        /// Resolve all Dependency Injection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfigurationRoot config)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.ConfigurateSwaggerGen();

            services.AddSignalR(e =>
            {
                e.EnableDetailedErrors = true;
            });

            services.RegisterCustomLogs();
            services.RegisterAzureBlob(config);
            services.ConfigureAzureLogging(config);

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<CustomLogFilter>();
            });

            services.RegisterSession();
            services.RegisterRepositories();
            services.RegisterServices();
        }
    }
}
