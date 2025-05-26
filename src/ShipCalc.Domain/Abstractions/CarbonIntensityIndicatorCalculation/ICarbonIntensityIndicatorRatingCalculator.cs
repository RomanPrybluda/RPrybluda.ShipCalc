using ShipCalc.Domain.Result;

namespace ShipCalc.Domain.Abstractions;

public interface ICarbonIntensityIndicatorRatingCalculator
{
    Task<CarbonIntensityIndicatorCalcResult> CalculateRatingAsync(
        Ship ship,
        decimal co2EmissionsInTons,
        decimal distanceTravelledInNMs,
        int year);
}
