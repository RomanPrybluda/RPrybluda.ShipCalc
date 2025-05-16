namespace ShipCalc.Domain
{
    public class CarbonIntensityIndicatorAttainedCalculator
    {
        public double CalculateAttainedCII(Ship ship, double co2EmissionsInTon, double distanceTravelledInNM)
        {

            var capacityCalculator = new CapacityCalculator();
            var capacity = capacityCalculator
                .CalculateCapacity(
                ship.ShipType, ship.SummerDeadweight, ship.GrossTonnage);

            var cubicCapacityCorrFactorCalculator = new CubicCapacityCorrFactorCalculator();
            var cubicCapacityCorrectionFactor = cubicCapacityCorrFactorCalculator
                .CalculateCubicCapacityCorrectionFactor(
                ship.ShipType,
                ship.SummerDeadweight,
                capacity,
                ship.GrossTonnage);

            var iceClasedShipCapacityCorrFactorCalculator = new IceClasedShipCapacityCorrFactorCalculator();
            var iceClasedShipCapacityCorrFactor = iceClasedShipCapacityCorrFactorCalculator
                .CalculateIceClasedCapacityCorrectionFactor(
                ship.ShipType,
                ship.SummerDeadweight,
                ship.IceClass,
                ship.BlockCoefficient);

            var iASuperAndIAIceClassedShipCorrFactorCalculator = new IASuperAndIAIceClassedShipCorrFactorCalculator();
            var iASuperAndIAIceClassedShipCorrFactor = iASuperAndIAIceClassedShipCorrFactorCalculator
                .CalculateIASuperAndIAIceClassedShipCorrFactor(
                ship.IceClass);


            double attainedCII = co2EmissionsInTon /
                (capacity * distanceTravelledInNM *
                cubicCapacityCorrectionFactor *
                iceClasedShipCapacityCorrFactor *
                iASuperAndIAIceClassedShipCorrFactor);

            return attainedCII;
        }
    }
}