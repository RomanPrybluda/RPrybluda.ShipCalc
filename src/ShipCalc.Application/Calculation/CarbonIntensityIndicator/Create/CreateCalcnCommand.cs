using ShipCalc.Application.Abstractions.CQS;
using ShipCalc.Domain;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Calculation.CarbonIntensityIndicator;

public class CreateCalcnCommand
    : ICommand<CreateCalcnResponseDTO>
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

    public static Ship ToShip(CreateCalcnCommand command)
    {
        return new Ship
        {
            Id = Guid.NewGuid(),
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

}