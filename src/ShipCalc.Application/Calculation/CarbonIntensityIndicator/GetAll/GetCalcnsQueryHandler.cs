using ShipCalc.Application.Abstractions;
using ShipCalc.Application.Abstractions.CQS;

namespace ShipCalc.Application.Calculation.CarbonIntensityIndicator;

public sealed class GetCalcnsQueryHandler
    : IQueryHandler<GetCalcnsQuery, List<CalcnResponse>>
{
    private readonly ICarbonIntensityIndicatorCalcnRepo _ciiCalcnRepo;
    private readonly IShipRepo _shipRepo;

    public GetCalcnsQueryHandler(
        ICarbonIntensityIndicatorCalcnRepo ciiCalcnRepo,
        IShipRepo shipRepo)
    {
        _ciiCalcnRepo = ciiCalcnRepo;
        _shipRepo = shipRepo;
    }

    public async Task<List<CalcnResponse>> Handle(
        GetCalcnsQuery query,
        CancellationToken cancellationToken)
    {
        var calculations = await _ciiCalcnRepo.GetAllAsync(cancellationToken);
        var ships = await _shipRepo.GetAllAsync(cancellationToken);

        var shipDictionary = ships.ToDictionary(ship => ship.Id);

        var responseList = new List<CalcnResponse>();

        foreach (var calculation in calculations)
        {
            shipDictionary.TryGetValue(calculation.ShipId, out var relatedShip);
            responseList.Add(CalcnResponse.ToCalcnResponse(calculation, relatedShip));
        }

        return responseList;
    }
}
