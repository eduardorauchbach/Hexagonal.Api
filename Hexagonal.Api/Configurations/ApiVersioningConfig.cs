namespace Hexagonal.Api.Configurations
{
    public static class ApiVersioningConfig
    {
        /// <summary>
        /// Versioning api service configuration
        /// </summary>
        /// <param name="services"></param>
        public static void AddApiVersioningConfig(this IServiceCollection services)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));

            services.AddApiVersioning(p =>
            {
                p.DefaultApiVersion = new(1, 0);
                p.ReportApiVersions = true;
                p.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });
        }
    }
}
