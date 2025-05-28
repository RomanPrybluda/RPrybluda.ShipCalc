using Microsoft.EntityFrameworkCore;
using ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;
using ShipCalc.Domain.Calculation.CorrectionFactors;
using ShipCalc.Domain.Enums;
using ShipCalc.Infrastructure.Database;

namespace ShipCalc.Infrastructure.Repositories.CarbonIntensityIndicator.TableData;

public class RefDesignBlockCoeffRepo :
    IRefDesignBlockCoeffRepo
{
    private readonly ShipCalcDbContext _context;

    public RefDesignBlockCoeffRepo(
        ShipCalcDbContext context)
    {
        _context = context;
    }

    public async Task<RefDesignBlockCoeff?> GetByShipTypeAndDeadweightAsync(
        ShipType shipType,
        decimal deadweight,
        CancellationToken cancellationToken = default)
    {
        var refDesignBlockCoeff = await _context.RefDesignBlockCoeffs
            .AsNoTracking()
            .FirstOrDefaultAsync(
                c => c.ShipType == shipType
                && (!c.MinDeadweight.HasValue || deadweight >= c.MinDeadweight)
                && (!c.MaxDeadweight.HasValue || deadweight < c.MaxDeadweight));

        return refDesignBlockCoeff;
    }
}