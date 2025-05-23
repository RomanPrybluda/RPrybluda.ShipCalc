namespace ShipCalc.Domain
{
    public class CarbonIntensityIndicatorAttainedCalculator
    {
        private readonly IceClasedShipCapacityCorrFactorCalculator _iceClasedShipCapacityCorrFactorCalculator;
        private readonly IASuperAndIAIceClassedShipCorrFactorCalculator _iASuperAndIAIceClassedShipCorrFactorCalculator;

        public CarbonIntensityIndicatorAttainedCalculator(
            IceClasedShipCapacityCorrFactorCalculator iceClasedShipCapacityCorrFactorCalculator,
            IASuperAndIAIceClassedShipCorrFactorCalculator iASuperAndIAIceClassedShipCorrFactorCalculator)
        {
            _iceClasedShipCapacityCorrFactorCalculator = iceClasedShipCapacityCorrFactorCalculator;
            _iASuperAndIAIceClassedShipCorrFactorCalculator = iASuperAndIAIceClassedShipCorrFactorCalculator;
        }

        public double CalculateAttainedCarbonIntensityIndicator(
            Ship ship,
            double capacity,
            double co2EmissionsInTon,
            double distanceTravelledInNM)
        {
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
                 iceClasedShipCapacityCorrFactor *
                 iASuperAndIAIceClassedShipCorrFactor);

            return attainedCarbonIntensityIndicator;
        }
    }
}