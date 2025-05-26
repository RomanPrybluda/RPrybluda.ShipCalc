using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain;

public class IASuperAndIAIceClassedShipCorrFactor
{
    public Guid Id { get; set; }

    public IceClass IceClass { get; set; }

    public decimal CorrectionFactor { get; set; }
}
