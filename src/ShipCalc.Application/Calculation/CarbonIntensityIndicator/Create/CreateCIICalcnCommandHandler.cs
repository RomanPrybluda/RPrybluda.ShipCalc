using ShipCalc.Application.Abstractions;
using ShipCalc.Application.Abstractions.CQRS;
using ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;
using ShipCalc.Application.Calculators.CarbonIntensityIndicator;
using ShipCalc.Domain;
using ShipCalc.Domain.Abstractions.CarbonIntensityIndicator;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

namespace ShipCalc.Application.Calculation.CarbonIntensityIndicator;

public class CreateCIICalcnCommandHandler
    : ICommandHandler<CreateCIICalcnCommand, CarbonIntensityIndicatorCalculation>
{
    private readonly ICarbonIntensityIndicatorCalcnRepo _calcnRepo;
    private readonly IShipRepo _shipRepo;
    private readonly ICapacityCalculator _capacityCalculator;
    private readonly IRequiredCarbonIntensityIndicatorCalculator _requiredCarbonIntensityIndicatorCalculator;
    private readonly IAttainedCarbonIntensityIndicatorCalculator _attainedCarbonIntensityIndicatorCalculator;
    private readonly IRatingThresholdsRepo _carbonIntensityIndicatorRatingThresholdsRepo;

    public CreateCIICalcnCommandHandler(
        ICarbonIntensityIndicatorCalcnRepo repository,
        IShipRepo shipRepo,
        ICapacityCalculator capacityCalculator,
        IRequiredCarbonIntensityIndicatorCalculator requiredCarbonIntensityIndicatorCalculator,
        IAttainedCarbonIntensityIndicatorCalculator attainedCarbonIntensityIndicatorCalculator,
        IRatingThresholdsRepo carbonIntensityIndicatorRatingThresholdsRepository)
    {
        _calcnRepo = repository;
        _shipRepo = shipRepo;
        _capacityCalculator = capacityCalculator;
        _requiredCarbonIntensityIndicatorCalculator = requiredCarbonIntensityIndicatorCalculator;
        _attainedCarbonIntensityIndicatorCalculator = attainedCarbonIntensityIndicatorCalculator;
        _carbonIntensityIndicatorRatingThresholdsRepo = carbonIntensityIndicatorRatingThresholdsRepository;
    }

    public async Task<CarbonIntensityIndicatorCalculation> Handle(
        CreateCIICalcnCommand command,
        CancellationToken cancellationToken)
    {

        var ship = new Ship
        {
            Id = Guid.NewGuid(),
            ImoNumber = command.ImoNumber,
            ShipName = command.ShipName,
            GrossTonnage = command.GrossTonnage,
            SummerDeadweight = command.SummerDeadweight,
            BlockCoefficient = command.BlockCoefficient,
            CargoCompartmentCubicCapacity = command.CargoCompartmentCubicCapacity,
            ShipType = command.ShipType,
            IceClass = command.IceClass
        };
        await _shipRepo.AddAsync(ship);
        await _shipRepo.SaveChangesAsync(cancellationToken);

        var createdShip = await _shipRepo.GetById(ship.Id);
        if (createdShip == null)
        {
            throw new Exception($"Ship with id {ship.Id} not found after creation");
        }

        var calculator = new CarbonIntensityIndicatorRatingCalculator(
            _capacityCalculator,
            _requiredCarbonIntensityIndicatorCalculator,
            _attainedCarbonIntensityIndicatorCalculator,
            _carbonIntensityIndicatorRatingThresholdsRepo);

        var calcnResult = await calculator.CalculateRatingAsync(
                createdShip,
                command.Co2EmissionsInTons,
                command.DistanceTravelledInNMs,
                command.Year);

        await _calcnRepo.AddAsync(calcnResult);
        await _calcnRepo.SaveChangesAsync(cancellationToken);

        return calcnResult;
    }
}
