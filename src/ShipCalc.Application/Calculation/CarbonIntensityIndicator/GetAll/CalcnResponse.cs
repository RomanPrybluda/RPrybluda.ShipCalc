using ShipCalc.Domain;
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


    public static CalcnResponse ToCalcnResponse(CarbonIntensityIndicatorCalculation calculation, Ship? relatedShip)
    {
        return new CalcnResponse
        {
            Id = calculation.Id,
            ShipName = relatedShip?.ShipName ?? string.Empty,
            ImoNumber = relatedShip?.ImoNumber ?? 0,
            ShipType = relatedShip?.ShipType ?? ShipType.NotApplicable,
            IceClass = relatedShip?.IceClass ?? IceClass.NotApplicable,

            RequiredCarbonIntensityIndicator = calculation.RequiredCarbonIntensityIndicator,
            AttainedCarbonIntensityIndicator = calculation.AttainedCarbonIntensityIndicator,
            CarbonIntensityIndicatorNumericalRating = calculation.CarbonIntensityIndicatorNumericalRating,
            CarbonIntensityIndicatorRating = calculation.CarbonIntensityIndicatorRating
        };
    }
}
