namespace ShipCalc.Application.Abstractions.Data
{
    public interface ISeedDataInitializer
    {
        Task InitializeCapacityIceStrengthCorrFactorAsync();

        Task InitializeIASuperAndIAIceCorrFactorAsync();

        Task InitializeRatingThresholdsAsync();

        Task InitializeIReductionFactorAsync();

        Task InitializeIRefDesignBlockCoeffAsync();

        Task InitializeRefLineParamsAsync();

    }
}
