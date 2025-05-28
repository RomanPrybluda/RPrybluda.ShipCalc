using ShipCalc.Application.Abstractions;
using ShipCalc.Application.Abstractions.CQRS;

namespace ShipCalc.Api.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> Dispatch<TQuery, TResult>(
            TQuery command,
            CancellationToken cancellationToken)
            where TQuery : IQuery<TResult>
        {
            var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
            return await handler.Handle(command, cancellationToken);
        }
    }
}
