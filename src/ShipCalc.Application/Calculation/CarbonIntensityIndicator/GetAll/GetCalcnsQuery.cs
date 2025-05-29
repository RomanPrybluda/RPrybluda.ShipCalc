using ShipCalc.Application.Abstractions.CQRS;

namespace ShipCalc.Application.Calculation.CarbonIntensityIndicator
{
    public sealed record GetCalcnsQuery : IQuery<List<CalcnResponse>>;
}
