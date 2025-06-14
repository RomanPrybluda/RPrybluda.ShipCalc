using ShipCalc.Domain;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Calculation.CarbonIntensityIndicator;

public class CalcnByIdResponse
{
    public Guid Id { get; set; }

    public string ShipName { get; set; } = string.Empty;

    public int ImoNumber { get; set; }

    public ShipType ShipType { get; set; }

    public IceClass IceClass { get; set; }

    public decimal Co2EmissionsInTons { get; set; }

    public decimal DistanceTravelledInNMs { get; set; }

    public int Year { get; set; }

    public decimal Capacity { get; set; }

    public decimal RefLineParameterA { get; set; }

    public decimal RefLineParameterC { get; set; }

    public decimal RefLine { get; set; }

    public int RefLineReductionFactor { get; set; }

    public decimal RequiredCarbonIntensityIndicator { get; set; }

    public decimal IceClasedShipCapacityCorrFactor { get; set; }

    public decimal IASuperAndIAIceCorrFactor { get; set; }

    public decimal AttainedCarbonIntensityIndicator { get; set; }

    public decimal CarbonIntensityIndicatorNumericalRating { get; set; }

    public Rating CarbonIntensityIndicatorRating { get; set; }

    public DateTime CalculationDate { get; set; }

    public static CalcnByIdResponse ToCalcnByIdResponse(CarbonIntensityIndicatorCalculation calculation, Ship? ship)
    {
        return new CalcnByIdResponse
        {
            Id = calculation.Id,
            ShipName = ship?.ShipName ?? string.Empty,
            ImoNumber = ship?.ImoNumber ?? 0,
            ShipType = ship?.ShipType ?? ShipType.NotApplicable,
            IceClass = ship?.IceClass ?? IceClass.NotApplicable,

            Co2EmissionsInTons = calculation.Co2EmissionsInTons,
            DistanceTravelledInNMs = calculation.DistanceTravelledInNMs,
            Year = calculation.Year,
            Capacity = calculation.Capacity,
            RefLineParameterA = calculation.RefLineParameterA,
            RefLineParameterC = calculation.RefLineParameterC,
            RefLine = calculation.RefLine,
            RefLineReductionFactor = calculation.RefLineReductionFactor,
            RequiredCarbonIntensityIndicator = calculation.RequiredCarbonIntensityIndicator,
            IceClasedShipCapacityCorrFactor = calculation.IceClasedShipCapacityCorrFactor,
            IASuperAndIAIceCorrFactor = calculation.IASuperAndIAIceCorrFactor,
            AttainedCarbonIntensityIndicator = calculation.AttainedCarbonIntensityIndicator,
            CarbonIntensityIndicatorNumericalRating = calculation.CarbonIntensityIndicatorNumericalRating,
            CarbonIntensityIndicatorRating = calculation.CarbonIntensityIndicatorRating,
            CalculationDate = calculation.CalculationDate
        };
    }
}
