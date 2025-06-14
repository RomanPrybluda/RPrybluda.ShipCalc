using ShipCalc.Application.Abstractions.CQS;
using ShipCalc.Application.Calculation.CarbonIntensityIndicator;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

namespace ShipCalc.Api.Endpoints.Calculations.CarbonIntensityIndicator;

public sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("cii/calculations", async (
            CreateCalcnDTO request,
            ICommandDispatcher dispatcher,
            CancellationToken cancellationToken) =>
        {
            var command = CreateCalcnDTO.ToCIICalcn(request);

            var result = await dispatcher
            .Dispatch<CreateCalcnCommand, CarbonIntensityIndicatorCalculation>(
                command,
                cancellationToken);

            return Results.Created($"/cii/calculations/{result.Id}", result);
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
