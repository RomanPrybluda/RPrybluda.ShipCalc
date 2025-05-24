using ShipCalc.Domain;
using ShipCalc.Domain.Abstractions;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.CarbonIntensityIndicatorCalculation
{
    public class CarbonIntensityIndicatorRatingCalculator : ICarbonIntensityIndicatorRatingCalculator
    {
        public decimal Capacity { get; private set; }

        public decimal ParametrA { get; private set; }

        public decimal ParametrB { get; private set; }

        public decimal CarbonIntensityIndicatorRef { get; private set; }

        public decimal RequiredCarbonIntensityIndicator { get; private set; }

        public decimal AttainedCarbonIntensityIndicator { get; private set; }

        public decimal CarbonIntensityIndicatorNumericalRating { get; private set; }

        public CarbonIntensityIndicatorRating CarbonIntensityIndicatorRating { get; private set; }

        private readonly CapacityCalculator _capacityCalculator;
        private readonly CarbonIntensityIndicatorRefCalculator _refCalculator;
        private readonly CarbonIntensityIndicatorRequiredCalculator _requiredCalculator;
        private readonly CarbonIntensityIndicatorAttainedCalculator _attainedCalculator;

        public CarbonIntensityIndicatorRatingCalculator(
            CapacityCalculator capacityCalculator,
            CarbonIntensityIndicatorRefCalculator refCalculator,
            CarbonIntensityIndicatorRequiredCalculator requiredCalculator,
            CarbonIntensityIndicatorAttainedCalculator attainedCalculator)
        {
            _capacityCalculator = capacityCalculator ?? throw new ArgumentNullException(nameof(capacityCalculator));
            _refCalculator = refCalculator ?? throw new ArgumentNullException(nameof(refCalculator));
            _requiredCalculator = requiredCalculator ?? throw new ArgumentNullException(nameof(requiredCalculator));
            _attainedCalculator = attainedCalculator ?? throw new ArgumentNullException(nameof(attainedCalculator));
        }

        public async Task CalculateRatingAsync(
            Ship ship,
            CarbonIntensityIndicatorReferenceLineParameter carbonIntensityIndicatorRefParameters,
            CarbonIntensityIndicatorRatingThreshold carbonIntensityIndicatorRatingThresholds,
            decimal co2EmissionsInTons,
            decimal distanceTravelledInNMs,
            int year)
        {
            if (ship == null)
                throw new ArgumentNullException(nameof(ship));
            if (carbonIntensityIndicatorRefParameters == null)
                throw new ArgumentNullException(nameof(carbonIntensityIndicatorRefParameters));
            if (carbonIntensityIndicatorRatingThresholds == null)
                throw new ArgumentNullException(nameof(carbonIntensityIndicatorRatingThresholds));
            if (co2EmissionsInTons < 0)
                throw new ArgumentException("CO2 emissions cannot be negative.", nameof(co2EmissionsInTons));
            if (distanceTravelledInNMs < 0)
                throw new ArgumentException("Distance travelled cannot be negative.", nameof(distanceTravelledInNMs));
            if (year < 0)
                throw new ArgumentException("Year cannot be negative.", nameof(year));

            Capacity = await Task.Run(() => _capacityCalculator.CalculateCapacity(
                ship.ShipType,
                ship.SummerDeadweight,
                ship.GrossTonnage));

            ParametrA = carbonIntensityIndicatorRefParameters.A;
            ParametrB = carbonIntensityIndicatorRefParameters.C;

            CarbonIntensityIndicatorRef = await Task.Run(() => _refCalculator.CalculateCarbonIntensityIndicatorRef(
                Capacity,
                carbonIntensityIndicatorRefParameters.A,
                carbonIntensityIndicatorRefParameters.C));

            RequiredCarbonIntensityIndicator = await Task.Run(() => _requiredCalculator.CalculateRequiredCarbonIntensityIndicator(
                CarbonIntensityIndicatorRef, year));

            AttainedCarbonIntensityIndicator = await Task.Run(() => _attainedCalculator.CalculateAttainedCarbonIntensityIndicator(
                ship, Capacity, co2EmissionsInTons, distanceTravelledInNMs));

            if (RequiredCarbonIntensityIndicator == 0)
                throw new ArgumentException("Required CII cannot be zero.");

            CarbonIntensityIndicatorNumericalRating = AttainedCarbonIntensityIndicator / RequiredCarbonIntensityIndicator;

            CarbonIntensityIndicatorRating = await Task.Run(() => MapCiiRatingToLetterGrade(
                carbonIntensityIndicatorRatingThresholds,
                CarbonIntensityIndicatorNumericalRating));
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
}
