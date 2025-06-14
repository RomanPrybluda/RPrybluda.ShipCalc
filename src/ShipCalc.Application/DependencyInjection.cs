using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ShipCalc.Application.Abstractions.CQS;
using ShipCalc.Application.Dispatchers;
using ShipCalc.Domain.Abstractions;

namespace ShipCalc.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.Scan(scan => scan
        .FromAssembliesOf(typeof(DependencyInjection))
        .AddClasses(classes => classes.Where(type =>
            type.GetInterfaces().Any(i => i != typeof(ICalculator) && typeof(ICalculator).IsAssignableFrom(i))
        ))
        .AsImplementedInterfaces()
        .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssembliesOf(typeof(DependencyInjection))
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();

        return services;
    }
}
