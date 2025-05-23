using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain
{
    public class CarbonIntensityIndicatorReferenceLineParameters
    {
        public Guid Id { get; set; }

        public ShipType ShipType { get; set; }

        public decimal? LowerBound { get; set; }

        public decimal? UpperBound { get; set; }

        public decimal A { get; set; }

        public decimal C { get; set; }

        public bool Matches(decimal capacity)
        {
            return (!LowerBound.HasValue || capacity >= LowerBound) &&
                   (!UpperBound.HasValue || capacity < UpperBound);
        }
    }
}
