using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain;

public class IASuperAndIAIceCorrFactor
{
    public Guid Id { get; set; }

    public IceClass IceClass { get; set; }

    public decimal CorrectionFactor { get; set; }
}
