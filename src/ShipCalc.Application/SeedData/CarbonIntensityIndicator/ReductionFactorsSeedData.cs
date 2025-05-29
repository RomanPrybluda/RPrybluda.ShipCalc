using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

namespace ShipCalc.Application.SeedData;

public static class ReductionFactorsSeedData
{
    public static List<RefLineReductionFactor> GetData() => new()
    {
        new()
        {
            Id = Guid.NewGuid(),
            Year = 2023,
            ReductionFactorPercentage = 5
        },
        new()
        {
            Id = Guid.NewGuid(),
            Year = 2024,
            ReductionFactorPercentage = 7
        },
        new()
        {
            Id = Guid.NewGuid(),
            Year = 2025,
            ReductionFactorPercentage = 9
        },
        new()
        {
            Id = Guid.NewGuid(),
            Year = 2026,
            ReductionFactorPercentage = 11
        },

    };
}
