using Microsoft.EntityFrameworkCore;
using ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;
using ShipCalc.Domain.Enums;
using ShipCalc.Infrastructure.Database;

namespace ShipCalc.Infrastructure.Repositories.CarbonIntensityIndicator.TableData;

public class CarbonIntensityIndicatorRatingThresholdsRepo : IRatingThresholdsRepo
{
    private readonly ShipCalcDbContext _context;

    public CarbonIntensityIndicatorRatingThresholdsRepo(
        ShipCalcDbContext context)
    {
        _context = context;
    }

    public async Task<RatingThreshold?> GetThresholdsAsync(
        ShipType shipType,
        decimal deadWeight,
        CancellationToken cancellationToken = default)
    {
        int deadWeightInt = (int)Math.Round(deadWeight);

        var threshold = await _context.CIIRatingThresholds
            .AsNoTracking()
            .FirstOrDefaultAsync
                (t => t.ShipType == shipType &&
                (!t.LowerDeadweight.HasValue || deadWeightInt >= (decimal)t.LowerDeadweight) &&
                (!t.UpperDeadweight.HasValue || deadWeightInt < (decimal)t.UpperDeadweight));

        return threshold;
    }
}