using ShipCalc.Domain;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;

public interface IRatingThresholdsRepo
{
    Task<RatingThreshold> GetThresholdsAsync(ShipType shipType, decimal deadWeight);

}
