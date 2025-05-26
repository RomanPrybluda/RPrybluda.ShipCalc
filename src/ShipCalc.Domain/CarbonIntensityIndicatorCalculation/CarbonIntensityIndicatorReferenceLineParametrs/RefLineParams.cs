using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain;

public class RefLineParams
{
    public Guid Id { get; set; }

    public ShipType ShipType { get; set; }

    public decimal? LowerBound { get; set; }

    public decimal? UpperBound { get; set; }

    public decimal ParameterA { get; set; }

    public decimal ParameterC { get; set; }

}
