namespace ShipCalc.Application.Abstractions.Data
{
    public interface ISeedDataInitializer
    {
        Task InitializeRICapacityIceStrengthCorrFactorAsync();

        Task InitializeIASuperAndIAIceCorrFactorAsync();

        Task InitializeRatingThresholdsAsync();

        Task InitializeIReductionFactorAsync();

        Task InitializeIRefDesignBlockCoeffAsync();

        Task InitializeRefLineParamsAsync();

    }
}
