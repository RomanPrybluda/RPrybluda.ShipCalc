using Microsoft.EntityFrameworkCore;
using ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;
using ShipCalc.Domain.Enums;
using ShipCalc.Infrastructure.Database;

namespace ShipCalc.Infrastructure.Repositories.CarbonIntensityIndicator.TableData;

public class CarbonIntensityIndicatorRefLineParamsRepo :
    IRefLineParamsRepo
{
    private readonly ShipCalcDbContext _context;

    public CarbonIntensityIndicatorRefLineParamsRepo(
        ShipCalcDbContext context)
    {
        _context = context;
    }

    public async Task<RefLineParams?> GetByShipTypeAndCapacityAsync(
        ShipType shipType,
        decimal capacity,
        CancellationToken cancellationToken = default)
    {
        var parameters = await _context.CIIRefLineParams
            .AsNoTracking()
            .Where(p => p.ShipType == shipType &&
                        (!p.LowerBound.HasValue || capacity >= p.LowerBound.Value) &&
                        (!p.UpperBound.HasValue || capacity < p.UpperBound.Value))
            .FirstOrDefaultAsync();

        return parameters;
    }
}