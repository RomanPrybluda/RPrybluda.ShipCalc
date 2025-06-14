using ShipCalc.Application.Abstractions.CQS;

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
            TQuery query,
            CancellationToken cancellationToken)
            where TQuery : IQuery<TResult>
        {
            using var scope = _serviceProvider.CreateScope();
            var scopedProvider = scope.ServiceProvider;

            var handler = scopedProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
            return await handler.Handle(query, cancellationToken);
        }
    }
}
