using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain
{
    public class CarbonIntensityIndicatorRefParameters
    {
        public Guid Id { get; set; }

        public ShipType ShipType { get; set; }

        public double? LowerBound { get; set; }

        public double? UpperBound { get; set; }

        public double A { get; set; }

        public double C { get; set; }

        public bool Matches(double capacity)
        {
            return (!LowerBound.HasValue || capacity >= LowerBound) &&
                   (!UpperBound.HasValue || capacity < UpperBound);
        }
    }
}
