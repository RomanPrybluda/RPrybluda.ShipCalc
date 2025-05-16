namespace ShipCalc.Domain
{
    public class CarbonIntensityIndicatorRatingCalculator
    {
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

        public double Capacity { get; private set; }

        public double CarbonIntensityIndicatorRef { get; private set; }

        public double RequiredCarbonIntensityIndicator { get; private set; }

        public double AttainedCarbonIntensityIndicator { get; private set; }

        public double CarbonIntensityIndicatorNumericalRating { get; private set; }

        public CarbonIntensityIndicatorRating CarbonIntensityIndicatorRating { get; private set; }

        public void CalculateCarbonIntensityIndicatorRating(
            Ship ship, double co2EmissionsInTon, double distanceTravelledInNM, int year)
        {
            Capacity = _capacityCalculator.CalculateCapacity(
                ship.ShipType, ship.SummerDeadweight, ship.GrossTonnage);

            CarbonIntensityIndicatorRef = _refCalculator.CalculateCarbonIntensityIndicatorRef(ship, Capacity);

            RequiredCarbonIntensityIndicator = _requiredCalculator.CalculateRequiredCII(
                CarbonIntensityIndicatorRef, year);

            AttainedCarbonIntensityIndicator = _attainedCalculator.CalculateAttainedCarbonIntensityIndicator(
                ship, Capacity, co2EmissionsInTon, distanceTravelledInNM);

            if (RequiredCarbonIntensityIndicator == 0)
                throw new ArgumentException("Required CII cannot be zero.");

            var carbonIntensityIndicatorNumericaRating = AttainedCarbonIntensityIndicator / RequiredCarbonIntensityIndicator;

            CarbonIntensityIndicatorNumericalRating = carbonIntensityIndicatorNumericaRating;

            CarbonIntensityIndicatorRating = MapCiiRatingToLetterGrade(
                ship.ShipType, ship.SummerDeadweight, carbonIntensityIndicatorNumericaRating);
        }

        private CarbonIntensityIndicatorRating MapCiiRatingToLetterGrade(
            ShipType shipType,
            double deadWeight,
            double carbonIntensityIndicatorNumericaRating)
        {
            (double d1, double d2, double d3, double d4) = GetRatingThresholds(shipType, deadWeight);

            if (carbonIntensityIndicatorNumericaRating < d1) return CarbonIntensityIndicatorRating.A;
            if (carbonIntensityIndicatorNumericaRating < d2) return CarbonIntensityIndicatorRating.B;
            if (carbonIntensityIndicatorNumericaRating < d3) return CarbonIntensityIndicatorRating.C;
            if (carbonIntensityIndicatorNumericaRating < d4) return CarbonIntensityIndicatorRating.D;
            return CarbonIntensityIndicatorRating.E;
        }

        private (double d1, double d2, double d3, double d4) GetRatingThresholds(ShipType shipType, double deadWeight)
        {
            return shipType switch
            {
                ShipType.Tanker => (0.82, 0.93, 1.08, 1.28),
                ShipType.BulkCarrier => (0.86, 0.94, 1.06, 1.18),
                ShipType.CombinationCarrier => (0.87, 0.96, 1.06, 1.14),
                ShipType.ContainerShip => (0.83, 0.94, 1.07, 1.19),
                ShipType.GeneralCargoShip => (0.83, 0.94, 1.06, 1.19),
                ShipType.GasCarrier when deadWeight >= 65_000 => (0.81, 0.91, 1.12, 1.44),
                ShipType.GasCarrier => (0.85, 0.95, 1.06, 1.25),
                ShipType.LNGCarrier when deadWeight >= 100_000 => (0.89, 0.98, 1.06, 1.13),
                ShipType.LNGCarrier => (0.78, 0.92, 1.10, 1.37),
                ShipType.RoRoPassenger => (0.76, 0.89, 1.08, 1.27),
                ShipType.RoRoCargo => (0.76, 0.89, 1.08, 1.27),
                ShipType.RoRoCargoVehicle => (0.86, 0.94, 1.06, 1.16),
                ShipType.RefrigeratedCargoCarrier => (0.78, 0.91, 1.07, 1.20),
                ShipType.CruisePassengerShip => (0.87, 0.95, 1.06, 1.16),
                _ => throw new ArgumentException($"Unsupported ship type for CII rating: {shipType}")
            };
        }
    }
}