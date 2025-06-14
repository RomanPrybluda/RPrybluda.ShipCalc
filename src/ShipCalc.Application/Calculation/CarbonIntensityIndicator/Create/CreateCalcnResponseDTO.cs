using ShipCalc.Domain;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Calculation.CarbonIntensityIndicator;

public class CreateCalcnResponseDTO
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

    public static CreateCalcnResponseDTO ToCreateCalcnResponse(Ship ship, CreateCalcnCommand command)
    {
        return new CreateCalcnResponseDTO
        {
            ImoNumber = ship.ImoNumber,
            ShipName = ship.ShipName,
            GrossTonnage = ship.GrossTonnage,
            SummerDeadweight = ship.SummerDeadweight,
            BlockCoefficient = ship.BlockCoefficient,
            CargoCompartmentCubicCapacity = ship.CargoCompartmentCubicCapacity,
            ShipType = ship.ShipType,
            IceClass = ship.IceClass,
            Co2EmissionsInTons = command.Co2EmissionsInTons,
            DistanceTravelledInNMs = command.DistanceTravelledInNMs,
            Year = command.Year
        };
    }
}
