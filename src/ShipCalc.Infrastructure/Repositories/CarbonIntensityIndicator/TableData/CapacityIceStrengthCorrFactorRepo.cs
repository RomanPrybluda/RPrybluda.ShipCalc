using Microsoft.EntityFrameworkCore;
using ShipCalc.Application.Abstractions.Repositories.CarbonIntensityIndicator.TableData;
using ShipCalc.Domain.Calculation.CorrectionFactors;
using ShipCalc.Domain.Enums;
using ShipCalc.Infrastructure.Database;

namespace ShipCalc.Infrastructure.Repositories.CarbonIntensityIndicator.TableData;

public class CapacityIceStrengthCorrFactorRepo :
    ICapacityIceStrengthCorrFactorRepo
{
    private readonly ShipCalcDbContext _context;

    public CapacityIceStrengthCorrFactorRepo(
        ShipCalcDbContext context)
    {
        _context = context;
    }

    public async Task<CapacityIceStrengthCorrFactor?> GetByIceClassAsync(
        IceClass iceClass,
        CancellationToken cancellationToken = default)
    {
        var correctionFactor = await _context.CapacityIceStrengthCorrFactors
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.IceClass == iceClass);

        return correctionFactor;
    }
}