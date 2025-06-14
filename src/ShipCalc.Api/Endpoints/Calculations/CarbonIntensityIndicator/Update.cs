using ShipCalc.Application.Abstractions.CQS;
using ShipCalc.Application.Calculation.CarbonIntensityIndicator;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

namespace ShipCalc.Api.Endpoints.Calculations.CarbonIntensityIndicator;

public sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("cii/calculations/{id:guid}", async (
            Guid id,
            UpdateCalcnDTO request,
            ICommandDispatcher dispatcher,
            CancellationToken cancellationToken) =>
        {
            if (id != id)
                return Results.BadRequest("ID in URL must match ID in request body.");

            var command = UpdateCalcnDTO.ToCIICalcn(id, request);

            var result = await dispatcher
                .Dispatch<UpdateCalcnCommand, CarbonIntensityIndicatorCalculation>(
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
