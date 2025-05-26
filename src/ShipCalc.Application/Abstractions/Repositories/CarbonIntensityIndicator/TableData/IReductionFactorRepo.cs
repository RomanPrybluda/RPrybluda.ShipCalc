using ShipCalc.Domain.ReductionFactor;

namespace ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;

public interface IReductionFactorRepo
{
    Task<ReductionFactor> GetByYearAsync(int year);
}
