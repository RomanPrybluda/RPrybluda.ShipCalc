using FluentValidation;

namespace ShipCalc.Application.Calculation.CarbonIntensityIndicator;

internal sealed class DeleteTodoCommandValidator : AbstractValidator<DeleteCalcnCommand>
{
    public DeleteTodoCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
