using ShipCalc.Domain.ReductionFactor;

namespace ShipCalc.Application.Abstractions
{
    public interface IRequiredCarbonIntensityIndicatorReductionFactorRepository
    {
        Task<RequiredCarbonIntensityIndicatorReductionFactor> GetByIdAsync(Guid id);

        Task<IEnumerable<RequiredCarbonIntensityIndicatorReductionFactor>> GetAllAsync();

        Task<RequiredCarbonIntensityIndicatorReductionFactor> GetByYearAsync(int year);

        Task<IEnumerable<RequiredCarbonIntensityIndicatorReductionFactor>> GetByYearRangeAsync(int minYear, int maxYear);

        Task AddAsync(RequiredCarbonIntensityIndicatorReductionFactor factor);

        Task UpdateAsync(RequiredCarbonIntensityIndicatorReductionFactor factor);

        Task DeleteAsync(Guid id);
    }
}
