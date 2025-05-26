using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

namespace ShipCalc.Domain.Abstractions.CarbonIntensityIndicator;

public interface IRatingCalculator
{
    Task<CalculationData> CalculateRatingAsync(
        Ship ship,
        decimal co2EmissionsInTons,
        decimal distanceTravelledInNMs,
        int year);
}
