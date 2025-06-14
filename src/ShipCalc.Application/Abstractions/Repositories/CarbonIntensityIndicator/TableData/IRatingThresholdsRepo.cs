using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;

public interface IRatingThresholdsRepo : IRepository
{
    Task<RatingThreshold> GetThresholdsAsync(ShipType shipType, decimal deadWeight, CancellationToken cancellationToken = default);

}
