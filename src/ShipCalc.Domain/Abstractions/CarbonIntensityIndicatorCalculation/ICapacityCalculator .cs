using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain.Abstractions
{
    public interface ICapacityCalculator
    {
        decimal CalculateCapacity(ShipType shipType, decimal deadWeight, decimal grossTonnage);
    }
}
