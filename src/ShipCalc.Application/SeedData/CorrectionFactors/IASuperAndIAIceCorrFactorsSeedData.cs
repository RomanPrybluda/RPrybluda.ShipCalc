using ShipCalc.Domain.Calculation.CorrectionFactors;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.SeedData;

public static class IASuperAndIAIceCorrFactorsSeedData
{
    public static List<IASuperAndIAIceCorrFactor> GetData() => new()
    {
        new()
        {
            Id = Guid.NewGuid(),
            IceClass = IceClass.IASuper,
            CorrectionFactor = 1.05m
        },
        new()
        {
            Id = Guid.NewGuid(),
            IceClass = IceClass.IA,
            CorrectionFactor = 1.05m
        },
    };
}