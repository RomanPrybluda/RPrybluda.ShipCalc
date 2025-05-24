using ShipCalc.Application.Abstractions;
using ShipCalc.Domain.Abstractions.CarbonIntensityIndicatorCalculation;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.CarbonIntensityIndicatorCalculation
{
    public class IASuperAndIAIceClassedShipCorrFactorCalculator : IIASuperAndIAIceClassedShipCorrFactorCalculator
    {
        private readonly IIASuperAndIAIceClassedShipCorrFactorRepository _corrFactorRepository;

        public IASuperAndIAIceClassedShipCorrFactorCalculator(
            IIASuperAndIAIceClassedShipCorrFactorRepository corrFactorRepository)
        {
            _corrFactorRepository = corrFactorRepository
                ?? throw new ArgumentNullException(nameof(corrFactorRepository));
        }

        public async Task<decimal> CalculateIASuperAndIAIceClassedShipCorrFactor(IceClass? iceClass)
        {
            if (!iceClass.HasValue || iceClass == IceClass.NotApplicable)
                return 1.0m;

            var corrFactor = await _corrFactorRepository.GetByIceClassAsync(iceClass.Value);
            if (corrFactor == null)
                return 1.0m;

            if (corrFactor.CorrectionFactor <= 0)
                throw new InvalidOperationException($"Correction factor for ice class {iceClass} must be greater than zero.");

            return corrFactor.CorrectionFactor;
        }
    }
}
