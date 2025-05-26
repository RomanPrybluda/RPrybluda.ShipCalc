using ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;
using ShipCalc.Domain.Abstractions.CorrFactor;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Calculations.CorrectionFactors;

public class IceClasedShipCapacityCorrFactorCalculator : IIceClasedShipCapacityCorrFactorCalculator
{
    private readonly decimal DefaultCorrectionFactor = 1.0m;
    private readonly decimal MinimumCorrectionFactor = 1.0m;

    private readonly ICapacityIceStrengthCorrFactorRepo _iceStrengthRepo;
    private readonly IRefDesignBlockCoeffRepo _blockCoeffRepo;

    public IceClasedShipCapacityCorrFactorCalculator(
        ICapacityIceStrengthCorrFactorRepo iceStrengthRepo,
        IRefDesignBlockCoeffRepo blockCoeffRepo)
    {
        _iceStrengthRepo = iceStrengthRepo;
        _blockCoeffRepo = blockCoeffRepo;
    }

    public async Task<decimal> CalculateIceClasedCapacityCorrectionFactor(
        ShipType shipType,
        decimal deadweight,
        IceClass iceClass,
        decimal blockCoefficient)
    {
        if (deadweight <= 0)
            throw new ArgumentException("Deadweight must be greater than zero.", nameof(deadweight));
        if (blockCoefficient <= 0 || blockCoefficient > 1)
            throw new ArgumentException("Block coefficient (C_b) must be between 0 and 1.", nameof(blockCoefficient));

        decimal fIceClass;
        if (iceClass == IceClass.NotApplicable)
        {
            fIceClass = DefaultCorrectionFactor;
        }
        else
        {
            var capacityIceStrengthFactor = await _iceStrengthRepo.GetByIceClassAsync(iceClass);
            if (capacityIceStrengthFactor == null)
                throw new ArgumentException($"No ice strengthening correction factor found for ice class: {iceClass}");

            fIceClass = capacityIceStrengthFactor.ConstantA + capacityIceStrengthFactor.ConstantB / deadweight;
            if (fIceClass <= 0)
                throw new ArgumentException($"Calculated f_i(ice class) must be greater than zero for ice class: {iceClass}");
        }

        decimal fIcb;
        if (shipType == ShipType.BulkCarrier || shipType == ShipType.Tanker || shipType == ShipType.GeneralCargoShip)
        {
            var blockCoefficientRef = await _blockCoeffRepo.GetByShipTypeAndDeadweightAsync(shipType, deadweight);
            if (blockCoefficientRef == null)
                throw new ArgumentException($"No reference block coefficient found for ship type {shipType} and deadweight {deadweight}");

            fIcb = blockCoefficientRef.BlockCoefficient / blockCoefficient;
            fIcb = Math.Max(fIcb, MinimumCorrectionFactor);
        }
        else
        {
            fIcb = DefaultCorrectionFactor;
        }

        return fIceClass * fIcb;
    }
}