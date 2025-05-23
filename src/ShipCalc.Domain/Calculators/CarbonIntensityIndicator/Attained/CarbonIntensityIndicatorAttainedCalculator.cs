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

        public decimal CalculateAttainedCarbonIntensityIndicator(
            Ship ship,
            decimal capacity,
            decimal co2EmissionsInTon,
            decimal distanceTravelledInNM)
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

            decimal attainedCarbonIntensityIndicator = 1000000m * co2EmissionsInTon /
                (capacity *
                 distanceTravelledInNM *
                 iceClasedShipCapacityCorrFactor *
                 iASuperAndIAIceClassedShipCorrFactor);

            return attainedCarbonIntensityIndicator;
        }
    }
}