using ShipCalc.Application.Abstractions.CQS;
using ShipCalc.Application.Calculation.CarbonIntensityIndicator;

namespace ShipCalc.Api.Endpoints.Calculations.CarbonIntensityIndicator;

public sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.CarbonIntensityIndicator.Calculations, async (
            CreateCalcnRequestDTO request,
            ICommandDispatcher dispatcher,
            CancellationToken cancellationToken) =>
        {
            var command = CreateCalcnRequestDTO.ToCommand(request);

            var result = await dispatcher
            .DispatchAsync<CreateCalcnCommand, CreateCalcnResponseDTO>(
                command,
                cancellationToken);

            return Results.Created($"/{ApiRoutes.CarbonIntensityIndicator.Calculations}", result);
        })
        .WithTags(Tags.CIICalcns)
        .WithName("CreateCIICalculation")
        .WithOpenApi(operation =>
        {
            operation.Summary = "Create new Carbon Intensity Indicator calculation";
            operation.Description = "Creates a new Carbon Intensity Indicator calculation for a ship";
            return operation;
        });
    }
}
