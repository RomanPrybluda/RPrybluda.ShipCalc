using ShipCalc.Application.Abstractions.Repositories;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

namespace ShipCalc.Application.Abstractions;

public interface ICarbonIntensityIndicatorCalcnRepo : IRepository
{
    Task<CarbonIntensityIndicatorCalculation> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<CarbonIntensityIndicatorCalculation> GetByShipIdAsync(Guid shipId, CancellationToken cancellationToken = default);

    Task<IEnumerable<CarbonIntensityIndicatorCalculation>> GetAllAsync(CancellationToken cancellationToken = default);

    Task AddAsync(CarbonIntensityIndicatorCalculation record, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}