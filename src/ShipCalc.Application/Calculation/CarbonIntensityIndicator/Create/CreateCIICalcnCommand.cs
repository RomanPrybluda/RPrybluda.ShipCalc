using ShipCalc.Application.Abstractions.CQRS;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Calculation.CarbonIntensityIndicator;

public class CreateCIICalcnCommand
    : ICommand<CarbonIntensityIndicatorCalculation>
{
    public int ImoNumber { get; set; }

    public string ShipName { get; set; } = string.Empty;

    public decimal GrossTonnage { get; set; }

    public decimal SummerDeadweight { get; set; }

    public decimal BlockCoefficient { get; set; }

    public decimal CargoCompartmentCubicCapacity { get; set; }

    public ShipType ShipType { get; set; }

    public IceClass IceClass { get; set; }

    public decimal Co2EmissionsInTons { get; set; }

    public decimal DistanceTravelledInNMs { get; set; }

    public int Year { get; set; }
}