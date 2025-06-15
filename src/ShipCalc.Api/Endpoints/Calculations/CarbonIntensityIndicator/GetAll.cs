using ShipCalc.Application.Abstractions.CQS;
using ShipCalc.Application.Calculation.CarbonIntensityIndicator;

namespace ShipCalc.Api.Endpoints.Calculations.CarbonIntensityIndicator;

public sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.CarbonIntensityIndicator.Calculations, async (
            IQueryDispatcher dispatcher,
            CancellationToken cancellationToken) =>
        {
            var query = new GetCalcnsQuery();

            var results = await dispatcher
                .DispatchAsync<GetCalcnsQuery, List<CalcnResponse>>(
                    query,
                    cancellationToken);

            return Results.Ok(results);
        })
        .WithTags(Tags.CIICalcns)
        .WithName("GetAllCIICalculations")
        .WithOpenApi(operation =>
        {
            operation.Summary = "Get all Carbon Intensity Indicator calculations";
            operation.Description = "Returns all Carbon Intensity Indicator calculations with ship details";
            return operation;
        });
    }
}