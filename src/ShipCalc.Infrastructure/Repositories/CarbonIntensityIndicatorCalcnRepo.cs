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
        var calculation = await _context.CIICalculations
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);
        return calculation;
    }

    public async Task<CarbonIntensityIndicatorCalculation?> GetByShipIdAsync(Guid shipId,
        CancellationToken cancellationToken = default)
    {
        var calculation = await _context.CIICalculations
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.ShipId == shipId);
        return calculation;
    }

    public async Task<IEnumerable<CarbonIntensityIndicatorCalculation?>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var calculations = await _context.CIICalculations
            .AsNoTracking()
            .ToListAsync();
        return calculations;
    }

    public async Task AddAsync(CarbonIntensityIndicatorCalculation record,
        CancellationToken cancellationToken = default)
    {
        await _context.CIICalculations.AddAsync(record);
    }

    public async Task DeleteAsync(CarbonIntensityIndicatorCalculation calcn)
    {
        _context.CIICalculations.Remove(calcn);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}