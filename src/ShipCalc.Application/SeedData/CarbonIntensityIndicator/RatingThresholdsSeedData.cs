using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.SeedData;

public static class RatingThresholdsSeedData
{
    public static List<RatingThreshold> GetData() => new()
    {
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.Tanker,
            D1 = 0.82m, D2 = 0.93m, D3 = 1.08m, D4 = 1.28m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.BulkCarrier,
            D1 = 0.86m, D2 = 0.94m, D3 = 1.06m, D4 = 1.18m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.CombinationCarrier,
            D1 = 0.87m, D2 = 0.96m, D3 = 1.06m, D4 = 1.14m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.ContainerShip,
            D1 = 0.83m, D2 = 0.94m, D3 = 1.07m, D4 = 1.19m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.GeneralCargoShip,
            D1 = 0.83m, D2 = 0.94m, D3 = 1.06m, D4 = 1.19m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.GasCarrier,
            LowerDeadweight = 65_000,
            D1 = 0.81m, D2 = 0.91m, D3 = 1.12m, D4 = 1.44m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.GasCarrier,
            UpperDeadweight = 65_000,
            D1 = 0.85m, D2 = 0.95m, D3 = 1.06m, D4 = 1.25m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.LNGCarrier,
            LowerDeadweight = 100_000,
            D1 = 0.89m, D2 = 0.98m, D3 = 1.06m, D4 = 1.13m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.LNGCarrier,
            UpperDeadweight = 100_000,
            D1 = 0.78m, D2 = 0.92m, D3 = 1.10m, D4 = 1.37m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.RoRoPassenger,
            D1 = 0.76m, D2 = 0.89m, D3 = 1.08m, D4 = 1.27m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.RoRoCargo,
            D1 = 0.76m, D2 = 0.89m, D3 = 1.08m, D4 = 1.27m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.RoRoCargoVehicle,
            D1 = 0.86m, D2 = 0.94m, D3 = 1.06m, D4 = 1.16m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.RefrigeratedCargoCarrier,
            D1 = 0.78m, D2 = 0.91m, D3 = 1.07m, D4 = 1.20m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.CruisePassengerShip,
            D1 = 0.87m, D2 = 0.95m, D3 = 1.06m, D4 = 1.16m
        }
    };
}
