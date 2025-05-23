using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain.Abstractions
{
    public interface IIceClasedShipCapacityCorrFactorCalculator
    {
        decimal CalculateIceClasedCapacityCorrectionFactor(
        ShipType shipType,
        decimal deadweight,
        IceClass iceClass,
        decimal blockCoefficient);
    }
}
