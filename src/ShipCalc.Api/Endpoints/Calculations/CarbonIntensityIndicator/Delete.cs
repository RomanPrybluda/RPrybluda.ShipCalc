using ShipCalc.Application.Abstractions.CQS;
using ShipCalc.Application.Calculation.CarbonIntensityIndicator;

namespace ShipCalc.Api.Endpoints.Calculations.CarbonIntensityIndicator;

public sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiRoutes.CarbonIntensityIndicator.CalculationById, async (
            Guid id,
            ICommandDispatcher dispatcher,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteCalcnCommand(id);

            await dispatcher.DispatchAsync<DeleteCalcnCommand, Unit>(
                command,
                cancellationToken);

            return Results.NoContent();
        })
        .WithTags(Tags.CIICalcns)
        .WithName("DeleteCIICalculation")
        .WithOpenApi(operation =>
        {
            operation.Summary = "Delete Carbon Intensity Indicator calculation by Id";
            operation.Description = "Deletes a Carbon Intensity Indicator calculation for a ship";
            return operation;
        });
    }
}
