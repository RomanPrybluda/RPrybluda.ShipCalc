using ShipCalc.Domain.Abstractions.CarbonIntensityIndicator;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

namespace ShipCalc.Application.Calculators.CarbonIntensityIndicator;

public class CarbonIntensityIndicatorReferenceLineCalculator : IRefLineCalculator
{
    public decimal CalculateRefLine(decimal capacity, decimal parametrA, decimal parametrC)
    {
        if (capacity <= 0)
            throw new InvalidCapacityException(capacity);

        if (parametrA <= 0)
            throw new InvalidRefLineParameterAException(parametrA);

        if (parametrC < 0)
            throw new InvalidRefLineParameterCException(parametrC);

        var carbonIntensityIndicatorReference = parametrA * (decimal)Math.Pow((double)capacity, -(double)parametrC);

        return carbonIntensityIndicatorReference;
    }
}
