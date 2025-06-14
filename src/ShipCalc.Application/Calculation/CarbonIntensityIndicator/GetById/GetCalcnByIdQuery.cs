using ShipCalc.Application.Abstractions.CQS;

namespace ShipCalc.Application.Calculation.CarbonIntensityIndicator
{
    public sealed record GetCalcnByIdQuery(Guid id) : IQuery<CalcnByIdResponse>;

}
