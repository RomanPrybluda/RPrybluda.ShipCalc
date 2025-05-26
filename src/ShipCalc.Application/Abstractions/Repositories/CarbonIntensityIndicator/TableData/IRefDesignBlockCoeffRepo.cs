using ShipCalc.Domain.Calculation.CorrectionFactors;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;

public interface IRefDesignBlockCoeffRepo
{
    Task<RefDesignBlockCoeff> GetByShipTypeAndDeadweightAsync(ShipType shipType, decimal deadWeight, CancellationToken cancellationToken = default);

}
