using ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;
using ShipCalc.Domain.Abstractions.CarbonIntensityIndicator;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Calculators.CarbonIntensityIndicator;

public class RequiredCarbonIntensityIndicatorCalculator : IRequiredCarbonIntensityIndicatorCalculator
{
    private const decimal ONE_HUNDRED_PERCENT = 100m;

    public decimal RefLineParameterA { get; private set; }

    public decimal RefLineParameterC { get; private set; }

    public decimal RefLine { get; private set; }

    public int RefLineReductionFactor { get; private set; }

    public decimal RequiredCarbonIntensityIndicator { get; private set; }

    private readonly IRefLineParamsRepo _refLineParametrs;
    private readonly IReductionFactorRepo _reductionFactor;
    private readonly IRefLineCalculator _referenceLine;

    public RequiredCarbonIntensityIndicatorCalculator(
        IRefLineParamsRepo refLineParametrs,
        IReductionFactorRepo reductionFactor,
        IRefLineCalculator referenceLine)
    {
        _refLineParametrs = refLineParametrs;
        _reductionFactor = reductionFactor;
        _referenceLine = referenceLine;
    }

    public async Task CalculateRequiredCarbonIntensityIndicator(
        ShipType shipType,
        decimal capacity,
        int year)
    {
        if (capacity < 0)
            throw new InvalidCapacityException(capacity);
        if (year < 0)
            throw new InvalidYearException(year);

        var parameters = await _refLineParametrs.GetByShipTypeAndCapacityAsync(shipType, capacity)
            ?? throw new RefLineParametersNotFoundException(shipType, capacity);

        RefLineParameterA = parameters.ParameterA;
        RefLineParameterC = parameters.ParameterC;

        var carbonIntensityIndicatorRefLine = _referenceLine.CalculateRefLine(
            capacity,
            RefLineParameterA,
            RefLineParameterC);

        if (carbonIntensityIndicatorRefLine <= 0)
            throw new ArgumentException("Carbon Intensity Indicator Reference Line must be greater than zero.");

        var referenceLineReductionFactor = await _reductionFactor.GetByYearAsync(year)
            ?? throw new ReductionFactorNotFoundException(year);

        RefLine = carbonIntensityIndicatorRefLine;

        RefLineReductionFactor = referenceLineReductionFactor.ReductionFactorPercentage;

        RequiredCarbonIntensityIndicator =
            (ONE_HUNDRED_PERCENT - RefLineReductionFactor) / ONE_HUNDRED_PERCENT * carbonIntensityIndicatorRefLine;
    }
}