using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShipCalc.Application.Abstractions.Data;
using ShipCalc.Infrastructure.Database;

namespace ShipCalc.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddDatabase(configuration)
            .AddInternalServices();

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        var localConnectionString = configuration["ConnectionStrings:LocalConnectionString"];

        if (!string.IsNullOrWhiteSpace(localConnectionString))
        {
            connectionString = localConnectionString;
        }

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Connection string is not set. Check environment variables, appsettings.json, or secrets.");
        }

        services.AddDbContext<ShipCalcDbContext>(options =>
            options.UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "Default"))
                   .UseSnakeCaseNamingConvention());

        services.AddScoped<IShipCalcDbContext>(sp => sp.GetRequiredService<ShipCalcDbContext>());

        return services;
    }

    private static IServiceCollection AddInternalServices(this IServiceCollection services)
    {
        services.AddScoped<ISeedDataInitializer, SeedDataInitializer>();
        services.AddScoped<SeedDataRunner>();

        return services;
    }
}
