using ShipCalc.Application.Abstractions;
using ShipCalc.Application.Abstractions.Repositories;
using ShipCalc.Domain.Abstractions;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.CarbonIntensityIndicatorCalculation;

public class RequiredCarbonIntensityIndicatorCalculator : IRequiredCarbonIntensityIndicatorCalculator
{
    public decimal RefLineParameterA { get; private set; }

    public decimal RefLineParameterC { get; private set; }

    public decimal RefLine { get; private set; }

    public int RefLineReductionFactor { get; private set; }

    public decimal RequiredCarbonIntensityIndicator { get; private set; }

    private readonly ICarbonIntensityIndicatorReferenceLineParameterRepository _refLineParametrs;
    private readonly IRequiredCarbonIntensityIndicatorReductionFactorRepository _reductionFactor;
    private readonly ICarbonIntensityIndicatorReferenceLineCalculator _referenceLine;

    public RequiredCarbonIntensityIndicatorCalculator(
        ICarbonIntensityIndicatorReferenceLineParameterRepository refLineParametrs,
        IRequiredCarbonIntensityIndicatorReductionFactorRepository reductionFactor,
        ICarbonIntensityIndicatorReferenceLineCalculator referenceLine)
    {
        _refLineParametrs = refLineParametrs ?? throw new ArgumentNullException(nameof(refLineParametrs));
        _reductionFactor = reductionFactor ?? throw new ArgumentNullException(nameof(reductionFactor));
        _referenceLine = referenceLine ?? throw new ArgumentNullException(nameof(referenceLine));
    }

    public async Task CalculateRequiredCarbonIntensityIndicator(
        ShipType shipType,
        decimal capacity,
        int year)
    {
        if (capacity < 0)
            throw new ArgumentException("Capacity cannot be negative.", nameof(capacity));
        if (year < 0)
            throw new ArgumentException("Year cannot be negative.", nameof(year));

        var parameters = await _refLineParametrs.GetParametersByShipTypeAndCapacityAsync(shipType, capacity)
            ?? throw new ArgumentException($"No reference line parameters found for ship type {shipType} and capacity {capacity}.");

        RefLineParameterA = parameters.ParameterA;
        RefLineParameterC = parameters.ParameterC;

        var carbonIntensityIndicatorRefLine = _referenceLine.CalculateCarbonIntensityIndicatorReferenceLine(
            capacity,
            RefLineParameterA,
            RefLineParameterC);

        if (carbonIntensityIndicatorRefLine <= 0)
            throw new ArgumentException("Carbon Intensity Indicator Reference Line must be greater than zero.");

        var referenceLineReductionFactor = await _reductionFactor.GetByYearAsync(year)
            ?? throw new ArgumentException($"Reduction factor for year {year} is not defined.");

        RefLine = carbonIntensityIndicatorRefLine;

        RefLineReductionFactor = referenceLineReductionFactor.ReductionFactorPercentage;

        RequiredCarbonIntensityIndicator = (100m - RefLineReductionFactor) / 100.0m * carbonIntensityIndicatorRefLine;
    }
}