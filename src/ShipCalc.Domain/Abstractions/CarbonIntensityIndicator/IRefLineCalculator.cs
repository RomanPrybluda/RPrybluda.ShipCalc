namespace ShipCalc.Domain.Abstractions.CarbonIntensityIndicator;

public interface IRefLineCalculator : ICalculator
{
    decimal CalculateRefLine(decimal capacity, decimal parametrA, decimal parametrC);
}
