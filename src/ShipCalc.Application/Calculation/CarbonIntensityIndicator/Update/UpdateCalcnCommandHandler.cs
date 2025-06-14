using ShipCalc.Application.Abstractions;
using ShipCalc.Application.Abstractions.CQS;
using ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;
using ShipCalc.Application.Calculators.CarbonIntensityIndicator;
using ShipCalc.Domain.Abstractions.CarbonIntensityIndicator;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

namespace ShipCalc.Application.Calculation.CarbonIntensityIndicator;

public class UpdateCalcnCommandHandler : ICommandHandler<UpdateCalcnCommand, UpdateCalcnResponseDTO>
{
    private readonly ICarbonIntensityIndicatorCalcnRepo _ciiCalcnRepo;
    private readonly IShipRepo _shipRepo;
    private readonly ICapacityCalculator _capacityCalculator;
    private readonly IRequiredCarbonIntensityIndicatorCalculator _requiredCarbonIntensityIndicatorCalculator;
    private readonly IAttainedCarbonIntensityIndicatorCalculator _attainedCarbonIntensityIndicatorCalculator;
    private readonly IRatingThresholdsRepo _carbonIntensityIndicatorRatingThresholdsRepo;

    public UpdateCalcnCommandHandler(
        ICarbonIntensityIndicatorCalcnRepo ciiCalcnRepo,
        IShipRepo shipRepo,
        ICapacityCalculator capacityCalculator,
        IRequiredCarbonIntensityIndicatorCalculator requiredCarbonIntensityIndicatorCalculator,
        IAttainedCarbonIntensityIndicatorCalculator attainedCarbonIntensityIndicatorCalculator,
        IRatingThresholdsRepo carbonIntensityIndicatorRatingThresholdsRepository)
    {
        _ciiCalcnRepo = ciiCalcnRepo;
        _shipRepo = shipRepo;
        _capacityCalculator = capacityCalculator;
        _requiredCarbonIntensityIndicatorCalculator = requiredCarbonIntensityIndicatorCalculator;
        _attainedCarbonIntensityIndicatorCalculator = attainedCarbonIntensityIndicatorCalculator;
        _carbonIntensityIndicatorRatingThresholdsRepo = carbonIntensityIndicatorRatingThresholdsRepository;
    }

    public async Task<UpdateCalcnResponseDTO> Handle(
        UpdateCalcnCommand command,
        CancellationToken cancellationToken)
    {
        var calcn = await _ciiCalcnRepo.GetByIdAsync(command.Id, cancellationToken)
                    ?? throw new CalculationNotFound(command.Id);

        var ship = await _shipRepo.GetByIdAsync(calcn.ShipId, cancellationToken)
            ?? throw new ShipNotFound(calcn.ShipId);

        var updatedShip = UpdateCalcnCommand.UpdateShip(command);
        await _shipRepo.SaveChangesAsync(cancellationToken);

        var calculator = new CarbonIntensityIndicatorRatingCalculator(
            _capacityCalculator,
            _requiredCarbonIntensityIndicatorCalculator,
            _attainedCarbonIntensityIndicatorCalculator,
            _carbonIntensityIndicatorRatingThresholdsRepo);

        var ciiCalcnResult = await calculator.CalculateRatingAsync(
            updatedShip,
            command.Co2EmissionsInTons,
            command.DistanceTravelledInNMs,
            command.Year)
            ?? throw new UpdateCalculationFailed();

        var updatedCalns = UpdateCalcnCommand.UpdateCiiCalcn(calcn, ciiCalcnResult);

        await _ciiCalcnRepo.SaveChangesAsync(cancellationToken);

        var calnDTO = UpdateCalcnResponseDTO.FromCalculation(calcn, ship);

        return calnDTO;
    }
}
