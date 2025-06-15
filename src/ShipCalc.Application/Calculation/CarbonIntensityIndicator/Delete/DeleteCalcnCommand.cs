using ShipCalc.Application.Abstractions.CQS;

namespace ShipCalc.Application.Calculation.CarbonIntensityIndicator;

public sealed record DeleteCalcnCommand(Guid Id) : ICommand<Unit>;
