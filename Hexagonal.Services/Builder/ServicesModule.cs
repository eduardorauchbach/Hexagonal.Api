﻿using Hexagonal.Adapter.Blob;
using Hexagonal.Services.Crypt;
using Hexagonal.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Hexagonal.Services.Builder
{
    public static class ServicesModule
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IHashService, HashService>();
            services.AddSingleton<ITokenService, TokenService>();

            services.AddSingleton<IBlobService, BlobService>();

            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVerificationService, VerificationService>();

            return services;
        }
    }
}
