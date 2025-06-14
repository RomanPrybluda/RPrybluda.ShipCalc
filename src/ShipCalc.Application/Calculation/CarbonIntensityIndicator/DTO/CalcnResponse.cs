using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Calculation.CarbonIntensityIndicator;

public class CalcnResponse
{
    public Guid Id { get; set; }

    public string ShipName { get; set; } = string.Empty;

    public int ImoNumber { get; set; }

    public ShipType ShipType { get; set; }

    public IceClass IceClass { get; set; }

    public decimal RequiredCarbonIntensityIndicator { get; set; }

    public decimal AttainedCarbonIntensityIndicator { get; set; }

    public decimal CarbonIntensityIndicatorNumericalRating { get; set; }

    public Rating CarbonIntensityIndicatorRating { get; set; }
}
