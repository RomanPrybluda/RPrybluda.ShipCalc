using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

namespace ShipCalc.Application.Abstractions;

public interface ICalculationDataRepo
{
    Task<CalculationData> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<CalculationData> GetByShipIdAsync(Guid shipId, CancellationToken cancellationToken = default);

    Task<IEnumerable<CalculationData>> GetAllAsync(CancellationToken cancellationToken = default);

    Task AddAsync(CalculationData record, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}