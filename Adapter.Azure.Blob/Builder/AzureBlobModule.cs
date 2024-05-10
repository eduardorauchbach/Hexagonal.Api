using Microsoft.Extensions.DependencyInjection;
using Hexagonal.Adapter.Blob.Repository;
using Microsoft.Extensions.Configuration;
using Adapter.Azure.Blob.Configurations;
using Adapter.Azure.Blob;

namespace Hexagonal.Adapter.Blob.Builder
{
    public static class AzureBlobModule
    {
        public static IServiceCollection RegisterAzureBlob(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IBlobRepository, BlobRepository>();

            services.Configure<BlobSettings>(config.GetSection("BlobSettings"));
            services.AddOptions();

            BlobPathHelper.Initialize(config["BlobSettings:BaseUrl"]);

            return services;
        }
    }
}
