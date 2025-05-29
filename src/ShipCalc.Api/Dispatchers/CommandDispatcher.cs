using FluentValidation;
using ShipCalc.Application.Abstractions.CQRS;

namespace ShipCalc.Api.Dispatchers;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> Dispatch<TCommand, TResult>(
        TCommand command,
        CancellationToken cancellationToken)
        where TCommand : ICommand<TResult>
    {
        var validator = _serviceProvider.GetService<IValidator<TCommand>>();
        if (validator != null)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }

        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
        return await handler.Handle(command, cancellationToken);
    }
}
