namespace ShipCalc.Domain
{
    public class CarbonIntensityIndicatorRefCalculator
    {
        public double CalculateCarbonIntensityIndicatorRef(double capacity, double parametrA, double parametrC)
        {
            var carbonIntensityIndicatorReference = parametrA * Math.Pow(capacity, -parametrC);
            return carbonIntensityIndicatorReference;
        }
    }
}
