using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain.Abstractions.CarbonIntensityIndicatorCalculation
{
    public interface IIASuperAndIAIceClassedShipCorrFactorCalculator
    {
        Task<decimal> CalculateIASuperAndIAIceClassedShipCorrFactor(IceClass? iceClass);
    }
}
