using ShipCalc.Domain.Abstractions.CarbonIntensityIndicator;

namespace ShipCalc.Application.Calculations.CarbonIntensityIndicator;

public class CarbonIntensityIndicatorReferenceLineCalculator : IRefLineCalculator
{
    public decimal CalculateRefLine(decimal capacity, decimal parametrA, decimal parametrC)
    {
        if (capacity <= 0)
            throw new ArgumentException("Capacity must be greater than zero.", nameof(capacity));

        if (parametrA <= 0)
            throw new ArgumentException("Parameter RefLineParameterA must be greater than zero.", nameof(parametrA));

        if (parametrC < 0)
            throw new ArgumentException("Parameter RefLineParameterC cannot be negative.", nameof(parametrC));

        var carbonIntensityIndicatorReference = parametrA * (decimal)Math.Pow((double)capacity, -(double)parametrC);

        return carbonIntensityIndicatorReference;
    }
}
