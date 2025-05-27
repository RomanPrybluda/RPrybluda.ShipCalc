using Microsoft.Extensions.DependencyInjection;
using ShipCalc.Application.Abstractions.Data;
using ShipCalc.Infrastructure.Database;

namespace ShipCalc.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {

        //services.AddDbContext<ShipCalcDbContext>();
        services.AddScoped<ISeedDataInitializer, SeedDataInitializer>();
        services.AddScoped<SeedDataRunner>();

        return services;
    }
}
