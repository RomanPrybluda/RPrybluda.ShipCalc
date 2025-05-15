namespace ShipCalc.Domain
{
    public class CarbonIntensityIndicatorAttainedCalculator
    {
        public double CalculateAttainedCII(Ship ship, double co2EmissionsInTon, double distanceTravelledInNM)
        {

            var capacityCalculator = new CapacityCalculator();
            var capacity = capacityCalculator.CalculateCapacity(ship.ShipType, ship.SummerDeadweight, ship.GrossTonnage);

            double attainedCII = co2EmissionsInTon / (capacity * distanceTravelledInNM);

            return attainedCII;
        }
    }
}