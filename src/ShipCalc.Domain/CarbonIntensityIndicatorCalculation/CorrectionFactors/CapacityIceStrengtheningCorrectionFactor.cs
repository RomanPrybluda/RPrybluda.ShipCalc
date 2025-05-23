using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain
{
    public class CapacityIceStrengtheningCorrectionFactor
    {
        public Guid Id { get; set; }

        public IceClass IceClass { get; set; }

        public decimal ConstantA { get; set; }

        public decimal ConstantB { get; set; }
    }
}
