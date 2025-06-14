using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Calculation.CarbonIntensityIndicator;

public class UpdateCalcnRequestDTO
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

    public static UpdateCalcnCommand ToCommand(Guid id, UpdateCalcnRequestDTO request)
    {
        return new UpdateCalcnCommand
        {
            Id = id,
            ImoNumber = request.ImoNumber,
            ShipName = request.ShipName,
            GrossTonnage = request.GrossTonnage,
            SummerDeadweight = request.SummerDeadweight,
            BlockCoefficient = request.BlockCoefficient,
            CargoCompartmentCubicCapacity = request.CargoCompartmentCubicCapacity,
            ShipType = request.ShipType,
            IceClass = request.IceClass,
            Co2EmissionsInTons = request.Co2EmissionsInTons,
            DistanceTravelledInNMs = request.DistanceTravelledInNMs,
            Year = request.Year
        };
    }
}

