using ShipCalc.Domain;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Abstractions.Repositories
{
    public interface ICarbonIntensityIndicatorReferenceLineParameterRepository
    {
        Task<CarbonIntensityIndicatorReferenceLineParameter?> GetByIdAsync(Guid id);

        Task<IEnumerable<CarbonIntensityIndicatorReferenceLineParameter>> GetAllAsync();

        Task<CarbonIntensityIndicatorReferenceLineParameter> GetParametersByShipTypeAndCapacityAsync(ShipType shipType, decimal capacity);

        Task AddAsync(CarbonIntensityIndicatorReferenceLineParameter parameter);

        Task UpdateAsync(CarbonIntensityIndicatorReferenceLineParameter parameter);

        Task DeleteAsync(Guid id);
    }
}
