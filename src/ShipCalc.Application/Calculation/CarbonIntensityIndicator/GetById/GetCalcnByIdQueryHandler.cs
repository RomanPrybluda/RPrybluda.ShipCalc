using ShipCalc.Application.Abstractions;
using ShipCalc.Application.Abstractions.CQS;
using ShipCalc.Domain;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Calculation.CarbonIntensityIndicator;

public sealed class GetCalcnByIdQueryHandler
    : IQueryHandler<GetCalcnByIdQuery, CalcnResponse>
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

    public async Task<CalcnResponse> Handle(
        GetCalcnByIdQuery query,
        CancellationToken cancellationToken)
    {

        var calculation = await _ciiCalcnRepo.GetByIdAsync(query.id, cancellationToken);
        if (calculation == null)
            throw new Exception("Calculation not found");

        Ship? relatedShip = null;
        if (calculation.ShipId != null)
        {
            relatedShip = await _shipRepo.GetByIdAsync(calculation.ShipId, cancellationToken);
        }



        return new CalcnResponse
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
    }
}

