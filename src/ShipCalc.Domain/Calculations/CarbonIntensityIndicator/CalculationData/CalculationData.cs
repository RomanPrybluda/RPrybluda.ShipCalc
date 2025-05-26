namespace ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

public class CalculationData
{
    public Guid Id { get; set; }

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

    public DateTime CalculationDate { get; set; } = DateTime.Now;

    public Guid ShipId { get; set; }

}
