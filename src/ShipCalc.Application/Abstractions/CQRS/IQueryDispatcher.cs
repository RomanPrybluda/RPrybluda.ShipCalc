namespace ShipCalc.Application.Abstractions.CQRS;

public interface IQueryDispatcher
{
    Task<TResult> Dispatch<TQuery, TResult>(TQuery command, CancellationToken cancellationToken)
        where TQuery : IQuery<TResult>;
}
