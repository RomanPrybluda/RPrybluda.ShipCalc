using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain.Abstractions;

public interface IRequiredCarbonIntensityIndicatorCalculator
{
    decimal RefLineParameterA { get; }

    decimal RefLineParameterC { get; }

    public int RefLineReductionFactor { get; }

    public decimal RefLine { get; }

    public decimal RequiredCarbonIntensityIndicator { get; }

    Task CalculateRequiredCarbonIntensityIndicator(ShipType shipType, decimal capacity, int year);
}
