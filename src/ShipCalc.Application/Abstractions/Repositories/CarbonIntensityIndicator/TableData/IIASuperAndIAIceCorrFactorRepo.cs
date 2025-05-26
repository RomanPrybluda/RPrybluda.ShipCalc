using ShipCalc.Domain;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;

public interface IIASuperAndIAIceCorrFactorRepo
{
    Task<IASuperAndIAIceCorrFactor> GetByIceClassAsync(IceClass iceClass);
}
