namespace ShipCalc.Domain.Abstractions
{
    public interface IRequiredCarbonIntensityIndicatorCalculator
    {
        decimal CalculateRequiredCarbonIntensityIndicator(
            decimal carbonIntensityIndicatorRef,
            int? year = null);
    }
}
