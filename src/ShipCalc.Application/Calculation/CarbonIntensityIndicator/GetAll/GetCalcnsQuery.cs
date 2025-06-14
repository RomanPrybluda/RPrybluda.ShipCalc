using ShipCalc.Application.Abstractions.CQS;

namespace ShipCalc.Application.Calculation.CarbonIntensityIndicator
{
    public sealed class GetCalcnsQuery : IQuery<List<CalcnResponse>>;
}
