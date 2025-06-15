using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ShipCalc.Application.Abstractions.CQS;

namespace ShipCalc.Application.Dispatchers;

public sealed class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> DispatchAsync<TCommand, TResult>(
        TCommand command,
        CancellationToken cancellationToken)
        where TCommand : ICommand<TResult>
    {
        using var scope = _serviceProvider.CreateScope();
        var provider = scope.ServiceProvider;

        var validator = provider.GetService<IValidator<TCommand>>();
        if (validator is not null)
        {
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }

        var handler = provider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
        return await handler.Handle(command, cancellationToken);
    }
}
