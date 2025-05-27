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
        await _initializer.InitializeRICapacityIceStrengthCorrFactorAsync();
        await _initializer.InitializeIASuperAndIAIceCorrFactorAsync();
        await _initializer.InitializeRatingThresholdsAsync();
        await _initializer.InitializeIReductionFactorAsync();
        await _initializer.InitializeIRefDesignBlockCoeffAsync();
        await _initializer.InitializeRefLineParamsAsync();
    }
}
