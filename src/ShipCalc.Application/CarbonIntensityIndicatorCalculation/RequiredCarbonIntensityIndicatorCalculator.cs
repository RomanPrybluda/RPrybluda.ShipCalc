using ShipCalc.Application.Abstractions;
using ShipCalc.Domain.Abstractions;

namespace ShipCalc.Application.CarbonIntensityIndicatorCalculation
{
    public class RequiredCarbonIntensityIndicatorCalculator : IRequiredCarbonIntensityIndicatorCalculator
    {
        private readonly IRequiredCarbonIntensityIndicatorReductionFactorRepository _reductionFactorRepository;

        public RequiredCarbonIntensityIndicatorCalculator(
            IRequiredCarbonIntensityIndicatorReductionFactorRepository reductionFactorRepository)
        {
            _reductionFactorRepository = reductionFactorRepository
                ?? throw new ArgumentNullException(nameof(reductionFactorRepository));
        }

        public async Task<decimal> CalculateRequiredCarbonIntensityIndicator(
            decimal carbonIntensityIndicatorRef,
            int? year = null)
        {
            if (carbonIntensityIndicatorRef <= 0)
                throw new ArgumentException("Carbon Intensity Indicator Ref must be greater than zero.");

            int currentYear = 2025;
            int targetYear = year ?? currentYear;

            if (targetYear < 2023 || targetYear > 2030)
                throw new ArgumentException($"Year must be between 2023 and 2030. Provided year: {targetYear}");

            var reductionFactor = await _reductionFactorRepository.GetByYearAsync(targetYear);
            if (reductionFactor == null)
                throw new ArgumentException($"Reduction factor for year {targetYear} is not defined.");

            decimal z = reductionFactor.ReductionFactorPercentage;

            return (100m - z) / 100.0m * carbonIntensityIndicatorRef;
        }
    }
}
