using ShipCalc.Domain.Calculation.CorrectionFactors;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.SeedData;

public static class CapacityIceStrengthCorrFactorsSeedData
{
    public static List<CapacityIceStrengthCorrFactor> GetData() => new()
    {
        new()
        {
            Id = Guid.NewGuid(),
            IceClass = IceClass.IC,
            ConstantA  = 1.0044m, ConstantB  = 58.5m
        },
        new()
        {
            Id = Guid.NewGuid(),
            IceClass = IceClass.IB,
            ConstantA  = 1.0067m, ConstantB  = 62.7m
        },
        new()
        {
            Id = Guid.NewGuid(),
            IceClass = IceClass.IA,
            ConstantA  = 1.0099m, ConstantB  = 95.1m
        },
        new()
        {
            Id = Guid.NewGuid(),
            IceClass = IceClass.IASuper,
            ConstantA  = 1.0151m, ConstantB  = 228.75m
        },
    };
}

