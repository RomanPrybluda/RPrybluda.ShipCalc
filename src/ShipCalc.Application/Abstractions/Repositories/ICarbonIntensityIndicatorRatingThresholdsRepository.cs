using ShipCalc.Domain;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Abstractions.Repositories;

public interface ICarbonIntensityIndicatorRatingThresholdsRepository
{
    Task<CarbonIntensityIndicatorRatingThreshold> GetThresholdsAsync(ShipType shipType, decimal deadWeight);

    Task<IEnumerable<CarbonIntensityIndicatorRatingThreshold>> GetAllAsync();

    Task AddAsync(CarbonIntensityIndicatorRatingThreshold threshold);

    Task UpdateAsync(CarbonIntensityIndicatorRatingThreshold threshold);

    Task DeleteAsync(Guid id);
}
