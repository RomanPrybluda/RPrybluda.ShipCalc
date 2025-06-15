using ShipCalc.Application.Abstractions.CQS;
using ShipCalc.Application.Calculation.CarbonIntensityIndicator;

namespace ShipCalc.Api.Endpoints.Calculations.CarbonIntensityIndicator;

public sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(ApiRoutes.CarbonIntensityIndicator.CalculationById, async (
            Guid id,
            UpdateCalcnRequestDTO request,
            ICommandDispatcher dispatcher,
            CancellationToken cancellationToken) =>
        {
            var command = UpdateCalcnRequestDTO.ToCommand(id, request);

            var result = await dispatcher
                .DispatchAsync<UpdateCalcnCommand, UpdateCalcnResponseDTO>(
                    command,
                    cancellationToken);

            return Results.Ok(result);
        })
        .WithTags(Tags.CIICalcns)
        .WithName("UpdateCIICalculation")
        .WithOpenApi(operation =>
        {
            operation.Summary = "Update an existing Carbon Intensity Indicator calculation";
            operation.Description = "Updates the details of an existing Carbon Intensity Indicator calculation for a ship";
            return operation;
        });
    }
}
