using ShipCalc.Api.Extensions;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ShipCalc.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.Strict;
            });

            services.AddEndpoints(Assembly.GetExecutingAssembly());

            //services.AddExceptionHandler<GlobalExceptionHandler>(); TODO
            //services.AddProblemDetails(); TODO

            return services;
        }
    }
}
