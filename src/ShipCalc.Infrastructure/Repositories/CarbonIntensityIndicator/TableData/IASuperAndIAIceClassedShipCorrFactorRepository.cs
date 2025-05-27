using Microsoft.EntityFrameworkCore;
using ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;
using ShipCalc.Domain.Calculation.CorrectionFactors;
using ShipCalc.Domain.Enums;
using ShipCalc.Infrastructure.Data;

namespace ShipCalc.Infrastructure.Repositories.CarbonIntensityIndicator.TableData;

public class IASuperAndIAIceClassedShipCorrFactorRepository :
    IIASuperAndIAIceCorrFactorRepo
{
    private readonly ShipCalcDbContext _context;

    public IASuperAndIAIceClassedShipCorrFactorRepository(
        ShipCalcDbContext context)
    {
        _context = context;
    }

    public async Task<IASuperAndIAIceCorrFactor?> GetByIceClassAsync(
        IceClass iceClass,
        CancellationToken cancellationToken = default)
    {
        var iASuperAndIAIceCorrFactor = await _context.IASuperAndIAIceCorrFactors
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.IceClass == iceClass);

        return iASuperAndIAIceCorrFactor;
    }
}