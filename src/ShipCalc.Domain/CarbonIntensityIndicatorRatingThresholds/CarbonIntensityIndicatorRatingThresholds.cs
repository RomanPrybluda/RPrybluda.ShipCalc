using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain
{
    public class CarbonIntensityIndicatorRatingThresholds
    {
        public Guid Id { get; set; }

        public ShipType ShipType { get; set; }

        public double? LowerDeadweight { get; set; }

        public double? UpperDeadweight { get; set; }

        public double D1 { get; set; }

        public double D2 { get; set; }

        public double D3 { get; set; }

        public double D4 { get; set; }

        public bool Matches(ShipType shipType, double deadWeight)
        {
            return ShipType == shipType &&
                   (!LowerDeadweight.HasValue || deadWeight >= LowerDeadweight) &&
                   (!UpperDeadweight.HasValue || deadWeight < UpperDeadweight);
        }
    }
}