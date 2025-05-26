namespace ShipCalc.Domain;

public sealed class CarbonIntensityIndicatorCalcRecord
{
    public Guid Id { get; set; }

    public double CarbonIntensityIndicatorRef { get; set; }

    public double RequiredCarbonIntensityIndicator { get; set; }

    public double IceClasedShipCapacityCorrFactor { get; set; }

    public double CubicCapacityCorrectionFactor { get; set; }

    public double IASuperAndIAIceClassedShipCorrFactor { get; set; }

    public DateTime CalculationDate { get; set; } = DateTime.Now;

    public Guid ShipId { get; set; }

}
