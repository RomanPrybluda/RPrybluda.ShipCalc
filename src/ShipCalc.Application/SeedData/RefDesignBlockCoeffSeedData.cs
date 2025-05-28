using ShipCalc.Domain.Calculation.CorrectionFactors;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.SeedData;

public static class RefDesignBlockCoeffSeedData
{
    public static List<RefDesignBlockCoeff> GetData() => new()
    {
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.BulkCarrier,
            MaxDeadweight = 10_000,
            BlockCoefficient = 0.78m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.BulkCarrier,
            MinDeadweight = 10_000,
            MaxDeadweight = 25_000,
            BlockCoefficient = 0.80m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.BulkCarrier,
            MinDeadweight = 25_000,
            MaxDeadweight = 55_000,
            BlockCoefficient = 0.82m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.BulkCarrier,
            MinDeadweight = 55_000,
            MaxDeadweight = 75_000,
            BlockCoefficient = 0.86m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.BulkCarrier,
            MaxDeadweight = 75_000,
            BlockCoefficient = 0.86m
        },

        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.Tanker,
            MaxDeadweight = 10_000,
            BlockCoefficient = 0.78m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.Tanker,
            MinDeadweight = 10_000,
            MaxDeadweight = 25_000,
            BlockCoefficient = 0.78m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.Tanker,
            MinDeadweight = 25_000,
            MaxDeadweight = 55_000,
            BlockCoefficient = 0.80m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.Tanker,
            MinDeadweight = 55_000,
            MaxDeadweight = 75_000,
            BlockCoefficient = 0.83m
        },
        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.Tanker,
            MaxDeadweight = 75_000,
            BlockCoefficient = 0.83m
        },

        new()
        {
            Id = Guid.NewGuid(),
            ShipType = ShipType.GeneralCargoShip,
            BlockCoefficient = 0.80m
        },
    };
}
