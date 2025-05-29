using FluentValidation;
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
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

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

builder.Services
    .AddScoped<ICommandHandler<CreateCalcnCommand, CarbonIntensityIndicatorCalculation>,
    CreateCalcnCommandHandler>();

builder.Services
    .AddTransient<IValidator<CreateCalcnCommand>, CreateCalcnCommandValidator>();

builder.Services
    .AddScoped<IQueryHandler<GetCalcnsQuery, List<CalcnResponse>>,
    GetCalcnsQueryHandler>();

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

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.Strict;
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