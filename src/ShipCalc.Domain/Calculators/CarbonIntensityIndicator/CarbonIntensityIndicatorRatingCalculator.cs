using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain
{
    public class CarbonIntensityIndicatorRatingCalculator
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

        public void CalculateCarbonIntensityIndicatorRatingAsync(
            Ship ship,
            CarbonIntensityIndicatorReferenceLineParameter carbonIntensityIndicatorRefParameters,
            CarbonIntensityIndicatorRatingThreshold carbonIntensityIndicatorRatingThresholds,
            decimal co2EmissionsInTon,
            decimal distanceTravelledInNM,
            int year)
        {
            Capacity = _capacityCalculator.CalculateCapacity(
                ship.ShipType,
                ship.SummerDeadweight,
                ship.GrossTonnage);

            CarbonIntensityIndicatorRef = _refCalculator.CalculateCarbonIntensityIndicatorRef(
                Capacity,
                carbonIntensityIndicatorRefParameters.A,
                carbonIntensityIndicatorRefParameters.C);

            RequiredCarbonIntensityIndicator = _requiredCalculator.CalculateRequiredCarbonIntensityIndicator(
                CarbonIntensityIndicatorRef, year);

            AttainedCarbonIntensityIndicator = _attainedCalculator.CalculateAttainedCarbonIntensityIndicator(
                ship, Capacity, co2EmissionsInTon, distanceTravelledInNM);

            if (RequiredCarbonIntensityIndicator == 0)
                throw new ArgumentException("Required CII cannot be zero.");

            CarbonIntensityIndicatorNumericalRating = AttainedCarbonIntensityIndicator / RequiredCarbonIntensityIndicator;

            CarbonIntensityIndicatorRating = MapCiiRatingToLetterGrade(carbonIntensityIndicatorRatingThresholds, CarbonIntensityIndicatorNumericalRating);
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