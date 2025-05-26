namespace ShipCalc.Domain.Abstractions;

public interface ICarbonIntensityIndicatorReferenceLineCalculator
{
    decimal CalculateCarbonIntensityIndicatorReferenceLine(decimal capacity, decimal parametrA, decimal parametrC);
}
