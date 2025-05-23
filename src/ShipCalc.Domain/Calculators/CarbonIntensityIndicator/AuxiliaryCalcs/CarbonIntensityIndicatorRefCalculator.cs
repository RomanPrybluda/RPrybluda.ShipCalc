namespace ShipCalc.Domain
{
    public class CarbonIntensityIndicatorRefCalculator
    {
        public decimal CalculateCarbonIntensityIndicatorRef(decimal capacity, decimal parametrA, decimal parametrC)
        {
            var carbonIntensityIndicatorReference = parametrA * (decimal)Math.Pow((double)capacity, -(double)parametrC);
            return carbonIntensityIndicatorReference;
        }
    }
}
