using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain.Abstractions
{
    public interface ICarbonIntensityIndicatorRatingCalculator
    {
        decimal Capacity { get; }

        decimal ParametrA { get; }

        decimal ParametrB { get; }

        decimal CarbonIntensityIndicatorRef { get; }

        decimal RequiredCarbonIntensityIndicator { get; }

        decimal AttainedCarbonIntensityIndicator { get; }

        decimal CarbonIntensityIndicatorNumericalRating { get; }

        CarbonIntensityIndicatorRating CarbonIntensityIndicatorRating { get; }

        void CalculateRatingAsync(
            Ship ship,
            CarbonIntensityIndicatorReferenceLineParameters carbonIntensityIndicatorRefParameters,
            CarbonIntensityIndicatorRatingThresholds carbonIntensityIndicatorRatingThresholds,
            decimal co2EmissionsInTons,
            decimal distanceTravelledInNMs,
            int year);
    }
}
