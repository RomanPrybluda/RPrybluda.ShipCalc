using ShipCalc.Application.Abstractions.CQS;
using ShipCalc.Application.Calculation.CarbonIntensityIndicator;

namespace ShipCalc.Api.Endpoints.Calculations.CarbonIntensityIndicator;

public sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("cii/calculations/{id:guid}", async (
            Guid id,
            IQueryDispatcher dispatcher,
            CancellationToken cancellationToken) =>
        {
            var query = new GetCalcnByIdQuery(id);

            var result = await dispatcher
                .Dispatch<GetCalcnByIdQuery, CalcnResponse>(
                    query,
                    cancellationToken);

            return Results.Ok(result);
        })
        .WithTags(Tags.CIICalcns)
        .WithName("GetByIdCIICalculation")
        .WithOpenApi(operation =>
        {
            operation.Summary = "Get Carbon Intensity Indicator calculation by Id";
            operation.Description = "Returns Carbon Intensity Indicator calculation with ship details";
            return operation;
        });
    }
}