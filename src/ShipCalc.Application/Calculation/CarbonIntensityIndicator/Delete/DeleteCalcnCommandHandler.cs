using ShipCalc.Application.Abstractions;
using ShipCalc.Application.Abstractions.CQS;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

namespace ShipCalc.Application.Calculation.CarbonIntensityIndicator;

public sealed class DeleteCalcnCommandHandler
    : ICommandHandler<DeleteCalcnCommand, Unit>
{
    private readonly ICarbonIntensityIndicatorCalcnRepo _ciiCalcnRepo;

    public DeleteCalcnCommandHandler(ICarbonIntensityIndicatorCalcnRepo ciiCalcnRepo)
    {
        _ciiCalcnRepo = ciiCalcnRepo;
    }

    public async Task<Unit> Handle(DeleteCalcnCommand command, CancellationToken cancellationToken)
    {
        var calculation = await _ciiCalcnRepo.GetByIdAsync(command.Id, cancellationToken);
        if (calculation is null)
            throw new CalculationNotFound(command.Id);

        await _ciiCalcnRepo.DeleteAsync(calculation);
        await _ciiCalcnRepo.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
