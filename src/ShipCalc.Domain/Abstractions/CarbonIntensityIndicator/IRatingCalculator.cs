using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

namespace ShipCalc.Domain.Abstractions.CarbonIntensityIndicator;

public interface IRatingCalculator : ICalculator
{
    Task<CarbonIntensityIndicatorCalculation> CalculateRatingAsync(
        Ship ship,
        decimal co2EmissionsInTons,
        decimal distanceTravelledInNMs,
        int year);
}
