namespace ShipCalc.Application.Abstractions.CQS;

public interface IQueryDispatcher
{
    Task<TResult> DispatchAsync<TQuery, TResult>(TQuery command, CancellationToken cancellationToken)
        where TQuery : IQuery<TResult>;
}
