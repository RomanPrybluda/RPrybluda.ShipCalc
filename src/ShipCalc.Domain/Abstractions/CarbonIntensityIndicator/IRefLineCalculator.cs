namespace ShipCalc.Domain.Abstractions.CarbonIntensityIndicator;

public interface IRefLineCalculator
{
    decimal CalculateRefLine(decimal capacity, decimal parametrA, decimal parametrC);
}
