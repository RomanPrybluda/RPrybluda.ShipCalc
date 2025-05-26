using ShipCalc.Application.Abstractions.Repositories;
using ShipCalc.Domain;
using ShipCalc.Domain.Abstractions;
using ShipCalc.Domain.Result;

namespace ShipCalc.Application.CarbonIntensityIndicatorCalculation;

public class CarbonIntensityIndicatorRatingCalculator : ICarbonIntensityIndicatorRatingCalculator
{

    private readonly ICapacityCalculator _capacityCalculator;
    private readonly IRequiredCarbonIntensityIndicatorCalculator _requiredCarbonIntensityIndicatorCalculator;
    private readonly IAttainedCarbonIntensityIndicatorCalculator _attainedCarbonIntensityIndicatorCalculator;
    private readonly ICarbonIntensityIndicatorRatingThresholdsRepository _carbonIntensityIndicatorRatingThresholdsRepository;

    public CarbonIntensityIndicatorRatingCalculator(
        ICapacityCalculator capacityCalculator,
        IRequiredCarbonIntensityIndicatorCalculator requiredCarbonIntensityIndicatorCalculator,
        IAttainedCarbonIntensityIndicatorCalculator attainedCarbonIntensityIndicatorCalculator,
        ICarbonIntensityIndicatorRatingThresholdsRepository carbonIntensityIndicatorRatingThresholdsRepository)
    {
        _capacityCalculator = capacityCalculator
            ?? throw new ArgumentNullException(nameof(capacityCalculator));

        _requiredCarbonIntensityIndicatorCalculator = requiredCarbonIntensityIndicatorCalculator
            ?? throw new ArgumentNullException(nameof(requiredCarbonIntensityIndicatorCalculator));

        _attainedCarbonIntensityIndicatorCalculator = attainedCarbonIntensityIndicatorCalculator
            ?? throw new ArgumentNullException(nameof(attainedCarbonIntensityIndicatorCalculator));

        _carbonIntensityIndicatorRatingThresholdsRepository = carbonIntensityIndicatorRatingThresholdsRepository;
    }

    public async Task<CarbonIntensityIndicatorCalcResult> CalculateRatingAsync(
        Ship ship,
        decimal co2EmissionsInTons,
        decimal distanceTravelledInNMs,
        int year)
    {
        if (ship == null)
            throw new ArgumentNullException(nameof(ship));
        if (co2EmissionsInTons < 0)
            throw new ArgumentException("CO2 emissions cannot be negative.", nameof(co2EmissionsInTons));
        if (distanceTravelledInNMs < 0)
            throw new ArgumentException("Distance travelled cannot be negative.", nameof(distanceTravelledInNMs));
        if (year < 0)
            throw new ArgumentException("Year cannot be negative.", nameof(year));

        var capacity = _capacityCalculator.CalculateCapacity(
            ship.ShipType,
            ship.SummerDeadweight,
            ship.GrossTonnage);

        var requiredCarbonIntensityIndicator = _requiredCarbonIntensityIndicatorCalculator
            .CalculateRequiredCarbonIntensityIndicator(
            ship.ShipType,
            capacity,
            year);

        var attainedCarbonIntensityIndicator = _attainedCarbonIntensityIndicatorCalculator
            .CalculateAttainedCarbonIntensityIndicator(
            ship,
            capacity,
            co2EmissionsInTons,
            distanceTravelledInNMs);

        var carbonIntensityIndicatorNumericalRating =
            _attainedCarbonIntensityIndicatorCalculator.IceClasedShipCapacityCorrFactor /
            _requiredCarbonIntensityIndicatorCalculator.RequiredCarbonIntensityIndicator;

        var carbonIntensityIndicatorRatingThresholds = await _carbonIntensityIndicatorRatingThresholdsRepository
            .GetThresholdsAsync(ship.ShipType, ship.SummerDeadweight);

        var carbonIntensityIndicatorRating = MapCiiRatingToLetterGrade(
            carbonIntensityIndicatorRatingThresholds,
            carbonIntensityIndicatorNumericalRating);

        var carbonIntensityIndicatorCalcResult = new CarbonIntensityIndicatorCalcResult
        {
            Capacity = capacity,

            ReferenceLineParameterA = _requiredCarbonIntensityIndicatorCalculator.RefLineParameterA,
            ReferenceLineParameterC = _requiredCarbonIntensityIndicatorCalculator.RefLineParameterC,
            ReferenceLine = _requiredCarbonIntensityIndicatorCalculator.RefLine,
            ReferenceLineReductionFactor = _requiredCarbonIntensityIndicatorCalculator.RefLineReductionFactor,
            RequiredCarbonIntensityIndicator = _requiredCarbonIntensityIndicatorCalculator.RequiredCarbonIntensityIndicator,

            IceClasedShipCapacityCorrFactor = _attainedCarbonIntensityIndicatorCalculator.IceClasedShipCapacityCorrFactor,
            IASuperAndIAIceClassedShipCorrFactor = _attainedCarbonIntensityIndicatorCalculator.IASuperAndIAIceClassedShipCorrFactor,
            AttainedCarbonIntensityIndicator = _attainedCarbonIntensityIndicatorCalculator.AttainedCarbonIntensityIndicator,

            CarbonIntensityIndicatorNumericalRating = carbonIntensityIndicatorNumericalRating,
            CarbonIntensityIndicatorRating = carbonIntensityIndicatorRating
        };

        return carbonIntensityIndicatorCalcResult;
    }

    private CarbonIntensityIndicatorRating MapCiiRatingToLetterGrade(
        CarbonIntensityIndicatorRatingThreshold carbonIntensityIndicatorRatingThresholds,
        decimal carbonIntensityIndicatorNumericalRating)
    {
        return carbonIntensityIndicatorNumericalRating switch
        {
            var rating when rating < carbonIntensityIndicatorRatingThresholds.D1 => CarbonIntensityIndicatorRating.A,
            var rating when rating < carbonIntensityIndicatorRatingThresholds.D2 => CarbonIntensityIndicatorRating.B,
            var rating when rating < carbonIntensityIndicatorRatingThresholds.D3 => CarbonIntensityIndicatorRating.C,
            var rating when rating < carbonIntensityIndicatorRatingThresholds.D4 => CarbonIntensityIndicatorRating.D,
            _ => CarbonIntensityIndicatorRating.E
        };
    }
}