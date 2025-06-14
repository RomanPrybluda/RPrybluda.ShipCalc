namespace ShipCalc.Application.Abstractions.CQS;

public interface IQueryDispatcher
{
    Task<TResult> Dispatch<TQuery, TResult>(TQuery command, CancellationToken cancellationToken)
        where TQuery : IQuery<TResult>;
}
