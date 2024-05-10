using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hexagonal.Api.Configurations
{
    public static class ControllersConfig
    {
        /// <summary>
        /// Controller service configuration. Contains configuration about Serialization, Api Behavior Options, ...
        /// </summary>
        /// <param name="services"></param>
        public static void AddControllersConfig(this IServiceCollection services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
                options.ReturnHttpNotAcceptable = true;
                options.EnableEndpointRouting = true;
                options.RequireHttpsPermanent = true;
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
                options.JsonSerializerOptions.IgnoreReadOnlyFields = true;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            })
            .AddNewtonsoftJson(newtonsoft =>
            {
                newtonsoft.SerializerSettings.Converters.Add(new StringEnumConverter());
                newtonsoft.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver()
                {
                    IgnoreSerializableAttribute = true,
                    SerializeCompilerGeneratedMembers = true,
                    IgnoreIsSpecifiedMembers = false,
                };
                newtonsoft.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                newtonsoft.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                newtonsoft.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            })
            .ConfigureApiBehaviorOptions(setupAction =>
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();
                    var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(context.HttpContext, context.ModelState);

                    problemDetails.Detail = "See the errors field for details.";
                    problemDetails.Instance = context.HttpContext.Request.Path;

                    var actionExecutingContext = context as ActionExecutingContext;

                    if (context.ModelState.ErrorCount > 0 && actionExecutingContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count)
                    {
                        problemDetails.Type = "https://leadsoft.inf.com/developers/modelvalidationproblem";
                        problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                        problemDetails.Title = "One or more validation errors ocurred.";

                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { Constant.ApplicationProblemJson }
                        };
                    }

                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "One or more validation errors ocurred.";

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { Constant.ApplicationProblemJson }
                    };
                };
            });
        }
    }
}
