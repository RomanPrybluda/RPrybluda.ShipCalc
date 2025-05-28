using ShipCalc.Application.Abstractions.CQRS;
using ShipCalc.Application.Calculation.CarbonIntensityIndicator;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Api.Endpoints.Calculations.CarbonIntensityIndicator;

public sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("cii-calculations", async (
            Request request,
            ICommandDispatcher dispatcher,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateCIICalcnCommand
            {
                ImoNumber = request.ImoNumber,
                ShipName = request.ShipName,
                GrossTonnage = request.GrossTonnage,
                SummerDeadweight = request.SummerDeadweight,
                BlockCoefficient = request.BlockCoefficient,
                CargoCompartmentCubicCapacity = request.CargoCompartmentCubicCapacity,
                ShipType = request.ShipType,
                IceClass = request.IceClass,
                Co2EmissionsInTons = request.Co2EmissionsInTons,
                DistanceTravelledInNMs = request.DistanceTravelledInNMs,
                Year = request.Year
            };

            var result = await dispatcher
            .Dispatch<CreateCIICalcnCommand, CarbonIntensityIndicatorCalculation>(
                command,
                cancellationToken);

            return Results.Created($"/cii-calculations/{result.Id}", result);
        })
        .WithTags(Tags.Calcs)
        .WithName("CreateCIICalculation") // Уникальное имя операции
        .WithOpenApi(operation =>
        {
            operation.Summary = "Create new CII calculation";
            operation.Description = "Creates a new Carbon Intensity Indicator calculation for a ship";
            return operation;
        });
    }

    public sealed class Request
    {
        public int ImoNumber { get; set; }
        public string ShipName { get; set; } = string.Empty;
        public decimal GrossTonnage { get; set; }
        public decimal SummerDeadweight { get; set; }
        public decimal BlockCoefficient { get; set; }
        public decimal CargoCompartmentCubicCapacity { get; set; }
        public ShipType ShipType { get; set; }
        public IceClass IceClass { get; set; }
        public decimal Co2EmissionsInTons { get; set; }
        public decimal DistanceTravelledInNMs { get; set; }
        public int Year { get; set; }
    }
}
