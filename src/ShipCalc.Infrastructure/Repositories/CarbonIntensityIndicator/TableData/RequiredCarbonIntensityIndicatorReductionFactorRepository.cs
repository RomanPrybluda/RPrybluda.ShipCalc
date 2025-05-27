using Microsoft.EntityFrameworkCore;
using ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;
using ShipCalc.Infrastructure.Data;

namespace ShipCalc.Infrastructure.Repositories.CarbonIntensityIndicator.TableData;

public class RequiredCarbonIntensityIndicatorReductionFactorRepository :
    IReductionFactorRepo
{
    private readonly ShipCalcDbContext _context;

    public RequiredCarbonIntensityIndicatorReductionFactorRepository(
        ShipCalcDbContext context)
    {
        _context = context;
    }

    public async Task<RefLineReductionFactor?> GetByYearAsync(
        int year,
        CancellationToken cancellationToken = default)
    {
        var refLineReductionFactor = await _context.CIIReqReductionFactors
            .AsNoTracking()
            .FirstOrDefaultAsync(rf => rf.Year == year);

        return refLineReductionFactor;
    }

}