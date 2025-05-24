using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain.Abstractions
{
    public interface ICapacityCalculator
    {
        Task<decimal> CalculateCapacity(ShipType shipType, decimal deadWeight, decimal grossTonnage);
    }
}
