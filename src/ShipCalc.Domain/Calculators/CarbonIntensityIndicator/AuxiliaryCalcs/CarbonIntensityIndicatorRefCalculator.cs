namespace ShipCalc.Domain
{
    public class CarbonIntensityIndicatorRefCalculator
    {

        public double CalculateCarbonIntensityIndicatorRef(Ship ship, double capacity)
        {
            var (a, c) = GetACParameters(ship.ShipType, ship.SummerDeadweight, ship.GrossTonnage);
            var carbonIntensityIndicatorReference = a * Math.Pow(capacity, -c);

            return carbonIntensityIndicatorReference;
        }

        public (double a, double c) GetACParameters(ShipType shipType, double deadWeight, double grossTonnage)
        {
            double capacity = (shipType == ShipType.RoRoCargoVehicle ||
                              shipType == ShipType.RoRoCargo ||
                              shipType == ShipType.RoRoPassenger ||
                              shipType == ShipType.RoRoPassengerCargoHighSpeedCraft ||
                              shipType == ShipType.CruisePassengerShip) ? grossTonnage : deadWeight;

            if (shipType == ShipType.BulkCarrier)
            {
                if (deadWeight >= 279_000)
                    return (a: 4745, c: 0.622);
                return (a: 4745, c: 0.622);
            }

            if (shipType == ShipType.GasCarrier)
            {
                if (deadWeight >= 65_000)
                    return (a: 14405E+7, c: 2.071);
                if (deadWeight < 65_000)
                    return (a: 8104, c: 0.639);
            }

            if (shipType == ShipType.Tanker)
            {
                return (a: 5247, c: 0.610);
            }

            if (shipType == ShipType.ContainerShip)
            {
                return (a: 1984, c: 0.489);
            }

            if (shipType == ShipType.GeneralCargoShip)
            {
                if (deadWeight >= 20_000)
                    return (a: 31948, c: 0.792);
                if (deadWeight < 20_000)
                    return (a: 588, c: 0.3885);
            }

            if (shipType == ShipType.RefrigeratedCargoCarrier)
            {
                return (a: 4600, c: 0.557);
            }

            if (shipType == ShipType.CombinationCarrier)
            {
                return (a: 5119, c: 0.622);
            }

            if (shipType == ShipType.LNGCarrier)
            {
                if (deadWeight >= 100_000)
                    return (a: 9827, c: 0);
                if (deadWeight >= 65_000 && deadWeight < 100_000)
                    return (a: 14479E+10, c: 2.673);
                if (deadWeight < 65_000)
                    return (a: 14479E+10, c: 2.673);
            }

            if (shipType == ShipType.RoRoCargoVehicle || shipType == ShipType.RoRoCargo || shipType == ShipType.RoRoPassenger || shipType == ShipType.RoRoPassengerCargoHighSpeedCraft)
            {
                if (grossTonnage >= 57_700)
                    return (a: 3627, c: 0.590);
                if (grossTonnage >= 30_000 && grossTonnage < 57_700)
                    return (a: 3627, c: 0.590);
                if (grossTonnage < 30_000)
                    return (a: 330, c: 0.329);
            }

            if (shipType == ShipType.CruisePassengerShip)
            {
                return (a: 930, c: 0.383);
            }

            throw new ArgumentException($"Unsupported ship type or invalid capacity: {shipType}, DeadWeight: {deadWeight}, GrossTonnage: {grossTonnage}");
        }

    }
}
