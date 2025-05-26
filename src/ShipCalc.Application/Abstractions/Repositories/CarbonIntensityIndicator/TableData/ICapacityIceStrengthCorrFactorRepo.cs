using ShipCalc.Domain;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;

public interface ICapacityIceStrengthCorrFactorRepo
{
    Task<CapacityIceStrengthCorrFactor> GetByIceClassAsync(IceClass iceClass);

}
