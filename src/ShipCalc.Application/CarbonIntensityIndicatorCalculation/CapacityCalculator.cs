using ShipCalc.Domain.Abstractions;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.CarbonIntensityIndicatorCalculation
{
    public class CapacityCalculator : ICapacityCalculator
    {
        public decimal CalculateCapacity(ShipType shipType, decimal deadWeight, decimal grossTonnage)
        {
            if (deadWeight < 0)
                throw new ArgumentException("Deadweight cannot be negative.", nameof(deadWeight));
            if (grossTonnage < 0)
                throw new ArgumentOutOfRangeException(nameof(grossTonnage), "Gross tonnage cannot be negative.");

            decimal capacity;

            if (shipType == ShipType.Tanker ||
                shipType == ShipType.BulkCarrier ||
                shipType == ShipType.CombinationCarrier ||
                shipType == ShipType.ContainerShip ||
                shipType == ShipType.GeneralCargoShip ||
                shipType == ShipType.GasCarrier)
            {
                capacity = deadWeight;
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

            throw new ArgumentException($"Unsupported ship type: {shipType}", nameof(shipType));
        }
    }
}
