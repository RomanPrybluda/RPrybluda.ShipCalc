using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain.Abstractions.CorrFactor;

public interface IIceClasedShipCapacityCorrFactorCalculator
{
    Task<decimal> CalculateIceClasedCapacityCorrectionFactor(
    ShipType shipType,
    decimal deadweight,
    IceClass iceClass,
    decimal blockCoefficient);
}
