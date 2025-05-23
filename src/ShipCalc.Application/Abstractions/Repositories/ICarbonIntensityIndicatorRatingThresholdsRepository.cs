using ShipCalc.Domain;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Abstractions.Repositories
{
    public interface ICarbonIntensityIndicatorRatingThresholdsRepository
    {
        Task<CarbonIntensityIndicatorRatingThresholds> GetThresholdsAsync(ShipType shipType, double deadWeight);
    }
}
