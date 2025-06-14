using ShipCalc.Application.Abstractions;
using ShipCalc.Application.Abstractions.CQS;
using ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;
using ShipCalc.Application.Calculators.CarbonIntensityIndicator;
using ShipCalc.Domain.Abstractions.CarbonIntensityIndicator;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

namespace ShipCalc.Application.Calculation.CarbonIntensityIndicator;

public class CreateCalcnCommandHandler
    : ICommandHandler<CreateCalcnCommand, CreateCalcnResponseDTO>
{
    private readonly ICarbonIntensityIndicatorCalcnRepo _ciiCalcnRepo;
    private readonly IShipRepo _shipRepo;
    private readonly ICapacityCalculator _capacityCalculator;
    private readonly IRequiredCarbonIntensityIndicatorCalculator _requiredCarbonIntensityIndicatorCalculator;
    private readonly IAttainedCarbonIntensityIndicatorCalculator _attainedCarbonIntensityIndicatorCalculator;
    private readonly IRatingThresholdsRepo _carbonIntensityIndicatorRatingThresholdsRepo;

    public CreateCalcnCommandHandler(
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

    public async Task<CreateCalcnResponseDTO> Handle(
        CreateCalcnCommand command,
        CancellationToken cancellationToken)
    {
        var ship = CreateCalcnCommand.ToShip(command);
        await _shipRepo.AddAsync(ship);
        await _shipRepo.SaveChangesAsync(cancellationToken);

        var createdShip = await _shipRepo.GetByIdAsync(ship.Id);
        if (createdShip == null)
            throw new ShipNotFound(ship.Id);

        var calculator = new CarbonIntensityIndicatorRatingCalculator(
            _capacityCalculator,
            _requiredCarbonIntensityIndicatorCalculator,
            _attainedCarbonIntensityIndicatorCalculator,
            _carbonIntensityIndicatorRatingThresholdsRepo);

        var ciiCalcnResult = await calculator.CalculateRatingAsync(
                createdShip,
                command.Co2EmissionsInTons,
                command.DistanceTravelledInNMs,
                command.Year);

        if (ciiCalcnResult == null)
            throw new CreateCalculationFailed();

        await _ciiCalcnRepo.AddAsync(ciiCalcnResult);
        await _ciiCalcnRepo.SaveChangesAsync(cancellationToken);

        var calcnDTO = CreateCalcnResponseDTO.ToCreateCalcnResponse(createdShip, command);

        return calcnDTO;
    }
}
