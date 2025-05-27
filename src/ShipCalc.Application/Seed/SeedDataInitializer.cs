namespace ShipCalc.Application.Seed;

public static class SeedDataInitializer
{
    public static async Task InitializeAsync(ShipCalcDbContext context)
    {

        await RatingThresholdsDataInitializer.InitializeAsync(context);
    }
}
