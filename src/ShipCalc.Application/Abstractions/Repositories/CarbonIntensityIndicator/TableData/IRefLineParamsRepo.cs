using ShipCalc.Domain;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;

public interface IRefLineParamsRepo
{
    Task<RefLineParams> GetByShipTypeAndCapacityAsync(ShipType shipType, decimal capacity);
}
