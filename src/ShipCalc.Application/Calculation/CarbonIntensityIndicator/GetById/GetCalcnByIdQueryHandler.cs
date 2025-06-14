using ShipCalc.Application.Abstractions;
using ShipCalc.Application.Abstractions.CQS;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

namespace ShipCalc.Application.Calculation.CarbonIntensityIndicator;

public sealed class GetCalcnByIdQueryHandler
    : IQueryHandler<GetCalcnByIdQuery, CalcnByIdResponse>
{
    private readonly ICarbonIntensityIndicatorCalcnRepo _ciiCalcnRepo;
    private readonly IShipRepo _shipRepo;

    public GetCalcnByIdQueryHandler(
        ICarbonIntensityIndicatorCalcnRepo ciiCalcnRepo,
        IShipRepo shipRepo)
    {
        _ciiCalcnRepo = ciiCalcnRepo;
        _shipRepo = shipRepo;
    }

    public async Task<CalcnByIdResponse> Handle(
        GetCalcnByIdQuery query,
        CancellationToken cancellationToken)
    {

        var calculation = await _ciiCalcnRepo.GetByIdAsync(query.id, cancellationToken);
        if (calculation == null)
            throw new CalculationNotFound(query.id);

        var ship = await _shipRepo.GetByIdAsync(calculation.ShipId, cancellationToken);
        if (ship == null)
            throw new ShipNotFound(calculation.ShipId);

        var calculationDTO = CalcnByIdResponse.ToCalcnByIdResponse(calculation, ship);

        return calculationDTO;
    }
}

