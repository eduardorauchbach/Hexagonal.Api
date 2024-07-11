using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Hexagonal.Api.Configurations
{
    public static class SwaggerConfig
    {
        internal static IServiceCollection ConfigurateSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
             {
                 options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                 {
                     Name = "Authorization",
                     Type = SecuritySchemeType.ApiKey,
                     Scheme = "Bearer",
                     BearerFormat = "JWT",
                     In = ParameterLocation.Header,
                     Description = "Header de autorização JWT usando o esquema Bearer.\r\n\r\nInforme 'Bearer'[espaço] e o seu token.\r\n\r\nExamplo: \'Bearer 12345abcdef\'",
                 });

                 options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                        }
                    });

                 // Enable Swagger annotations
                 options.EnableAnnotations();

                 // Set the comments path for the Swagger JSON and UI.
                 var xmlFileApi = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                 var xmlFileCommon = $"Hexagonal.Common.xml";
                 var xmlFileDTO = $"Hexagonal.Dto.xml";

                 var xmlPathApi = Path.Combine(AppContext.BaseDirectory, xmlFileApi);
                 var xmlPathCommon = Path.Combine(AppContext.BaseDirectory, xmlFileCommon);
                 var xmlPathDTO = Path.Combine(AppContext.BaseDirectory, xmlFileDTO);

                 options.IncludeXmlComments(xmlPathApi);
                 options.IncludeXmlComments(xmlPathCommon);
                 options.IncludeXmlComments(xmlPathDTO);
                 options.UseInlineDefinitionsForEnums();

                 options.CustomSchemaIds(type => type.FullName);
             });

            return services;
        }
    }
}
