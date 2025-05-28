using Microsoft.EntityFrameworkCore;
using ShipCalc.Application.Abstractions;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;
using ShipCalc.Infrastructure.Database;

namespace ShipCalc.Infrastructure.Repositories;

public class CarbonIntensityIndicatorCalcnRepo :
    ICarbonIntensityIndicatorCalcnRepo
{
    private readonly ShipCalcDbContext _context;

    public CarbonIntensityIndicatorCalcnRepo(
        ShipCalcDbContext context)
    {
        _context = context;
    }

    public async Task<CarbonIntensityIndicatorCalculation?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.CIICalculations
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<CarbonIntensityIndicatorCalculation?> GetByShipIdAsync(Guid shipId,
        CancellationToken cancellationToken = default)
    {
        return await _context.CIICalculations
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.ShipId == shipId);
    }

    public async Task<IEnumerable<CarbonIntensityIndicatorCalculation?>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.CIICalculations
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(CarbonIntensityIndicatorCalculation record,
        CancellationToken cancellationToken = default)
    {
        await _context.CIICalculations.AddAsync(record);
    }

    public async Task DeleteAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        await _context.CIICalculations
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}