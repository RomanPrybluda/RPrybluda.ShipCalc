namespace ShipCalc.Domain
{
    public class CarbonIntensityIndicatorRequiredCalculator
    {
        public double CalculateRequiredCII(double carbonIntensityIndicatorRef, int? year = null)
        {

            if (carbonIntensityIndicatorRef <= 0)
                throw new ArgumentException("CII_ref must be greater than zero.");

            int currentYear = 2025;
            int targetYear = year ?? currentYear;

            if (targetYear < 2023 || targetYear > 2030)
                throw new ArgumentException($"Year must be between 2023 and 2030. Provided year: {targetYear}");

            double z;

            if (targetYear <= 2026)
            {
                z = 5 + (targetYear - 2023) * 2;
            }
            else
            {
                throw new ArgumentException($"Reduction factor Z for year {targetYear} is not yet determined.");
            }

            return (100 - z) / 100.0 * carbonIntensityIndicatorRef;
        }
    }
}