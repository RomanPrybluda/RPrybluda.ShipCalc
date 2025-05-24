namespace ShipCalc.Domain.Abstractions
{
    public interface IRequiredCarbonIntensityIndicatorCalculator
    {
        Task<decimal> CalculateRequiredCarbonIntensityIndicator(
            decimal carbonIntensityIndicatorRef,
            int? year = null);
    }
}
