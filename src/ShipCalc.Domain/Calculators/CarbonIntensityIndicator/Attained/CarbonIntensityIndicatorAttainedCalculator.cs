namespace ShipCalc.Domain
{
    public class CarbonIntensityIndicatorAttainedCalculator
    {
        private readonly CubicCapacityCorrFactorCalculator _cubicCapacityCorrFactorCalculator;
        private readonly IceClasedShipCapacityCorrFactorCalculator _iceClasedShipCapacityCorrFactorCalculator;
        private readonly IASuperAndIAIceClassedShipCorrFactorCalculator _iASuperAndIAIceClassedShipCorrFactorCalculator;

        public CarbonIntensityIndicatorAttainedCalculator(
            CubicCapacityCorrFactorCalculator cubicCapacityCorrFactorCalculator,
            IceClasedShipCapacityCorrFactorCalculator iceClasedShipCapacityCorrFactorCalculator,
            IASuperAndIAIceClassedShipCorrFactorCalculator iASuperAndIAIceClassedShipCorrFactorCalculator)
        {
            _cubicCapacityCorrFactorCalculator = cubicCapacityCorrFactorCalculator;
            _iceClasedShipCapacityCorrFactorCalculator = iceClasedShipCapacityCorrFactorCalculator;
            _iASuperAndIAIceClassedShipCorrFactorCalculator = iASuperAndIAIceClassedShipCorrFactorCalculator;
        }

        public double CalculateAttainedCarbonIntensityIndicator(
            Ship ship,
            double capacity,
            double co2EmissionsInTon,
            double distanceTravelledInNM)
        {
            var cubicCapacityCorrectionFactor = _cubicCapacityCorrFactorCalculator
                .CalculateCubicCapacityCorrectionFactor(
                    ship.ShipType,
                    ship.SummerDeadweight,
                    capacity,
                    ship.GrossTonnage);

            var iceClasedShipCapacityCorrFactor = _iceClasedShipCapacityCorrFactorCalculator
                .CalculateIceClasedCapacityCorrectionFactor(
                    ship.ShipType,
                    ship.SummerDeadweight,
                    ship.IceClass,
                    ship.BlockCoefficient);

            var iASuperAndIAIceClassedShipCorrFactor = _iASuperAndIAIceClassedShipCorrFactorCalculator
                .CalculateIASuperAndIAIceClassedShipCorrFactor(
                    ship.IceClass);

            double attainedCarbonIntensityIndicator = 1000000 * co2EmissionsInTon /
                (capacity *
                 distanceTravelledInNM *
                 cubicCapacityCorrectionFactor *
                 iceClasedShipCapacityCorrFactor *
                 iASuperAndIAIceClassedShipCorrFactor);

            return attainedCarbonIntensityIndicator;
        }
    }
}