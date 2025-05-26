using Microsoft.EntityFrameworkCore;
using ShipCalc.Application.Abstractions;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;
using ShipCalc.Infrastructure.Data;

namespace ShipCalc.Infrastructure.Repositories;

public class CarbonIntensityIndicatorCalcRecordRepository :
    ICalculationDataRepo
{
    private readonly ShipCalcDbContext _context;

    public CarbonIntensityIndicatorCalcRecordRepository(
        ShipCalcDbContext context)
    {
        _context = context;
    }

    public async Task<CalculationData?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.CalculationDatas
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<CalculationData?> GetByShipIdAsync(Guid shipId,
        CancellationToken cancellationToken = default)
    {
        return await _context.CalculationDatas
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.ShipId == shipId);
    }

    public async Task<IEnumerable<CalculationData?>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.CalculationDatas
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(CalculationData record,
        CancellationToken cancellationToken = default)
    {
        await _context.CalculationDatas.AddAsync(record);
    }

    public async Task DeleteAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        await _context.CalculationDatas
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}