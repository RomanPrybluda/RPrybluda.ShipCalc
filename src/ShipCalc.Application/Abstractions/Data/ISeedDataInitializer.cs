namespace ShipCalc.Application.Abstractions.Data
{
    public interface ISeedDataInitializer
    {
        Task InitializeCapacityIceStrengthCorrFactorAsync();

        Task InitializeIASuperAndIAIceCorrFactorAsync();

        Task InitializeRatingThresholdsAsync();

        Task InitializeReductionFactorAsync();

        Task InitializeRefDesignBlockCoeffAsync();

        Task InitializeRefLineParamsAsync();

    }
}
