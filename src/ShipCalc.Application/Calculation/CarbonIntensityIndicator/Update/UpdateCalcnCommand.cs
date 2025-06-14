using ShipCalc.Application.Abstractions.CQS;
using ShipCalc.Domain;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Calculation.CarbonIntensityIndicator;

public class UpdateCalcnCommand : ICommand<CarbonIntensityIndicatorCalculation>
{
    public Guid Id { get; set; }

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

    public static Ship UpdateShip(UpdateCalcnCommand command)
    {
        return new Ship
        {
            ImoNumber = command.ImoNumber,
            ShipName = command.ShipName,
            GrossTonnage = command.GrossTonnage,
            SummerDeadweight = command.SummerDeadweight,
            BlockCoefficient = command.BlockCoefficient,
            CargoCompartmentCubicCapacity = command.CargoCompartmentCubicCapacity,
            ShipType = command.ShipType,
            IceClass = command.IceClass
        };
    }

    public static CarbonIntensityIndicatorCalculation UpdateCiiCalcn(
    CarbonIntensityIndicatorCalculation calcn,
    CarbonIntensityIndicatorCalculation calculatedResult)
    {
        calcn.Co2EmissionsInTons = calculatedResult.Co2EmissionsInTons;
        calcn.DistanceTravelledInNMs = calculatedResult.DistanceTravelledInNMs;
        calcn.Year = calculatedResult.Year;
        calcn.Capacity = calculatedResult.Capacity;
        calcn.RefLineParameterA = calculatedResult.RefLineParameterA;
        calcn.RefLineParameterC = calculatedResult.RefLineParameterC;
        calcn.RefLine = calculatedResult.RefLine;
        calcn.RefLineReductionFactor = calculatedResult.RefLineReductionFactor;
        calcn.RequiredCarbonIntensityIndicator = calculatedResult.RequiredCarbonIntensityIndicator;
        calcn.IceClasedShipCapacityCorrFactor = calculatedResult.IceClasedShipCapacityCorrFactor;
        calcn.IASuperAndIAIceCorrFactor = calculatedResult.IASuperAndIAIceCorrFactor;
        calcn.AttainedCarbonIntensityIndicator = calculatedResult.AttainedCarbonIntensityIndicator;
        calcn.CarbonIntensityIndicatorNumericalRating = calculatedResult.CarbonIntensityIndicatorNumericalRating;
        calcn.CarbonIntensityIndicatorRating = calculatedResult.CarbonIntensityIndicatorRating;
        calcn.CalculationDate = calculatedResult.CalculationDate;

        return calcn;
    }

}
