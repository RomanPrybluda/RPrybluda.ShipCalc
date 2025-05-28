using ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;
using ShipCalc.Domain;
using ShipCalc.Domain.Abstractions.CarbonIntensityIndicator;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

namespace ShipCalc.Application.Calculators.CarbonIntensityIndicator;

public class CarbonIntensityIndicatorRatingCalculator : IRatingCalculator
{

    private readonly ICapacityCalculator _capacityCalculator;
    private readonly IRequiredCarbonIntensityIndicatorCalculator _requiredCarbonIntensityIndicatorCalculator;
    private readonly IAttainedCarbonIntensityIndicatorCalculator _attainedCarbonIntensityIndicatorCalculator;
    private readonly IRatingThresholdsRepo _carbonIntensityIndicatorRatingThresholdsRepository;

    public CarbonIntensityIndicatorRatingCalculator(
        ICapacityCalculator capacityCalculator,
        IRequiredCarbonIntensityIndicatorCalculator requiredCarbonIntensityIndicatorCalculator,
        IAttainedCarbonIntensityIndicatorCalculator attainedCarbonIntensityIndicatorCalculator,
        IRatingThresholdsRepo carbonIntensityIndicatorRatingThresholdsRepo)
    {
        _capacityCalculator = capacityCalculator;

        _requiredCarbonIntensityIndicatorCalculator = requiredCarbonIntensityIndicatorCalculator;

        _attainedCarbonIntensityIndicatorCalculator = attainedCarbonIntensityIndicatorCalculator;

        _carbonIntensityIndicatorRatingThresholdsRepository = carbonIntensityIndicatorRatingThresholdsRepo;
    }

    public async Task<CarbonIntensityIndicatorCalculation> CalculateRatingAsync(
        Ship ship,
        decimal co2EmissionsInTons,
        decimal distanceTravelledInNMs,
        int year)
    {
        if (ship == null)
            throw new InvalidShipException();

        if (co2EmissionsInTons < 0)
            throw new NegativeCo2EmissionsException();

        if (distanceTravelledInNMs < 0)
            throw new NegativeDistanceTravelledException();

        if (year < 0)
            throw new NegativeYearException();

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

        var carbonIntensityIndicatorCalcResult = new CarbonIntensityIndicatorCalculation
        {
            Co2EmissionsInTons = co2EmissionsInTons,
            DistanceTravelledInNMs = distanceTravelledInNMs,
            Year = year,

            Capacity = capacity,

            RefLineParameterA = _requiredCarbonIntensityIndicatorCalculator.RefLineParameterA,
            RefLineParameterC = _requiredCarbonIntensityIndicatorCalculator.RefLineParameterC,
            RefLine = _requiredCarbonIntensityIndicatorCalculator.RefLine,
            RefLineReductionFactor = _requiredCarbonIntensityIndicatorCalculator.RefLineReductionFactor,
            RequiredCarbonIntensityIndicator = _requiredCarbonIntensityIndicatorCalculator.RequiredCarbonIntensityIndicator,

            IceClasedShipCapacityCorrFactor = _attainedCarbonIntensityIndicatorCalculator.IceClasedShipCapacityCorrFactor,
            IASuperAndIAIceCorrFactor = _attainedCarbonIntensityIndicatorCalculator.IASuperAndIAIceClassedShipCorrFactor,
            AttainedCarbonIntensityIndicator = _attainedCarbonIntensityIndicatorCalculator.AttainedCarbonIntensityIndicator,

            CarbonIntensityIndicatorNumericalRating = carbonIntensityIndicatorNumericalRating,
            CarbonIntensityIndicatorRating = carbonIntensityIndicatorRating
        };

        return carbonIntensityIndicatorCalcResult;
    }

    private Rating MapCiiRatingToLetterGrade(
        RatingThreshold carbonIntensityIndicatorRatingThresholds,
        decimal carbonIntensityIndicatorNumericalRating)
    {
        return carbonIntensityIndicatorNumericalRating switch
        {
            var rating when rating < carbonIntensityIndicatorRatingThresholds.D1 => Rating.A,
            var rating when rating < carbonIntensityIndicatorRatingThresholds.D2 => Rating.B,
            var rating when rating < carbonIntensityIndicatorRatingThresholds.D3 => Rating.C,
            var rating when rating < carbonIntensityIndicatorRatingThresholds.D4 => Rating.D,
            _ => Rating.E
        };
    }
}