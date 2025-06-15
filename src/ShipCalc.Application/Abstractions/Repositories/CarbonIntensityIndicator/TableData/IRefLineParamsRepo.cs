using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;

public interface IRefLineParamsRepo : IRepository
{
    Task<RefLineParams> GetByShipTypeAndCapacityAsync(ShipType shipType, decimal capacity, CancellationToken cancellationToken = default);
}
