using ShipCalc.Domain.Calculation.CorrectionFactors;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;

public interface IIASuperAndIAIceCorrFactorRepo
{
    Task<IASuperAndIAIceCorrFactor> GetByIceClassAsync(IceClass iceClass, CancellationToken cancellationToken = default);
}
