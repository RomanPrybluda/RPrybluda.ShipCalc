using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain;

public class RatingThreshold
{
    public Guid Id { get; set; }

    public ShipType ShipType { get; set; }

    public int? LowerDeadweight { get; set; }

    public int? UpperDeadweight { get; set; }

    public decimal D1 { get; set; }

    public decimal D2 { get; set; }

    public decimal D3 { get; set; }

    public decimal D4 { get; set; }
}