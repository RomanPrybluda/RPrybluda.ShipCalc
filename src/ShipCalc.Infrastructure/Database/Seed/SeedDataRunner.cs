using ShipCalc.Application.Abstractions.Data;

namespace ShipCalc.Infrastructure.Database;

public class SeedDataRunner
{
    private readonly ISeedDataInitializer _initializer;

    public SeedDataRunner(ISeedDataInitializer initializer)
    {
        _initializer = initializer;
    }

    public async Task RunAllAsync()
    {
        await _initializer.InitializeCapacityIceStrengthCorrFactorAsync();
        await _initializer.InitializeIASuperAndIAIceCorrFactorAsync();
        await _initializer.InitializeRatingThresholdsAsync();
        await _initializer.InitializeReductionFactorAsync();
        await _initializer.InitializeRefDesignBlockCoeffAsync();
        await _initializer.InitializeRefLineParamsAsync();
    }
}
