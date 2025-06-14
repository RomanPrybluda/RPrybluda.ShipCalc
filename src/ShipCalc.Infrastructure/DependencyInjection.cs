using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShipCalc.Application.Abstractions.Data;
using ShipCalc.Application.Abstractions.Repositories;
using ShipCalc.Infrastructure.Database;
using ShipCalc.Infrastructure.Repositories.CarbonIntensityIndicator.TableData;

namespace ShipCalc.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddDatabase(configuration)
            .AddRepositories()
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

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<CarbonIntensityIndicatorRefLineParamsRepo>()
            .AddClasses(classes => classes.AssignableTo<IRepository>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
