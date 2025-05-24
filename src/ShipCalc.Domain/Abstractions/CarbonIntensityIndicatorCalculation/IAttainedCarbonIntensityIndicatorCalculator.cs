namespace ShipCalc.Domain.Abstractions
{
    public interface IAttainedCarbonIntensityIndicatorCalculator
    {
        decimal CalculateAttainedCarbonIntensityIndicator(
        Ship ship,
        decimal capacity,
        decimal co2EmissionsInTons,
        decimal distanceTravelledInNMs);
    }
}
