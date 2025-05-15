namespace ShipCalc.Domain
{
    public class CarbonIntensityIndicatorRatingCalculator
    {
        public (double rating, CarbonIntensityIndicatorRating letterGrade) CalculateCarbonIntensityIndicatorRating(
            Ship ship, double co2EmissionsInTon,
            double distanceTravelledInNM,
            int year)
        {
            var carbonIntensityIndicatorCalculationValidator = new CarbonIntensityIndicatorCalculationValidator();
            var isValidGrossTonnage = carbonIntensityIndicatorCalculationValidator.ValidateGrossTonnage(ship.GrossTonnage);

            if (!isValidGrossTonnage)
            {
                return (0.0, CarbonIntensityIndicatorRating.E); // Default to E for invalid cases
            }

            // Step 1: Calculate CII_ref
            var carbonIntensityIndicatorRefCalculator = new CarbonIntensityIndicatorRefCalculator();
            double carbonIntensityIndicatorRef = carbonIntensityIndicatorRefCalculator.CalculateCarbonIntensityIndicatorRef(ship);

            // Step 2: Calculate CII Required
            var carbonIntensityIndicatorRequiredCalculator = new CarbonIntensityIndicatorRequiredCalculator();
            double requiredCarbonIntensityIndicator = carbonIntensityIndicatorRequiredCalculator.CalculateRequiredCII(carbonIntensityIndicatorRef, year);

            // Step 3: Calculate CII Attained
            var carbonIntensityIndicatorAttainedCalculator = new CarbonIntensityIndicatorAttainedCalculator();
            double attainedCarbonIntensityIndicator = carbonIntensityIndicatorAttainedCalculator.CalculateAttainedCII(ship, co2EmissionsInTon, distanceTravelledInNM);

            // Step 4: Calculate CII Rating (numerical)
            if (requiredCarbonIntensityIndicator == 0)
                throw new ArgumentException("Required CII cannot be zero.");
            double carbonIntensityIndicatorRating = attainedCarbonIntensityIndicator / requiredCarbonIntensityIndicator;

            // Step 5: Map CII Rating to Letter Grade
            CarbonIntensityIndicatorRating letterGrade = MapCiiRatingToLetterGrade(
                ship.ShipType,
                ship.SummerDeadweight,
                carbonIntensityIndicatorRating);

            return (carbonIntensityIndicatorRating, letterGrade);
        }

        private CarbonIntensityIndicatorRating MapCiiRatingToLetterGrade(
            ShipType shipType,
            double deadWeight,
            double carbonIntensityIndicatorRating)
        {
            (double d1, double d2, double d3, double d4) = GetRatingThresholds(shipType, deadWeight);

            if (carbonIntensityIndicatorRating < d1) return CarbonIntensityIndicatorRating.A;
            if (carbonIntensityIndicatorRating < d2) return CarbonIntensityIndicatorRating.B;
            if (carbonIntensityIndicatorRating < d3) return CarbonIntensityIndicatorRating.C;
            if (carbonIntensityIndicatorRating < d4) return CarbonIntensityIndicatorRating.D;
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