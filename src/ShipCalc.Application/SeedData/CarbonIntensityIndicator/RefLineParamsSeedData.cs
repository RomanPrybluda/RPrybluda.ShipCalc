using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.SeedData;

public static class RefLineParamsSeedData
{
    public static List<RefLineParams> GetData() => new()
    {
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.BulkCarrier,
            LowerBound = 279_000m,
            ParameterA = 4745m, ParameterC = 0.622m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.BulkCarrier,
            UpperBound = 279_000m,
            ParameterA = 4745m, ParameterC = 0.622m
        },

        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.GasCarrier,
            LowerBound = 65_000m,
            ParameterA = 0.0014405m, ParameterC = 2.071m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.GasCarrier,
            UpperBound = 65_000m,
            ParameterA = 8104m, ParameterC = 0.639m
        },

        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.Tanker,
            ParameterA = 5247m, ParameterC = 0.610m
        },

        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.ContainerShip,
            ParameterA = 1984m, ParameterC = 0.489m
        },

        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.GeneralCargoShip,
            LowerBound = 20_000m,
            ParameterA = 31948m, ParameterC = 0.792m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.GeneralCargoShip,
            UpperBound = 20_000m,
            ParameterA = 588m, ParameterC = 0.3885m
        },

        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.RefrigeratedCargoCarrier,
            ParameterA = 4600m, ParameterC = 0.557m
        },

        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.CombinationCarrier,
            ParameterA = 5119m, ParameterC = 0.622m
        },

        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.LNGCarrier,
            LowerBound = 100_000m,
            ParameterA = 9827m, ParameterC = 0m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.LNGCarrier,
            LowerBound = 65_000m,
            UpperBound = 100_000m,
            ParameterA = 0.0000014479m, ParameterC = 2.673m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.LNGCarrier,
            UpperBound = 65_000m,
            ParameterA = 0.0000014479m, ParameterC = 2.673m
        },

        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.RoRoCargoVehicle,
            LowerBound = 57_700m,
            ParameterA = 3627m, ParameterC = 0.590m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.RoRoCargoVehicle,
            LowerBound = 30_000m,
            UpperBound = 57_700m,
            ParameterA = 3627m, ParameterC = 0.590m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.RoRoCargoVehicle,
            UpperBound = 30_000m,
            ParameterA = 330m, ParameterC = 0.329m
        },

        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.RoRoCargo,
            ParameterA = 1967m, ParameterC = 0.485m
        },

        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.RoRoPassenger,
            ParameterA = 2023m, ParameterC = 0.460m
        },

        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.RoRoPassengerCargoHighSpeedCraft,
            ParameterA = 4196m, ParameterC = 0.460m
        },

        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.CruisePassengerShip,
            ParameterA = 930m, ParameterC = 0.383m
        }
    };
}
