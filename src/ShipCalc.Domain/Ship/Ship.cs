using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain
{
    public sealed class Ship
    {
        public Guid Id { get; set; }

        public int ImoNumber { get; set; }

        public string ShipName { get; set; } = string.Empty;

        public decimal GrossTonnage { get; set; }

        public decimal SummerDeadweight { get; set; }

        public decimal BlockCoefficient { get; set; }

        public decimal CargoCompartmentCubicCapacity { get; set; }

        public ShipType ShipType { get; set; }

        public IceClass IceClass { get; set; }

    }
}
