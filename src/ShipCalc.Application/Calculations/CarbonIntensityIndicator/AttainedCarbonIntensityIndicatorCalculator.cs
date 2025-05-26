using ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;
using ShipCalc.Domain;
using ShipCalc.Domain.Abstractions.CarbonIntensityIndicator;
using ShipCalc.Domain.Abstractions.CorrFactor;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

namespace ShipCalc.Application.Calculations.CarbonIntensityIndicator;

public class AttainedCarbonIntensityIndicatorCalculator : IAttainedCarbonIntensityIndicatorCalculator
{
    private const decimal EmissionConversionFactor = 1_000_000m;

    public decimal IceClasedShipCapacityCorrFactor { get; private set; }

    public decimal IASuperAndIAIceClassedShipCorrFactor { get; private set; }

    public decimal AttainedCarbonIntensityIndicator { get; private set; }

    private readonly IIceClasedShipCapacityCorrFactorCalculator _iceClasedShipCapacityCorrFactorCalc;
    private readonly IIASuperAndIAIceCorrFactorRepo _iASuperAndIAIceClassedShipCorrFactorRepo;

    public AttainedCarbonIntensityIndicatorCalculator(
        IIceClasedShipCapacityCorrFactorCalculator iceClasedShipCapacityCorrFactorCalc,
        ICapacityIceStrengthCorrFactorRepo capacityIceStrengtheningCorrectionFactorRepo,
        IRefDesignBlockCoeffRepo referenceDesignBlockCoefficientRepo,
        IIASuperAndIAIceCorrFactorRepo iASuperAndIAIceClassedShipCorrFactorRepo)
    {
        _iceClasedShipCapacityCorrFactorCalc = iceClasedShipCapacityCorrFactorCalc;
        _iASuperAndIAIceClassedShipCorrFactorRepo = iASuperAndIAIceClassedShipCorrFactorRepo;
    }

    public async Task CalculateAttainedCarbonIntensityIndicator(
        Ship ship,
        decimal capacity,
        decimal co2EmissionsInTons,
        decimal distanceTravelledInNMs)
    {
        if (ship == null)
            throw new ShipNotProvidedException();
        if (capacity <= 0)
            throw new InvalidCapacityException(capacity);
        if (co2EmissionsInTons < 0)
            throw new InvalidCO2EmissionsException(co2EmissionsInTons);
        if (distanceTravelledInNMs <= 0)
            throw new InvalidDistanceTravelledException(distanceTravelledInNMs);

        var iceClasedShipCapacityCorrFactor = await _iceClasedShipCapacityCorrFactorCalc
            .CalculateIceClasedCapacityCorrectionFactor(
            ship.ShipType,
            ship.SummerDeadweight,
            ship.IceClass,
            ship.BlockCoefficient);

        if (iceClasedShipCapacityCorrFactor <= 0)
            throw new InvalidIceClassedCapacityCorrFactorException(iceClasedShipCapacityCorrFactor);

        IceClasedShipCapacityCorrFactor = iceClasedShipCapacityCorrFactor;

        var iASuperAndIAIceClassedShipCorrFactor = await _iASuperAndIAIceClassedShipCorrFactorRepo.GetByIceClassAsync(ship.IceClass);

        if (iASuperAndIAIceClassedShipCorrFactor.CorrectionFactor <= 0)
            throw new InvalidIASuperAndIAIceCorrFactorException(iASuperAndIAIceClassedShipCorrFactor.CorrectionFactor);

        IASuperAndIAIceClassedShipCorrFactor = iASuperAndIAIceClassedShipCorrFactor.CorrectionFactor;

        decimal attainedCarbonIntensityIndicator = EmissionConversionFactor * co2EmissionsInTons /
            (capacity * distanceTravelledInNMs * IceClasedShipCapacityCorrFactor * IASuperAndIAIceClassedShipCorrFactor);

        AttainedCarbonIntensityIndicator = attainedCarbonIntensityIndicator;
    }

}