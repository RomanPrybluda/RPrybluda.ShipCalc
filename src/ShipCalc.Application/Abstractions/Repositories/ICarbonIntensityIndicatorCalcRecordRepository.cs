using ShipCalc.Domain;

namespace ShipCalc.Application.Abstractions;

public interface ICarbonIntensityIndicatorCalcRecordRepository
{
    Task<CarbonIntensityIndicatorCalcRecord> GetByIdAsync(Guid id);

    Task<IEnumerable<CarbonIntensityIndicatorCalcRecord>> GetByShipIdAsync(Guid shipId);

    Task<IEnumerable<CarbonIntensityIndicatorCalcRecord>> GetAllAsync();

    Task AddAsync(CarbonIntensityIndicatorCalcRecord record);

    Task UpdateAsync(CarbonIntensityIndicatorCalcRecord record);

    Task DeleteAsync(Guid id);
}