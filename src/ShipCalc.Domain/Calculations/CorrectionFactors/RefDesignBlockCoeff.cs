using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain.Calculation.CorrectionFactors;

public class RefDesignBlockCoeff
{
    public Guid Id { get; set; }

    public ShipType ShipType { get; set; }

    public int? MinDeadweight { get; set; }

    public int? MaxDeadweight { get; set; }

    public decimal BlockCoefficient { get; set; }
}
