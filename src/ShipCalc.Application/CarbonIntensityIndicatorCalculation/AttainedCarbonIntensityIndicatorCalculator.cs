using ShipCalc.Application.Abstractions;
using ShipCalc.Domain;
using ShipCalc.Domain.Abstractions;

namespace ShipCalc.Application.CarbonIntensityIndicatorCalculation;

public class AttainedCarbonIntensityIndicatorCalculator : IAttainedCarbonIntensityIndicatorCalculator
{

    public decimal IceClasedShipCapacityCorrFactor { get; private set; }

    public decimal IASuperAndIAIceClassedShipCorrFactor { get; private set; }

    public decimal AttainedCarbonIntensityIndicator { get; private set; }

    private readonly IIceClasedShipCapacityCorrFactorCalculator _iceClasedShipCapacityCorrFactorCalc;
    private readonly IIASuperAndIAIceClassedShipCorrFactorRepository _iASuperAndIAIceClassedShipCorrFactorRepo;

    public AttainedCarbonIntensityIndicatorCalculator(
        IIceClasedShipCapacityCorrFactorCalculator iceClasedShipCapacityCorrFactorCalc,
        ICapacityIceStrengtheningCorrectionFactorRepository capacityIceStrengtheningCorrectionFactorRepo,
        IReferenceDesignBlockCoefficientRepository referenceDesignBlockCoefficientRepo,
        IIASuperAndIAIceClassedShipCorrFactorRepository iASuperAndIAIceClassedShipCorrFactorRepo)
    {
        _iceClasedShipCapacityCorrFactorCalc = iceClasedShipCapacityCorrFactorCalc
            ?? throw new ArgumentNullException(nameof(iceClasedShipCapacityCorrFactorCalc));
        _iASuperAndIAIceClassedShipCorrFactorRepo = iASuperAndIAIceClassedShipCorrFactorRepo
            ?? throw new ArgumentNullException(nameof(iASuperAndIAIceClassedShipCorrFactorRepo));
    }

    public async Task CalculateAttainedCarbonIntensityIndicator(
        Ship ship,
        decimal capacity,
        decimal co2EmissionsInTons,
        decimal distanceTravelledInNMs)
    {
        if (ship == null)
            throw new ArgumentNullException(nameof(ship));
        if (capacity <= 0)
            throw new ArgumentException("Capacity must be greater than zero.", nameof(capacity));
        if (co2EmissionsInTons < 0)
            throw new ArgumentException("CO2 emissions cannot be negative.", nameof(co2EmissionsInTons));
        if (distanceTravelledInNMs <= 0)
            throw new ArgumentException("Distance travelled must be greater than zero.", nameof(distanceTravelledInNMs));

        var iceClasedShipCapacityCorrFactor = await _iceClasedShipCapacityCorrFactorCalc
            .CalculateIceClasedCapacityCorrectionFactor(
            ship.ShipType,
            ship.SummerDeadweight,
            ship.IceClass,
            ship.BlockCoefficient);
        if (iceClasedShipCapacityCorrFactor <= 0)
            throw new ArgumentException("Ice-classed capacity correction factor must be greater than zero.");

        IceClasedShipCapacityCorrFactor = iceClasedShipCapacityCorrFactor;

        var iASuperAndIAIceClassedShipCorrFactor = await _iASuperAndIAIceClassedShipCorrFactorRepo.GetByIceClassAsync(ship.IceClass);
        if (iASuperAndIAIceClassedShipCorrFactor.CorrectionFactor <= 0)
            throw new ArgumentException("IA Super or IA ice-classed correction factor must be greater than zero.");

        IASuperAndIAIceClassedShipCorrFactor = iASuperAndIAIceClassedShipCorrFactor.CorrectionFactor;

        decimal attainedCarbonIntensityIndicator = 1000000m * co2EmissionsInTons /
            (capacity * distanceTravelledInNMs * IceClasedShipCapacityCorrFactor * IASuperAndIAIceClassedShipCorrFactor);

        AttainedCarbonIntensityIndicator = attainedCarbonIntensityIndicator;
    }

}