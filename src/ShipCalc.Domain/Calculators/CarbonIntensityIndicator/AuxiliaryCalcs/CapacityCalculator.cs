using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain
{
    public class CapacityCalculator
    {
        public double CalculateCapacity(ShipType shipType, double deadWeigth, double grossTonnage)
        {
            double capacity;

            if (shipType == ShipType.Tanker ||
                shipType == ShipType.BulkCarrier ||
                shipType == ShipType.CombinationCarrier ||
                shipType == ShipType.ContainerShip ||
                shipType == ShipType.GeneralCargoShip ||
                shipType == ShipType.GasCarrier)
            {
                capacity = deadWeigth;
                return capacity;
            }

            if (shipType == ShipType.RoRoCargoVehicle ||
                shipType == ShipType.RoRoCargo ||
                shipType == ShipType.RoRoPassenger ||
                shipType == ShipType.RoRoPassengerCargoHighSpeedCraft ||
                shipType == ShipType.CruisePassengerShip)
            {
                capacity = grossTonnage;
                return capacity;
            }

            throw new ArgumentException($"Unsupported ship type: {shipType}");

        }
    }
}
