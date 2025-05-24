using ShipCalc.Application.Abstractions;
using ShipCalc.Domain.Abstractions;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.CarbonIntensityIndicatorCalculation
{
    public class IceClasedShipCapacityCorrFactorCalculator : IIceClasedShipCapacityCorrFactorCalculator
    {
        private readonly ICapacityIceStrengtheningCorrectionFactorRepository _iceStrengtheningRepository;
        private readonly IReferenceDesignBlockCoefficientRepository _blockCoefficientRepository;

        public IceClasedShipCapacityCorrFactorCalculator(
            ICapacityIceStrengtheningCorrectionFactorRepository iceStrengtheningRepository,
            IReferenceDesignBlockCoefficientRepository blockCoefficientRepository)
        {
            _iceStrengtheningRepository = iceStrengtheningRepository
                ?? throw new ArgumentNullException(nameof(iceStrengtheningRepository));
            _blockCoefficientRepository = blockCoefficientRepository
                ?? throw new ArgumentNullException(nameof(blockCoefficientRepository));
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

            // Step 1: Calculate f_i(ice class)
            decimal fIceClass;
            if (iceClass == IceClass.NotApplicable)
            {
                fIceClass = 1.0m;
            }
            else
            {
                var capacityIceStrengtheningFactor = await _iceStrengtheningRepository.GetByIceClassAsync(iceClass);
                if (capacityIceStrengtheningFactor == null)
                    throw new ArgumentException($"No ice strengthening correction factor found for ice class: {iceClass}");

                fIceClass = capacityIceStrengtheningFactor.ConstantA + capacityIceStrengtheningFactor.ConstantB / deadweight;
                if (fIceClass <= 0)
                    throw new ArgumentException($"Calculated f_i(ice class) must be greater than zero for ice class: {iceClass}");
            }

            // Step 2: Calculate f_icb
            decimal fIcb;
            if (shipType == ShipType.BulkCarrier || shipType == ShipType.Tanker || shipType == ShipType.GeneralCargoShip)
            {
                var blockCoefficientRef = await _blockCoefficientRepository.GetByShipTypeAndDeadweightAsync(shipType, deadweight);
                if (blockCoefficientRef == null)
                    throw new ArgumentException($"No reference block coefficient found for ship type {shipType} and deadweight {deadweight}");

                fIcb = blockCoefficientRef.BlockCoefficient / blockCoefficient;
                fIcb = Math.Max(fIcb, 1.0m);
            }
            else
            {
                fIcb = 1.0m;
            }

            // Step 3: Calculate final f_i
            return fIceClass * fIcb;
        }
    }
}