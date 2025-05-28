using Microsoft.EntityFrameworkCore;
using ShipCalc.Infrastructure.Database;

namespace ShipCalc.Api.Extensions;

public static class MigrationExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<ShipCalcDbContext>();
            await context.Database.MigrateAsync();

            var runner = services.GetRequiredService<SeedDataRunner>();
            await runner.RunAllAsync();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during database initialization.");
        }
    }
}