using ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;
using ShipCalc.Domain.Abstractions.CorrFactor;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Calculators.CorrectionFactors;

public class IceClasedShipCapacityCorrFactorCalculator : IIceClasedShipCapacityCorrFactorCalculator
{
    private const decimal DEFAULT_ICE_STRENTH_SHIP_CAPACITY_CORR_FACTOR = 1.0m;
    private const decimal MINIMUM_ICE_STRENTH_SHIP_CAPACITY_CORR_FACTOR = 1.0m;

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
            throw new InvalidDeadweightException();

        if (blockCoefficient <= 0 || blockCoefficient > 1)
            throw new InvalidBlockCoefficientException(blockCoefficient);

        decimal iceStrengthShipCapacityCorrFactor;
        if (iceClass == IceClass.NotApplicable)
        {
            iceStrengthShipCapacityCorrFactor = DEFAULT_ICE_STRENTH_SHIP_CAPACITY_CORR_FACTOR;
        }
        else
        {
            var capacityIceStrengthFactor = await _iceStrengthRepo.GetByIceClassAsync(iceClass);

            if (capacityIceStrengthFactor == null)
                throw new IceClassCorrectionFactorNotFoundException(iceClass);

            iceStrengthShipCapacityCorrFactor = capacityIceStrengthFactor.ConstantA + capacityIceStrengthFactor.ConstantB / deadweight;
            if (iceStrengthShipCapacityCorrFactor <= 0)
                throw new InvalidCalculatedIceClassFactorException(iceClass, iceStrengthShipCapacityCorrFactor);
        }

        decimal iceGoingCapabilityCapacityCorrFactor;
        if (shipType == ShipType.BulkCarrier || shipType == ShipType.Tanker || shipType == ShipType.GeneralCargoShip)
        {
            var blockCoefficientRef = await _blockCoeffRepo.GetByShipTypeAndDeadweightAsync(shipType, deadweight);
            if (blockCoefficientRef == null)
                throw new ReferenceBlockCoefficientNotFoundException(shipType, deadweight);

            iceGoingCapabilityCapacityCorrFactor = blockCoefficientRef.BlockCoefficient / blockCoefficient;
            iceGoingCapabilityCapacityCorrFactor = Math.Max(iceGoingCapabilityCapacityCorrFactor, MINIMUM_ICE_STRENTH_SHIP_CAPACITY_CORR_FACTOR);
        }
        else
        {
            iceGoingCapabilityCapacityCorrFactor = DEFAULT_ICE_STRENTH_SHIP_CAPACITY_CORR_FACTOR;
        }

        var capacityCorrFactor = iceStrengthShipCapacityCorrFactor * iceGoingCapabilityCapacityCorrFactor;

        return capacityCorrFactor;
    }
}