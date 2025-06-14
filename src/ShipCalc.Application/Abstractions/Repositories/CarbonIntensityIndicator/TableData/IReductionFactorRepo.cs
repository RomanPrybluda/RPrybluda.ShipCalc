using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

namespace ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;

public interface IReductionFactorRepo : IRepository
{
    Task<RefLineReductionFactor> GetByYearAsync(int year, CancellationToken cancellationToken = default);
}
