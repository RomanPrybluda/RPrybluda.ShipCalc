using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain
{
    public sealed class Ship
    {
        public Guid Id { get; set; }

        public int ImoNumber { get; set; }

        public string ShipName { get; set; } = string.Empty;

        public double GrossTonnage { get; set; }

        public double SummerDeadweight { get; set; }

        public double BlockCoefficient { get; set; }

        public double CargoCompartmentCubicCapacity { get; set; }

        public ShipType ShipType { get; set; }

        public IceClass IceClass { get; set; }

    }
}
