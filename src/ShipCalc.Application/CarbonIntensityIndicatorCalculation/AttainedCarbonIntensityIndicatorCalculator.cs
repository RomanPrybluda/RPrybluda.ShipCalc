using ShipCalc.Domain;
using ShipCalc.Domain.Abstractions;
using ShipCalc.Domain.Abstractions.CarbonIntensityIndicatorCalculation;

namespace ShipCalc.Application.CarbonIntensityIndicatorCalculation
{
    public class AttainedCarbonIntensityIndicatorCalculator : IAttainedCarbonIntensityIndicatorCalculator
    {
        private readonly IIceClasedShipCapacityCorrFactorCalculator _iceClasedShipCapacityCorrFactorCalculator;
        private readonly IIASuperAndIAIceClassedShipCorrFactorCalculator _iASuperAndIAIceClassedShipCorrFactorCalculator;

        public AttainedCarbonIntensityIndicatorCalculator(
            IIceClasedShipCapacityCorrFactorCalculator iceClasedShipCapacityCorrFactorCalculator,
            IIASuperAndIAIceClassedShipCorrFactorCalculator iASuperAndIAIceClassedShipCorrFactorCalculator)
        {
            _iceClasedShipCapacityCorrFactorCalculator = iceClasedShipCapacityCorrFactorCalculator
                ?? throw new ArgumentNullException(nameof(iceClasedShipCapacityCorrFactorCalculator));
            _iASuperAndIAIceClassedShipCorrFactorCalculator = iASuperAndIAIceClassedShipCorrFactorCalculator
                ?? throw new ArgumentNullException(nameof(iASuperAndIAIceClassedShipCorrFactorCalculator));
        }

        public async Task<decimal> CalculateAttainedCarbonIntensityIndicator(
            Ship ship,
            decimal capacity,
            decimal co2EmissionsInTons,
            decimal distanceTravelledInNMs)
        {
            if (ship == null)
                throw new ArgumentNullException(nameof(ship));
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be greater than zero.", nameof(capacity));
            if (co2EmissionsInTons < 0)
                throw new ArgumentException("CO2 emissions cannot be negative.", nameof(co2EmissionsInTons));
            if (distanceTravelledInNMs <= 0)
                throw new ArgumentException("Distance travelled must be greater than zero.", nameof(distanceTravelledInNMs));

            var iceClasedShipCapacityCorrFactor = await _iceClasedShipCapacityCorrFactorCalculator.CalculateIceClasedCapacityCorrectionFactor(
                ship.ShipType,
                ship.SummerDeadweight,
                ship.IceClass,
                ship.BlockCoefficient);

            var iASuperAndIAIceClassedShipCorrFactor = await _iASuperAndIAIceClassedShipCorrFactorCalculator.CalculateIASuperAndIAIceClassedShipCorrFactor(
                ship.IceClass);

            if (iceClasedShipCapacityCorrFactor <= 0)
                throw new ArgumentException("Ice-classed capacity correction factor must be greater than zero.");
            if (iASuperAndIAIceClassedShipCorrFactor <= 0)
                throw new ArgumentException("IA Super/IA ice-classed correction factor must be greater than zero.");

            decimal attainedCarbonIntensityIndicator = 1000000m * co2EmissionsInTons /
                (capacity * distanceTravelledInNMs * iceClasedShipCapacityCorrFactor * iASuperAndIAIceClassedShipCorrFactor);

            return attainedCarbonIntensityIndicator;
        }
    }
}