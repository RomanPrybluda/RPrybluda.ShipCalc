using Microsoft.OpenApi.Models;
using ShipCalc.Api.Dispatchers;
using ShipCalc.Api.Endpoints;
using ShipCalc.Api.Extensions;
using ShipCalc.Application.Abstractions;
using ShipCalc.Application.Abstractions.CQRS;
using ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;
using ShipCalc.Application.Calculation.CarbonIntensityIndicator;
using ShipCalc.Application.Calculators.CarbonIntensityIndicator;
using ShipCalc.Application.Calculators.CorrectionFactors;
using ShipCalc.Domain.Abstractions.CarbonIntensityIndicator;
using ShipCalc.Domain.Abstractions.CorrFactor;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;
using ShipCalc.Infrastructure;
using ShipCalc.Infrastructure.Repositories;
using ShipCalc.Infrastructure.Repositories.CarbonIntensityIndicator.TableData;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<IRefLineParamsRepo, CarbonIntensityIndicatorRefLineParamsRepo>();
builder.Services.AddScoped<IReductionFactorRepo, ReqCarbonIntensityIndicatorReductionFactorRepo>();
builder.Services.AddScoped<IRatingThresholdsRepo, CarbonIntensityIndicatorRatingThresholdsRepo>();
builder.Services.AddScoped<ICarbonIntensityIndicatorCalcnRepo, CarbonIntensityIndicatorCalcnRepo>();
builder.Services.AddScoped<IIASuperAndIAIceCorrFactorRepo, IASuperAndIAIceCorrFactorRepo>();
builder.Services.AddScoped<IRefDesignBlockCoeffRepo, RefDesignBlockCoeffRepo>();
builder.Services.AddScoped<ICapacityIceStrengthCorrFactorRepo, CapacityIceStrengthCorrFactorRepo>();
builder.Services.AddScoped<ICarbonIntensityIndicatorCalcnRepo, CarbonIntensityIndicatorCalcnRepo>();

builder.Services.AddScoped<IShipRepo, ShipRepo>();

builder.Services.AddScoped<ICapacityCalculator, CapacityCalculator>();
builder.Services.AddScoped<IRefLineCalculator, CarbonIntensityIndicatorReferenceLineCalculator>();
builder.Services.AddScoped<IRequiredCarbonIntensityIndicatorCalculator, RequiredCarbonIntensityIndicatorCalculator>();
builder.Services.AddScoped<IAttainedCarbonIntensityIndicatorCalculator, AttainedCarbonIntensityIndicatorCalculator>();
builder.Services.AddScoped<IIceClasedShipCapacityCorrFactorCalculator, IceClasedShipCapacityCorrFactorCalculator>();

builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();

builder.Services.AddScoped<
    ICommandHandler<CreateCIICalcnCommand, CarbonIntensityIndicatorCalculation>,
    CreateCIICalcnCommandHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ShipCalc API",
        Version = "v1",
        Description = "API for ship calculations"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

var endpointTypes = Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(t => typeof(IEndpoint).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

foreach (var type in endpointTypes)
{
    if (Activator.CreateInstance(type) is IEndpoint endpoint)
    {
        endpoint.MapEndpoint(app);
    }
}

await app.InitializeDatabaseAsync();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.Run();