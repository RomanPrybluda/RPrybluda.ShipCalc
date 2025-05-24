using ShipCalc.Domain.Abstractions;

namespace ShipCalc.Application.CarbonIntensityIndicatorCalculation
{
    public class CarbonIntensityIndicatorReferenceLineCalculator : ICarbonIntensityIndicatorReferenceLineCalculator
    {
        public decimal CalculateCarbonIntensityIndicatorReferenceLine(decimal capacity, decimal parametrA, decimal parametrC)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be greater than zero.", nameof(capacity));
            if (parametrA <= 0)
                throw new ArgumentException("Parameter A must be greater than zero.", nameof(parametrA));
            if (parametrC < 0)
                throw new ArgumentException("Parameter C cannot be negative.", nameof(parametrC));

            var carbonIntensityIndicatorReference = parametrA * (decimal)Math.Pow((double)capacity, -(double)parametrC);

            return carbonIntensityIndicatorReference;
        }
    }
}
