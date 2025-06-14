using ShipCalc.Domain.Abstractions.CarbonIntensityIndicator;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Calculators.CarbonIntensityIndicator;

public class CapacityCalculator : ICapacityCalculator
{
    public decimal CalculateCapacity(ShipType shipType, decimal deadWeight, decimal grossTonnage)
    {
        if (deadWeight < 0)
            throw new InvalidDeadweightException();

        if (grossTonnage < 0)
            throw new InvalidGrossTonnageException();

        decimal capacity;

        if (shipType == ShipType.RoRoCargoVehicle ||
            shipType == ShipType.RoRoCargo ||
            shipType == ShipType.RoRoPassenger ||
            shipType == ShipType.RoRoPassengerCargo ||
            shipType == ShipType.RoRoPassengerCargoHighSpeedCraft ||
            shipType == ShipType.CruisePassengerShip)
        {
            capacity = grossTonnage;
            return capacity;
        }
        else
        {
            capacity = deadWeight;
            return capacity;
        }

        throw new UnsupportedShipTypeException(shipType);
    }
}
