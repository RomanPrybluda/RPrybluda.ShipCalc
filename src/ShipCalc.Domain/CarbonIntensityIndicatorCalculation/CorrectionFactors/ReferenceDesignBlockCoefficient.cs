using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain
{
    public class ReferenceDesignBlockCoefficient
    {
        public Guid Id { get; set; }

        public ShipType ShipType { get; set; }

        public int? MinDeadweight { get; set; }

        public int? MaxDeadweight { get; set; }

        public decimal BlockCoefficient { get; set; }
    }
}
