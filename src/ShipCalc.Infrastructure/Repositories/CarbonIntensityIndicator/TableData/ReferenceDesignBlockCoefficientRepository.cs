using Microsoft.EntityFrameworkCore;
using ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;
using ShipCalc.Domain.Calculation.CorrectionFactors;
using ShipCalc.Domain.Enums;
using ShipCalc.Infrastructure.Data;

namespace ShipCalc.Infrastructure.Repositories.CarbonIntensityIndicator.TableData;

public class ReferenceDesignBlockCoefficientRepository :
    IRefDesignBlockCoeffRepo
{
    private readonly ShipCalcDbContext _context;

    public ReferenceDesignBlockCoefficientRepository(
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