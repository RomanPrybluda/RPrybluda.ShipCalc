using ShipCalc.Domain.Abstractions.CarbonIntensityIndicator;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Calculators.CarbonIntensityIndicator;

public class CapacityCalculator : ICapacityCalculator
{
    public decimal CalculateCapacity(ShipType shipType, decimal deadWeight, decimal grossTonnage)
    {
        if (deadWeight < 0)
            throw new Domain.Calculations.CarbonIntensityIndicator.InvalidDeadweightException();

        if (grossTonnage < 0)
            throw new Domain.Calculations.CarbonIntensityIndicator.InvalidGrossTonnageException();

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

        throw new UnsupportedShipTypeException(shipType);
    }
}
