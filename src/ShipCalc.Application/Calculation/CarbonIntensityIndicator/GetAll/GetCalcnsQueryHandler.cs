using ShipCalc.Application.Abstractions;
using ShipCalc.Application.Abstractions.CQS;
using ShipCalc.Domain;
using ShipCalc.Domain.Enums;

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
            Ship? relatedShip = null;
            if (calculation.ShipId != null)
            {
                shipDictionary.TryGetValue(calculation.ShipId, out relatedShip);
            }

            var calcnResponse = new CalcnResponse
            {
                ShipName = relatedShip?.ShipName ?? string.Empty,
                ImoNumber = relatedShip?.ImoNumber ?? 0,
                ShipType = relatedShip?.ShipType ?? ShipType.NotApplicable,
                IceClass = relatedShip?.IceClass ?? IceClass.NotApplicable,

                RequiredCarbonIntensityIndicator = calculation.RequiredCarbonIntensityIndicator,
                AttainedCarbonIntensityIndicator = calculation.AttainedCarbonIntensityIndicator,
                CarbonIntensityIndicatorNumericalRating = calculation.CarbonIntensityIndicatorNumericalRating,
                CarbonIntensityIndicatorRating = calculation.CarbonIntensityIndicatorRating
            };

            responseList.Add(calcnResponse);
        }

        return responseList;
    }
}
