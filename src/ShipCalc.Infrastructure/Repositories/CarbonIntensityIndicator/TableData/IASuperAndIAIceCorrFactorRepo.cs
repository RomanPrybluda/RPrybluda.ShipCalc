using Microsoft.EntityFrameworkCore;
using ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;
using ShipCalc.Domain.Calculation.CorrectionFactors;
using ShipCalc.Domain.Enums;
using ShipCalc.Infrastructure.Database;

namespace ShipCalc.Infrastructure.Repositories.CarbonIntensityIndicator.TableData;

public class IASuperAndIAIceCorrFactorRepo :
    IIASuperAndIAIceCorrFactorRepo
{
    private readonly ShipCalcDbContext _context;

    public IASuperAndIAIceCorrFactorRepo(
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