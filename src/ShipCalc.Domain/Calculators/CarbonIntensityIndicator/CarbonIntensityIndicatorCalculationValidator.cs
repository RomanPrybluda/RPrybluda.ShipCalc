namespace ShipCalc.Domain
{
    public class CarbonIntensityIndicatorCalculationValidator
    {

        private const int MIN_GROSS_TONNAGE = 5_000;

        public virtual bool ValidateGrossTonnage(double grossTonnage)
        {
            if (grossTonnage >= MIN_GROSS_TONNAGE)
            {
                return true;
            }

            return false;

        }

    }
}
